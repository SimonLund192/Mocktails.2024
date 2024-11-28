using Mocktails.ApiClient.Products;
using Mocktails.ApiClient.Products.DTOs;

namespace Employee.App;

public partial class ProductsControl : UserControl
{
    private readonly MocktailsApiClient _mocktailsApiClient;

    // Constructor
    public ProductsControl()
    {
        InitializeComponent();
        _mocktailsApiClient = new MocktailsApiClient("https://localhost:7203");
        SetupProductsControl();
    }

    private void SetupProductsControl()
    {
        // Setup DataGridView
        dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvProducts.MultiSelect = false;
        dgvProducts.AllowUserToAddRows = false;
        dgvProducts.ReadOnly = true;

        // Configure columns
        dgvProducts.Columns.Clear();
        dgvProducts.Columns.AddRange(new[]
        {
            CreateColumn("Product ID", "ProductId", 100),
            CreateColumn("Product Name", "ProductName", 200),
            CreateColumn("Price", "Price", 100),
            CreateColumn("Quantity", "Quantity", 100),
            CreateColumn("Description", "Description", 200),
            CreateColumn("Image URL", "ImageUrl", 200)
        });

        // Add event handlers
        btnAdd.Click += BtnAdd_Click;
        btnEdit.Click += BtnEdit_Click;
        btnDelete.Click += BtnDelete_Click;
        btnSearch.Click += BtnSearch_Click;

        LoadProductsFromApi();
    }

    private DataGridViewTextBoxColumn CreateColumn(string headerText, string dataPropertyName, int width)
    {
        return new DataGridViewTextBoxColumn
        {
            HeaderText = headerText,
            DataPropertyName = dataPropertyName,
            Width = width
        };
    }

    private async void LoadProductsFromApi()
    {
        try
        {
            var products = await _mocktailsApiClient.GetMocktailsAsync();
            dgvProducts.Rows.Clear();

            foreach (var product in products)
            {
                dgvProducts.Rows.Add(
                    product.Id,
                    product.Name,
                    product.Price.ToString("F2"),
                    product.Quantity,
                    product.Description,
                    product.ImageUrl
                );
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void BtnAdd_Click(object sender, EventArgs e)
    {
        using var form = new ProductForm("Add Product");
        if (form.ShowDialog() == DialogResult.OK && ValidateInput(form))
        {
            try
            {
                var newProduct = new MocktailDTO
                {
                    Name = form.txtProductName.Text,
                    Description = form.txtDescription.Text,
                    Price = decimal.Parse(form.txtPrice.Text),
                    Quantity = int.Parse(form.txtQuantity.Text),
                    ImageUrl = form.txtImageUrl.Text
                };

                await _mocktailsApiClient.CreateMocktailAsync(newProduct);
                LoadProductsFromApi();
                MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private bool ValidateInput(ProductForm form)
    {
        if (string.IsNullOrWhiteSpace(form.txtProductName.Text))
        {
            MessageBox.Show("Please enter a product name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (string.IsNullOrWhiteSpace(form.txtDescription.Text))
        {
            MessageBox.Show("Please enter a description.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (!decimal.TryParse(form.txtPrice.Text, out _))
        {
            MessageBox.Show("Please enter a valid price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (!int.TryParse(form.txtQuantity.Text, out _))
        {
            MessageBox.Show("Please enter a valid quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (string.IsNullOrWhiteSpace(form.txtImageUrl.Text))
        {
            MessageBox.Show("Please enter an image URL.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        return true;
    }

    private async void BtnEdit_Click(object sender, EventArgs e)
    {
        if (dgvProducts.SelectedRows.Count == 0)
        {
            MessageBox.Show("Please select a product to edit.", "Edit Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var row = dgvProducts.SelectedRows[0];
        var productId = row.Cells[0].Value?.ToString();

        if (string.IsNullOrWhiteSpace(productId))
        {
            MessageBox.Show("Invalid product selected.", "Edit Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Fetch the selected product from the API
        try
        {
            var product = await _mocktailsApiClient.GetMocktailByIdAsync(productId);
            using var form = new ProductForm("Edit Product")
            {
                txtProductName = { Text = product.Name },
                txtDescription = { Text = product.Description },
                txtPrice = { Text = product.Price.ToString("F2") },
                txtQuantity = { Text = product.Quantity.ToString() },
                txtImageUrl = { Text = product.ImageUrl }
            };

            if (form.ShowDialog() == DialogResult.OK && ValidateInput(form))
            {
                // Update the product with the new details
                product.Name = form.txtProductName.Text;
                product.Description = form.txtDescription.Text;
                product.Price = decimal.Parse(form.txtPrice.Text);
                product.Quantity = int.Parse(form.txtQuantity.Text);
                product.ImageUrl = form.txtImageUrl.Text;

                await _mocktailsApiClient.UpdateMocktailAsync(product);
                LoadProductsFromApi(); // Refresh the product list
                MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to edit product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    private void BtnDelete_Click(object sender, EventArgs e)
    {
        // Implementation for deleting a product
    }

    private void BtnSearch_Click(object sender, EventArgs e)
    {
        string searchText = textBox1.Text.ToLower();

        foreach (DataGridViewRow row in dgvProducts.Rows)
        {
            bool matchFound = false;
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value?.ToString().ToLower().Contains(searchText) == true)
                {
                    matchFound = true;
                    break;
                }
            }
            row.Visible = matchFound || string.IsNullOrWhiteSpace(searchText);
        }
    }

    private class ProductForm : Form
    {
        public TextBox txtProductName { get; private set; }
        public TextBox txtDescription { get; private set; }
        public TextBox txtPrice { get; private set; }
        public TextBox txtQuantity { get; private set; }
        public TextBox txtImageUrl { get; private set; }
        public Button btnSave { get; private set; }
        public Button btnCancel { get; private set; }

        public ProductForm(string title)
        {
            Text = title;
            Size = new Size(400, 400);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Padding = new Padding(20);

            Controls.AddRange(new Control[]
            {
                CreateLabel("Name:", 30, 30),
                txtProductName = CreateTextBox(150, 30),

                CreateLabel("Description:", 30, 70),
                txtDescription = CreateTextBox(150, 70),

                CreateLabel("Price:", 30, 110),
                txtPrice = CreateTextBox(150, 110),

                CreateLabel("Quantity:", 30, 150),
                txtQuantity = CreateTextBox(150, 150),

                CreateLabel("Image URL:", 30, 190),
                txtImageUrl = CreateTextBox(150, 190),

                btnSave = CreateButton("Save", 180, 250, DialogResult.OK),
                btnCancel = CreateButton("Cancel", 270, 250, DialogResult.Cancel)
            });

            AcceptButton = btnSave;
            CancelButton = btnCancel;
        }

        private Label CreateLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                AutoSize = true
            };
        }

        private TextBox CreateTextBox(int x, int y)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(200, 23)
            };
        }

        private Button CreateButton(string text, int x, int y, DialogResult dialogResult)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(80, 30),
                DialogResult = dialogResult
            };
        }
    }
}
