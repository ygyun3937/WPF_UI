using System.Windows;
using System.ComponentModel;
using WPF_UI.Models;
using System.Collections.ObjectModel;
using WPF_UI.Models;
using System.Windows.Input;
using System;
using System.Data;

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

        /// <summary>
        /// Connect Command 선언
        /// </summary>
        /// ICommand : using System.Windows.Input;
        private ICommand connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return (this.connectCommand) ?? (this.connectCommand = new DelegateCommand(Connect));
            }
        }

        private ICommand selectCommand;
        public ICommand SelectCommand
        {
            get
            {
                return (this.selectCommand)??(this.selectCommand = new DelegateCommand(DataSearch))
            }
        }

        private void DataSearch()
        {
            DataSet ds = new DataSet();

            string query = @"";

            SQLDBManager.Instance.ExecuteDsQuery(ds, query);

        }


        /// <summary>
        /// DB Connect
        /// </summary>
        private void Connect()
        {
            //Connect to DB
            if(SQLDBManager.Instance.GetConnection() == false)
            {
                string msg = $"Failed to Connect to Database";
                MessageBox.Show(msg, "Error");
                return;
            }
            else
            {
                string msg = $"Success to Connect to Database";
                MessageBox.Show(msg, "Inform");
            }
        }
    }
}
