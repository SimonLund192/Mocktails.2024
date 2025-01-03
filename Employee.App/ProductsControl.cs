using Mocktails.ApiClient.Products;
using Mocktails.ApiClient.Products.DTOs;
using Mocktails.Employee.App;

namespace Employee.App;

public partial class ProductsControl : UserControl
{
    private readonly MocktailsApiClient _mocktailsApiClient;

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
            CreateColumn("Mocktail ID", "Id", 100),
            CreateColumn("Mocktail Name", "Name", 200),
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
                    Name = form.txtProductName.Text,
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

    private async void BtnEdit_Click(object sender, EventArgs e)
    {
        if (dgvProducts.SelectedRows.Count == 0)
        {
            MessageBox.Show("Please select a mocktail to edit.", "Edit Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedRow = dgvProducts.SelectedRows[0];
        var mocktailId = selectedRow.Cells[0].Value?.ToString();

        if (!int.TryParse(mocktailId, out var mocktailIdInt))
        {
            MessageBox.Show("Invalid mocktail ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            var mocktail = await _mocktailsApiClient.GetMocktailByIdAsync(mocktailIdInt);
            using var form = new ProductForm("Edit Product")
            {
                txtProductName = { Text = mocktail.Name },
                txtDescription = { Text = mocktail.Description },
                txtPrice = { Text = mocktail.Price.ToString("F2") },
                txtQuantity = { Text = mocktail.Quantity.ToString() },
                txtImageUrl = { Text = mocktail.ImageUrl }
            };

            if (form.ShowDialog() == DialogResult.OK && ValidateInput(form))
            {
                mocktail.Name = form.txtProductName.Text;
                mocktail.Description = form.txtDescription.Text;
                mocktail.Price = decimal.Parse(form.txtPrice.Text);
                mocktail.Quantity = int.Parse(form.txtQuantity.Text);
                mocktail.ImageUrl = form.txtImageUrl.Text;

                await _mocktailsApiClient.UpdateMocktailAsync(mocktail);
                LoadMocktailsFromApi();
                MessageBox.Show("Mocktail updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to edit mocktail: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void BtnDelete_Click(object sender, EventArgs e)
    {
        if (dgvProducts.SelectedRows.Count == 0)
        {
            MessageBox.Show("Please select a mocktail to delete.", "Delete Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedRow = dgvProducts.SelectedRows[0];
        var mocktailId = selectedRow.Cells[0].Value?.ToString();

        if (!int.TryParse(mocktailId, out var mocktailIdInt))
        {
            MessageBox.Show("Invalid mocktail ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var confirmation = MessageBox.Show(
            "Are you sure you want to delete this mocktail? This action cannot be undone.",
            "Confirm Delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
        );

        if (confirmation != DialogResult.Yes) return;

        try
        {
            await _mocktailsApiClient.DeleteMocktailAsync(mocktailIdInt);
            LoadMocktailsFromApi();
            MessageBox.Show("Mocktail deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to delete mocktail: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnSearch_Click(object sender, EventArgs e)
    {
        var searchText = textBox1.Text.ToLower();

        foreach (DataGridViewRow row in dgvProducts.Rows)
        {
            row.Visible = row.Cells.Cast<DataGridViewCell>()
                .Any(cell => cell.Value?.ToString().ToLower().Contains(searchText) == true) ||
                string.IsNullOrWhiteSpace(searchText);
        }
    }

    private bool ValidateInput(ProductForm form)
    {
        return !(string.IsNullOrWhiteSpace(form.txtProductName.Text) ||
                 string.IsNullOrWhiteSpace(form.txtDescription.Text) ||
                 !decimal.TryParse(form.txtPrice.Text, out _) ||
                 !int.TryParse(form.txtQuantity.Text, out _) ||
                 string.IsNullOrWhiteSpace(form.txtImageUrl.Text));
    }

    private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}
