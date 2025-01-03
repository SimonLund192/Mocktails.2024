using System.Data;
using Mocktails.ApiClient.Orders;
using Mocktails.Employee.App.Orders;

namespace Mocktails.Employee.App;

public partial class OrdersControl : UserControl
{
    private readonly OrdersApiClient _ordersApiClient;

    public OrdersControl()
    {
        InitializeComponent();
        _ordersApiClient = new OrdersApiClient("https://localhost:7203");
        SetupOrdersControl();
    }

    private void SetupOrdersControl()
    {
        // Setup DataGridView
        dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvOrders.MultiSelect = false;
        dgvOrders.AllowUserToAddRows = false;
        dgvOrders.ReadOnly = true;

        // Configure columns
        dgvOrders.Columns.Clear();
        dgvOrders.Columns.AddRange(new[]
        {
                CreateColumn("Order ID", "Id", 100),
                CreateColumn("Order Date", "OrderDate", 150),
                CreateColumn("Total Amount", "TotalAmount", 100),
                CreateColumn("Status", "Status", 100),
                CreateColumn("Shipping Address", "ShippingAddress", 200),
            });

        // Add event handlers
        btnAdd.Click += BtnAdd_Click;
        btnEdit.Click += BtnEdit_Click;
        btnDelete.Click += BtnDelete_Click;
        btnSearch.Click += BtnSearch_Click;

        LoadOrdersFromApi();
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

    private async void LoadOrdersFromApi()
    {
        try
        {
            var orders = await _ordersApiClient.GetOrdersAsync();
            dgvOrders.Rows.Clear();

            foreach (var order in orders)
            {
                dgvOrders.Rows.Add(
                    order.Id,
                    order.OrderDate,
                    order.TotalAmount.ToString("F2"),
                    order.Status,
                    order.ShippingAddress
                );
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void BtnAdd_Click(object sender, EventArgs e)
    {
        //using var form = new OrderForm("Add Order");
        //if (form.ShowDialog() == DialogResult.OK && ValidateInput(form))
        //{
        //    try
        //    {
        //        var newOrder = new OrderDTO
        //        {
        //            OrderDate = DateTime.Now,
        //            TotalAmount = form.TotalAmount,
        //            Status = form.Status,
        //            ShippingAddress = form.ShippingAddress
        //        };

        //        await _ordersApiClient.CreateOrderAsync(newOrder);
        //        LoadOrdersFromApi();
        //        MessageBox.Show("Order added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Failed to add order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
    }

    private async void BtnEdit_Click(object sender, EventArgs e)
    {
        if (dgvOrders.SelectedRows.Count == 0)
        {
            MessageBox.Show("Please select an order to edit.", "Edit Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedRow = dgvOrders.SelectedRows[0];
        var orderId = selectedRow.Cells[0].Value?.ToString();

        if (!int.TryParse(orderId, out var orderIdInt))
        {
            MessageBox.Show("Invalid order ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            // Fetch the selected order
            var order = await _ordersApiClient.GetOrderByIdAsync(orderIdInt);
            if (order == null)
            {
                MessageBox.Show("Order not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Open the form to edit the order status
            using var form = new OrderForm("Edit Order")
            {
                //txtStatus = { Text = order.Status },
            };

            if (form.ShowDialog() == DialogResult.OK)
            {
                // Update the order status
                //order.Status = form.txtStatus.Text;

                // Ensure status is valid
                if (order.Status != "Pending" && order.Status != "Shipped")
                {
                    MessageBox.Show("Invalid status. Please enter 'Pending' or 'Shipped'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Save the updated order
                await _ordersApiClient.UpdateOrderAsync(order);
                LoadOrdersFromApi();
                MessageBox.Show("Order updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to edit order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void BtnDelete_Click(object sender, EventArgs e)
    {
        //if (dgvOrders.SelectedRows.Count == 0)
        //{
        //    MessageBox.Show("Please select an order to delete.", "Delete Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    return;
        //}

        //var selectedRow = dgvOrders.SelectedRows[0];
        //var orderId = selectedRow.Cells[0].Value?.ToString();

        //if (!int.TryParse(orderId, out var orderIdInt))
        //{
        //    MessageBox.Show("Invalid order ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    return;
        //}

        //var confirmation = MessageBox.Show(
        //    "Are you sure you want to delete this order? This action cannot be undone.",
        //    "Confirm Delete",
        //    MessageBoxButtons.YesNo,
        //    MessageBoxIcon.Warning
        //);

        //if (confirmation != DialogResult.Yes) return;

        //try
        //{
        //    await _ordersApiClient.DeleteOrderAsync(orderIdInt);
        //    LoadOrdersFromApi();
        //    MessageBox.Show("Order deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show($"Failed to delete order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}
    }

    private void BtnSearch_Click(object sender, EventArgs e)
    {
        var searchText = textBox1.Text.ToLower();

        foreach (DataGridViewRow row in dgvOrders.Rows)
        {
            row.Visible = row.Cells.Cast<DataGridViewCell>()
                .Any(cell => cell.Value?.ToString().ToLower().Contains(searchText) == true) ||
                string.IsNullOrWhiteSpace(searchText);
        }
    }

    //private bool ValidateOrderInput(OrderForm form)
    //{
    //    return form.TotalAmount > 0 &&
    //           !string.IsNullOrWhiteSpace(form.Status) &&
    //           !string.IsNullOrWhiteSpace(form.ShippingAddress);
    //}

    private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}
