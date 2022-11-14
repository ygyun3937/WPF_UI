
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.IconPacks;

namespace WPF_UI.UserControls
{
    /// <summary>
    /// MenuButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MenuButton : UserControl
    {
        public MenuButton()
        {
            InitializeComponent();
        }

        public PackIconMaterialKind Icon
        {
            get { return (PackIconMaterialKind)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }


        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(PackIconMaterialKind), typeof(MenuButton));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }


        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(MenuButton));




    }
}
