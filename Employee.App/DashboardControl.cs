using Mocktails.ApiClient.Orders;
using Mocktails.ApiClient.Orders.DTOs;
using Mocktails.ApiClient.Products;

namespace Employee.App;

public partial class DashboardControl : UserControl
{

    private readonly MocktailsApiClient _mocktailsApiClient;
    private readonly OrdersApiClient _ordersApiClient;

    public DashboardControl()
    {
        InitializeComponent();
        _mocktailsApiClient = new MocktailsApiClient("https://localhost:7203");
        _ordersApiClient = new OrdersApiClient("https://localhost:7203");
        SetupDashboard();
    }

    // Method to setup the Dashboard
    private async void SetupDashboard()
    {
        try
        {
            // Fetch data from API
            var orders = await _ordersApiClient.GetOrdersAsync();

            // Calculate statistics
            int totalOrders = orders.Count();
            int pendingShipments = orders.Count(o => o.Status == "Pending");
            int shippedOrders = orders.Count(o => o.Status == "Shipped");

            // Configure panels with real data
            ConfigureStatPanel(panel1, "Total Orders", totalOrders.ToString(), Color.LightSeaGreen);
            ConfigureStatPanel(panel2, "Shipped Orders", shippedOrders.ToString(), Color.LightSkyBlue);
            ConfigureStatPanel(panel3, "Pending Shipments", pendingShipments.ToString(), Color.LightCoral);

            // Set up DataGridView for recent orders
            SetupRecentOrdersGrid(orders);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to load dashboard data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // Method to configure a statistics panel
    private void ConfigureStatPanel(Panel panel, string title, string value, Color color)
    {
        panel.BackColor = color;
        panel.Padding = new Padding(10);

        // Create title label
        Label lblTitle = new Label
        {
            Text = title,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.White,
            Height = 20
        };

        // Create value label
        Label lblValue = new Label
        {
            Text = value,
            Font = new Font("Segoe UI", 16, FontStyle.Bold),
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.White
        };

        // Clear existing controls and add new ones
        panel.Controls.Clear();
        panel.Controls.Add(lblValue);
        panel.Controls.Add(lblTitle);
    }

    // Method to setup the DataGridView with real data
    private void SetupRecentOrdersGrid(IEnumerable<OrderDTO> orders)
    {
        dgvRecentOrders.Columns.Clear();
        dgvRecentOrders.AutoGenerateColumns = false;
        dgvRecentOrders.AllowUserToAddRows = false;
        dgvRecentOrders.ReadOnly = true;

        // Configure columns
        dgvRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Order ID",
            DataPropertyName = "Id",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        dgvRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Customer",
            DataPropertyName = "Customer",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        dgvRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Status",
            DataPropertyName = "Status",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });

        // Populate DataGridView with real data
        dgvRecentOrders.Rows.Clear();
        foreach (var order in orders.Take(10)) // Limit to the 10 most recent orders
        {
            dgvRecentOrders.Rows.Add(order.Id, order.UserId, order.Status);
        }

        // Apply styling
        dgvRecentOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvRecentOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        dgvRecentOrders.DefaultCellStyle.Font = new Font("Segoe UI", 9);
        dgvRecentOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    }

    private void panel1_Paint(object sender, PaintEventArgs e) { }
    private void flowStats_Paint(object sender, PaintEventArgs e) { }
    private void dgvRecentOrders_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
}
