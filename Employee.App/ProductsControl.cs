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
            CreateColumn("Mocktail ID", "MocktailId", 100),
            CreateColumn("Mocktail Name", "MocktailName", 200),
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

        LoadMocktailsFromApi();
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

    private async void LoadMocktailsFromApi()
    {
        try
        {
            var mocktails = await _mocktailsApiClient.GetMocktailsAsync();
            dgvProducts.Rows.Clear();

            foreach (var mocktail in mocktails)
            {
                dgvProducts.Rows.Add(
                    mocktail.Id,
                    mocktail.Name,
                    mocktail.Price.ToString("F2"),
                    mocktail.Quantity,
                    mocktail.Description,
                    mocktail.ImageUrl
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
                var newMocktail = new MocktailDTO
                {
                    Name = form.txtMocktailName.Text,
                    Description = form.txtDescription.Text,
                    Price = decimal.Parse(form.txtPrice.Text),
                    Quantity = int.Parse(form.txtQuantity.Text),
                    ImageUrl = form.txtImageUrl.Text
                };

                await _mocktailsApiClient.CreateMocktailAsync(newMocktail);
                LoadMocktailsFromApi();
                MessageBox.Show("Mocktail added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add mocktail: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private bool ValidateInput(ProductForm form)
    {
        if (string.IsNullOrWhiteSpace(form.txtMocktailName.Text))
        {
            MessageBox.Show("Please enter a mocktail name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

    private async void BtnDelete_Click(object sender, EventArgs e)
    {
        if (dgvProducts.SelectedRows.Count == 0)
        {
            MessageBox.Show("Please select a mocktail to delete.", "Delete Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var row = dgvProducts.SelectedRows[0];
        var mocktailId = row.Cells[0].Value?.ToString();

        if (string.IsNullOrWhiteSpace(mocktailId))
        {
            MessageBox.Show("Invalid mocktail selected.", "Delete Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (!int.TryParse(mocktailId, out int mocktailIdInt))
        {
            MessageBox.Show("Invalid mocktail ID format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var confirmation = MessageBox.Show(
            "Are you sure you want to delete this mocktail? This action cannot be undone.",
            "Confirm Delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
        );

        if (confirmation != DialogResult.Yes)
        {
            return;
        }

        try
        {
            await _mocktailsApiClient.DeleteMocktailAsync(mocktailIdInt);
            LoadMocktailsFromApi(); // Refresh the product list
            MessageBox.Show("Mocktail deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to delete mocktail: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    private async void BtnEdit_Click(object sender, EventArgs e)
    {
        if (dgvProducts.SelectedRows.Count == 0)
        {
            MessageBox.Show("Please select a mocktail to edit.", "Edit Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var row = dgvProducts.SelectedRows[0];
        var mocktailId = row.Cells[0].Value?.ToString();

        if (string.IsNullOrWhiteSpace(mocktailId))
        {
            MessageBox.Show("Invalid mocktail selected.", "Edit Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Fetch the selected product from the API
        try
        {
            if (!int.TryParse(mocktailId, out int mocktailIdInt))
            {
                MessageBox.Show("Invalid mocktail ID format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var mocktail = await _mocktailsApiClient.GetMocktailByIdAsync(mocktailIdInt);
            using var form = new ProductForm("Edit Product")
            {
                txtMocktailName = { Text = mocktail.Name },
                txtDescription = { Text = mocktail.Description },
                txtPrice = { Text = mocktail.Price.ToString("F2") },
                txtQuantity = { Text = mocktail.Quantity.ToString() },
                txtImageUrl = { Text = mocktail.ImageUrl }
            };

            if (form.ShowDialog() == DialogResult.OK && ValidateInput(form))
            {
                // Update the product with the new details
                mocktail.Name = form.txtMocktailName.Text;
                mocktail.Description = form.txtDescription.Text;
                mocktail.Price = decimal.Parse(form.txtPrice.Text);
                mocktail.Quantity = int.Parse(form.txtQuantity.Text);
                mocktail.ImageUrl = form.txtImageUrl.Text;

                await _mocktailsApiClient.UpdateMocktailAsync(mocktail);
                LoadMocktailsFromApi(); // Refresh the product list
                MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to edit product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
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
        public TextBox txtMocktailName { get; private set; }
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
                txtMocktailName = CreateTextBox(150, 30),

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
