using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPFTest.Common;
using WPFTest.DataAccess;
using WPFTest.Model;

namespace WPFTest.ViewModel
{
    public class LoginViewModel:NotifyBase
    {
        /*创建代码的快捷键
         ctor：快速创建构造函数
         prop：快速创建属性
         propfull：快速创建属性和字段
         而且可以在工具-代码片段管理器中设置这种快捷键
             */
        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase LoginCommand { get; set; }
        public LoginModel LoginModel { get; set; } = new LoginModel();
        private string errorMessage { get; set; }
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                DoNotify();
            }
        }

        private Visibility _showProgress = Visibility.Collapsed;
        public Visibility ShowProgress
        {
            get { return _showProgress; }
            set
            {
                _showProgress = value; this.DoNotify();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public LoginViewModel()
        {

            CloseWindowCommand = new CommandBase((i) => { (i as Window).Close(); }, (i) => { return true; });

            LoginCommand = new CommandBase(LoginDoExecute, (i) => { return ShowProgress == Visibility.Collapsed; });
        }

        private void LoginDoExecute(object o)
        {
            this.ShowProgress = Visibility.Visible;
            ErrorMessage = "";
            if (string.IsNullOrEmpty(LoginModel.UserName)||string.IsNullOrEmpty(LoginModel.Password))
            {
                ErrorMessage = "请输入用户名或者密码！";
                this.ShowProgress = Visibility.Collapsed;
                return; 
            }
            if (string.IsNullOrEmpty(LoginModel.ValidationCode))
            {
                ErrorMessage = "请输入验证码！";
                this.ShowProgress = Visibility.Collapsed;
                return;
            }

            Task.Run(new Action(() =>
            {
                try
                {
                    var user = LocalDataAccess.GetInstance().CheckUserInfo(LoginModel.UserName, LoginModel.Password);
                    if (user == null)
                    {
                        throw new Exception("登录失败！用户名或密码错误！");
                    }

                    GlobalValues.UserInfo = user;

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        (o as Window).DialogResult = true;
                    }));
                }
                catch (Exception ex)
                {
                    this.ErrorMessage = ex.Message;
                }
                finally
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        this.ShowProgress = Visibility.Collapsed;
                    }));
                }
            }));
        }
    }
}
