using Mocktails.ApiClient.Products;

namespace Employee.App;

public partial class DashboardControl : UserControl
{

    private readonly MocktailsApiClient _mocktailsApiClient;

    public DashboardControl()
    {
        InitializeComponent();
        _mocktailsApiClient = new MocktailsApiClient("https://localhost:7203");
        SetupDashboard();
    }

    // Method to setup the Dashboard
    private void SetupDashboard()
    {
        // Add panels for statistics with meaningful data
        ConfigureStatPanel(panel1, "Total Orders", "150", Color.LightSeaGreen);
        ConfigureStatPanel(panel2, "Total Products", "45", Color.LightSkyBlue);
        ConfigureStatPanel(panel3, "Pending Shipments", "30", Color.LightCoral);

        // Set up DataGridView for recent orders
        SetupRecentOrdersGrid();
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

    // Method to setup the DataGridView
    private void SetupRecentOrdersGrid()
    {
        dgvRecentOrders.Columns.Clear();
        dgvRecentOrders.AutoGenerateColumns = false;
        dgvRecentOrders.AllowUserToAddRows = false;
        dgvRecentOrders.ReadOnly = true;

        // Configure columns
        dgvRecentOrders.Columns.Add(new DataGridViewTextBoxColumn
        {
            HeaderText = "Order ID",
            DataPropertyName = "OrderId",
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

        // Add some dummy data
        dgvRecentOrders.Rows.Add("1024", "Bobby", "Shipped");
        dgvRecentOrders.Rows.Add("1025", "Lars", "Pending");
        dgvRecentOrders.Rows.Add("1026", "Karsten", "Delivered");

        // Apply styling
        dgvRecentOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvRecentOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        dgvRecentOrders.DefaultCellStyle.Font = new Font("Segoe UI", 9);
        dgvRecentOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void flowStats_Paint(object sender, PaintEventArgs e)
    {

    }
}
