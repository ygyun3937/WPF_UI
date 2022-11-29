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

        public string ADD_NAME
        {
            get
            {
                return viewModelMySql.ADD_NAME;
            }
            set
            {
                viewModelMySql.ADD_NAME = value;
                OnPropertyChanged("ADD_NAME");

            }
        }

        public string ADD_AGE
        {
            get
            {
                return viewModelMySql.ADD_AGE;
            }
            set
            {
                viewModelMySql.ADD_AGE = value;
                OnPropertyChanged("ADD_AGE");

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
        
        private ICommand insertCommand;
        public ICommand InsertCommand
        {
            get
            {
                return (this.insertCommand) ?? (this.insertCommand = new DelegateCommand(InsertEvent));
            }

        }

        private void InsertEvent()
        {
            DataSet ds = new DataSet();            

            string query = string.Format("insert into test_table(NAME,AGE) values('{0}',{1})",ADD_NAME,ADD_AGE);

            SQLDBManager.Instance.ExecuteDsQuery(ds, query);

            
            SampleViewMySqls.Clear();


            query = "select NAME,AGE from test_table";

            SQLDBManager.Instance.ExecuteDsQuery(ds, query);

            for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
            {
                ViewModelMySql obj = new ViewModelMySql();
                obj.NAME = ds.Tables[0].Rows[idx]["NAME"].ToString();
                obj.AGE = ds.Tables[0].Rows[idx]["AGE"].ToString();

                SampleViewMySqls.Add(obj);
            }

        }
        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return (this.deleteCommand) ?? (this.deleteCommand = new DelegateCommand(DeleteEvent));
            }

        }

        private void DeleteEvent()
        {
            DataSet ds = new DataSet();

            string query = string.Format("delete from test_table");

            SQLDBManager.Instance.ExecuteDsQuery(ds, query);
        }
        private void DataSearch()
        {
            SampleViewMySqls.Clear();

            DataSet ds = new DataSet();

            string query = "select NAME,AGE from test_table";

            SQLDBManager.Instance.ExecuteDsQuery(ds, query);

            for (int idx = 0;idx<ds.Tables[0].Rows.Count;idx++)
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
