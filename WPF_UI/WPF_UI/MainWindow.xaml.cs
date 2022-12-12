using System.Windows;
using System.Windows.Input;
using WPF_UI.ViewModel;

namespace WPF_UI
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDataDirty = false;

        public MainWindow()
        {
            InitializeComponent();

            //ViewMySql _viewMySql = new ViewMySql(this);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                
                this.DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            //uiGrid_Main.ItemsSource = ViewModel.ViewMySql.Equals().GEt
        }

        private void Exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
           // MessageBox.Show("Closing called");


            string msg = "Data is dirty. Close without saving?";
            MessageBoxResult result =
              MessageBox.Show(
                msg,
                "Data App",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Closing called");

            // If data is dirty, notify user and ask for a response
            if (this.isDataDirty)
            {
                string msg = "Data is dirty. Close without saving?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "Data App",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    // If user doesn't want to close, cancel closure
                    e.Cancel = true;
                }
            }
        }

      
    }
}
