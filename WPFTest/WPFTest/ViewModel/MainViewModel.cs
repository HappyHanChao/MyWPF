using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WPFTest.Common;
using WPFTest.Model;

namespace WPFTest.ViewModel
{
    public class MainViewModel : NotifyBase
    {
        public UserModel UserInfo { get; set; }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; this.DoNotify(); }
        }

        private FrameworkElement _mainContent;

        public FrameworkElement MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; this.DoNotify(); }
        }

        public CommandBase NavChangedCommand { get; set; }

        public MainViewModel()
        {
            UserInfo = new UserModel();
            //this.NavChangedCommand = new CommandBase(DoNavChanged, (i) => { return true; });

            //DoNavChanged("FirstPageView");
        }

        private void DoNavChanged(object obj)
        {
            Type type = Type.GetType("WPFTest.View." + obj.ToString());
            ConstructorInfo cti = type.GetConstructor(System.Type.EmptyTypes);
            this.MainContent = (FrameworkElement)cti.Invoke(null);
        }
    }
}
