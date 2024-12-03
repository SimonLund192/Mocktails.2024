using Mocktails.ApiClient.Orders;

namespace Employee.App;

public partial class OrdersControl : UserControl
{

    private readonly IOrdersApiClient _ordersApiClient;

    public OrdersControl()
    {
        InitializeComponent();
        _ordersApiClient = new OrdersApiClient("https://localhost:7203");
        //SetupOrdersControl();
    }

    private void OrdersControl_Load(object sender, EventArgs e)
    {

    }

    private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}
