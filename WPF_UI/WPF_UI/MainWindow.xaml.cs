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
    }
}
