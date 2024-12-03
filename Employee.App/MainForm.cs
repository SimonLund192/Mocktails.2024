namespace Employee.App;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        LoadView(new DashboardControl());
    }

    // Method to load a new UserControl
    private void LoadView(UserControl control)
    {
        mainPanel.Controls.Clear();
        control.Dock = DockStyle.Fill;
        mainPanel.Controls.Add(control);
    }

    private void btnDashboard_Click(object sender, EventArgs e)
    {
        LoadView(new DashboardControl());
    }

    private void btnProducts_Click(object sender, EventArgs e)
    {
        LoadView(new ProductsControl());
    }

    private void btnOrders_Click(object sender, EventArgs e)
    {
        //LoadView(new OrdersControl());
    }

    private void mainPanel_Paint(object sender, PaintEventArgs e)
    {

    }
}
