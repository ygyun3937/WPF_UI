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
        ObservableCollection<ViewModelMySql> _SameViewMySqls = null;
        public ObservableCollection<ViewModelMySql> SampleViewMySqls
        {
            get
            {
                if(_SameViewMySqls == null)
                {
                    _SameViewMySqls = new ObservableCollection<ViewModelMySql>();
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
                return (this.selectCommand) ?? (this.selectCommand = new DelegateCommand(DataSearch));
            }
        }

        private ICommand loadedCommand;
        public ICommand LoadedCommand
        {
            get
            {
                return (this.loadedCommand) ?? (this.loadedCommand = new DelegateCommand(LoadEvent));
            }

        }

        private void LoadEvent()
        {
            if(SQLDBManager.Instance.GetConnection() == false)
            {
                string msg = $"Failed to Connect to Database";
                MessageBox.Show(msg, "Error");
            }
            else
            {
                string msg = $"Success to Connect to Database";
                MessageBox.Show(msg, "Inform");
            }

        }

        private void DataSearch()
        {
            SampleViewMySqls.Clear();
            DataSet ds = new DataSet();

            string query =
                @"SELECT '윤영곤' AS NAME, '30' AS AGE UNION ALL
                  SELECT '오주석' AS NAME, '30' AS AGE UNION ALL
                  SELECT '강준모' AS NAME, '30' AS AGE UNION ALL
                  SELECT '조현우' AS NAME, '32' AS AGE UNION ALL
                  SELECT '김수민' AS NAME, '31' AS AGE UNION ALL
                  SELECT '정민우' AS NAME, '29' AS AGE ";

            SQLDBManager.Instance.ExecuteDsQuery(ds, query);


           for(int idx = 0;idx<ds.Tables[0].Rows.Count;idx++)
            {
                ViewModelMySql obj = new ViewModelMySql();
                obj.NAME = ds.Tables[0].Rows[idx]["NAME"].ToString();
                obj.AGE = ds.Tables[0].Rows[idx]["AGE"].ToString();

                SampleViewMySqls.Add(obj);
            }

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
