using System.Windows;
using System.ComponentModel;
using WPF_UI.Models;
using System.Collections.ObjectModel;

namespace WPF_UI.ViewModel
{
    /// <summary>
    /// ViewMySql.xaml에 대한 상호 작용 논리
    /// </summary>
    public class ViewMySql : INotifyPropertyChanged
    {
        ViewModelMySql viewModelMySql = new ViewModelMySql();

        public string NAME
        {
            get
            {
                return viewModelMySql.NAME;
            }
            set
            {
                viewModelMySql.NAME = value;
                OnPropertyChanged("NAME");
            }
        }
        public string AGE
        {
            get
            {
                return viewModelMySql.AGE;
            }
            set
            {
                viewModelMySql.AGE = value;
                OnPropertyChanged("AGE");
            }
        }
        ObservableCollection<ViewMySql> _SameViewMySqls = null;
        public ObservableCollection<ViewMySql> SampleViewMySqls
        {
            get
            {
                if(_SameViewMySqls == null)
                {
                    _SameViewMySqls = new ObservableCollection<ViewMySql>();
                }
                return _SameViewMySqls;
            }
            set
            {
                _SameViewMySqls = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
