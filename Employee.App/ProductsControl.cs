namespace Employee.App;

public partial class ProductsControl : UserControl
{
    // Form for adding/editing products
    private class ProductForm : Form
    {
        public TextBox txtProductName;
        public TextBox txtPrice;
        public TextBox txtQuantity;
        public Button btnSave;
        public Button btnCancel;

        public ProductForm(string title = "Edit Product")
        {
            // Form settings
            this.Text = title;
            this.Size = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Padding = new Padding(20);

            // Create and position labels
            Label lblName = new Label
            {
                Text = "Name:",
                Location = new Point(30, 30),
                AutoSize = true
            };

            Label lblPrice = new Label
            {
                Text = "Price:",
                Location = new Point(30, 70),
                AutoSize = true
            };

            Label lblQuantity = new Label
            {
                Text = "Quantity:",
                Location = new Point(30, 110),
                AutoSize = true
            };

            // Initialize textboxes with proper positioning
            txtProductName = new TextBox
            {
                Location = new Point(100, 27),
                Size = new Size(250, 23)
            };

            txtPrice = new TextBox
            {
                Location = new Point(100, 67),
                Size = new Size(250, 23)
            };

            txtQuantity = new TextBox
            {
                Location = new Point(100, 107),
                Size = new Size(250, 23)
            };

            // Initialize buttons with proper positioning
            btnSave = new Button
            {
                Text = "Save",
                Location = new Point(180, 160),
                Size = new Size(80, 30),
                DialogResult = DialogResult.OK
            };

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(270, 160),
                Size = new Size(80, 30),
                DialogResult = DialogResult.Cancel
            };

            // Add all controls to form
            this.Controls.AddRange(new Control[]
            {
        lblName, lblPrice, lblQuantity,
        txtProductName, txtPrice, txtQuantity,
        btnSave, btnCancel
            });

            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }
    }

    public ProductsControl()
    {
        InitializeComponent();
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
        dgvProducts.Columns.AddRange(new DataGridViewColumn[]
        {
            new DataGridViewTextBoxColumn
            {
                HeaderText = "Product ID",
                DataPropertyName = "ProductId",
                Width = 100
            },
            new DataGridViewTextBoxColumn
            {
                HeaderText = "Product Name",
                DataPropertyName = "ProductName",
                Width = 200
            },
            new DataGridViewTextBoxColumn
            {
                HeaderText = "Price",
                DataPropertyName = "Price",
                Width = 100
            },
            new DataGridViewTextBoxColumn
            {
                HeaderText = "Quantity",
                DataPropertyName = "Quantity",
                Width = 100
            }
        });

        // Add event handlers
        btnAdd.Click += BtnAdd_Click;
        btnEdit.Click += BtnEdit_Click;
        btnDelete.Click += BtnDelete_Click;
        btnSearch.Click += BtnSearch_Click;

        // Load initial data
        LoadDummyData();
    }

    private void LoadDummyData()
    {
        dgvProducts.Rows.Add("P001", "SaftMedKraft", "19,99", "10");
        dgvProducts.Rows.Add("P002", "KraftigSaft", "29.99", "50");
        dgvProducts.Rows.Add("P003", "LarsVingborgJuice", "59.99", "30");
    }

    private void BtnAdd_Click(object sender, EventArgs e)
    {
        using (var form = new ProductForm("Add Product")) // Changed title for Add
        {
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (ValidateInput(form))
                {
                    string productId = $"P{dgvProducts.Rows.Count + 1:D3}";
                    dgvProducts.Rows.Add(productId,
                        form.txtProductName.Text,
                        form.txtPrice.Text,
                        form.txtQuantity.Text);
                }
            }
        }
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        if (dgvProducts.SelectedRows.Count == 0) return;

        var row = dgvProducts.SelectedRows[0];
        using (var form = new ProductForm("Edit Product")) // Explicit title for Edit
        {
            form.txtProductName.Text = row.Cells[1].Value.ToString();
            form.txtPrice.Text = row.Cells[2].Value.ToString();
            form.txtQuantity.Text = row.Cells[3].Value.ToString();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (ValidateInput(form))
                {
                    row.Cells[1].Value = form.txtProductName.Text;
                    row.Cells[2].Value = form.txtPrice.Text;
                    row.Cells[3].Value = form.txtQuantity.Text;
                }
            }
        }
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        if (dgvProducts.SelectedRows.Count > 0)
        {
            if (MessageBox.Show("Are you sure you want to delete this product?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dgvProducts.Rows.Remove(dgvProducts.SelectedRows[0]);
            }
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

    private bool ValidateInput(ProductForm form)
    {
        if (string.IsNullOrWhiteSpace(form.txtProductName.Text))
        {
            MessageBox.Show("Please enter a product name.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (!decimal.TryParse(form.txtPrice.Text, out _))
        {
            MessageBox.Show("Please enter a valid price.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        if (!int.TryParse(form.txtQuantity.Text, out _))
        {
            MessageBox.Show("Please enter a valid quantity.", "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        return true;
    }

    private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}
