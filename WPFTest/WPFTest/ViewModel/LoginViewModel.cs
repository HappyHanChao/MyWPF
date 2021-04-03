using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFTest.Common;
using WPFTest.Model;

namespace WPFTest.ViewModel
{
    public class LoginViewModel
    {
        /*创建代码的快捷键
         ctor：快速创建构造函数
         prop：快速创建属性
         propfull：快速创建属性和字段
         而且可以在工具-代码片段管理器中设置这种快捷键
             */
        public CommandBase CloseWindowCommand { get; set; }
        public LoginModel LoginModel { get; set; }

        public LoginViewModel()
        {
            this.LoginModel = new LoginModel();
            this.LoginModel.UserName = "Jack";
            this.LoginModel.Password = "123456";

            CloseWindowCommand = new CommandBase((t) => { (t as Window).Close();  }, (t) => { return true; });
        }
    }
}
