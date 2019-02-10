using System;
using System.Collections.Generic;
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
using TodoApplication.TodoServiceReference;

namespace TodoApplication
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      Loaded += MainWindow_Loaded;
    }

    void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
      txtUserNm.Text = Environment.UserName;
      txtMachineNm.Text = Environment.MachineName;
    }

    private void btnAlert_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Nicely done!");
    }

    private void btnGet_Click(object sender, RoutedEventArgs e)
    {
      var ItemList = new List<ToDoItem>();
      using (var svc = new ServiceClient())
        ItemList = svc.GetToDoList();
      LbTodoItems.ItemsSource = ItemList;
    }

    private void btnUpdate_Click(object sender, RoutedEventArgs e)
    {
      var selectedItem = LbTodoItems.SelectedItem as ToDoItem ??
        new ToDoItem { Completed = chkCompleted.IsChecked.Value, Id = 2,
        Item = txtItemNote.Text };

      selectedItem.Item = txtItemNote.Text;
      selectedItem.Completed = chkCompleted.IsChecked.Value;

      using (var svc = new ServiceClient())
        svc.UpdateItem(selectedItem);
    }


  }

}
