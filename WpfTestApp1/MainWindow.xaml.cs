using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTestApp1
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();


      string[] products = new[] {"AAPL", "MSFT", "Sony", "Infy", "CTS"};

      StagedDataViewModel viewModel = new StagedDataViewModel();
      OrderCollectionViewModel orderCollection = new OrderCollectionViewModel();
      orderCollection.Header = "Imported Orders";
      Random r = new Random();
      for (int i = 0; i < 100; i++)
      {
        orderCollection.Orders.Add(new OrderRowItem()
        {
          OrderId = Guid.NewGuid().ToString(),
          Product = products[r.Next(0,4)],
          Quantity = r.NextDouble() * 100d,
          Side = r.Next() % 2 == 0 ? "Buy" : "Sell"
        });
      }
      viewModel.StagedData.Add(orderCollection);


      OrderCollectionViewModel orderCollection2 = new OrderCollectionViewModel();
      orderCollection2.Header = "Failed Orders";
      for (int i = 0; i < 10; i++)
      {
        orderCollection2.Orders.Add(new OrderRowItem()
        {
          OrderId = Guid.NewGuid().ToString(),
          Product = products[r.Next(0, 4)],
          Quantity = r.NextDouble() * 100d,
          Side = r.Next() % 2 == 0 ? "Buy" : "Sell"
        });
      }
      viewModel.StagedData.Add(orderCollection2);


      this.DataContext = viewModel;
    }
  }

  public class StagedDataViewModel
  {
    public List<OrderCollectionViewModel> StagedData { get; } = new List<OrderCollectionViewModel>();
  }


  public class OrderCollectionViewModel
  {
    public string Header { get; set; }
    public ObservableCollection<OrderRowItem> Orders { get; } = new ObservableCollection<OrderRowItem>();
  }

  public class OrderRowItem
  {
    public string OrderId { get; set; }
    public double Quantity { get; set; }
    public string Product { get; set; }
    public string Side { get; set; }
  }
}
