using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFTest.Common
{
    public class PasswordHelper
    {
        /*
         由于Password标签无法直接Binding后台类
         可以采用两种方式去间接的Binding数据
         1. 采用WPF用户控件的方式
         2. 采用附加类的方式
            密码框-附加类-Model
            附加类在密码框和Model之间起到一个桥梁的作用，将Model中的某个属性与附加类中的某个依赖属性绑定。
            附加类中的某个依赖属性变化的时候会触发一个委托通知到密码框。
            密码框中的数据发生变化的时候也会通知到附加类，附加类再改变Model中的值
         这边主要是第二种方式的实现
         */
        #region 1.  这个依赖属性是为了绑定密码框，当值改变时主动通知到密码框
        public static readonly DependencyProperty passwordProperty =
           DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordHelper), new
               FrameworkPropertyMetadata("", new PropertyChangedCallback(OnPropertyChanged)));

        //下面创建两个方法，对PasswordProperty这个依赖属性进行封装
        public static string GetPassword(DependencyObject d)
        {
            return d.GetValue(passwordProperty).ToString();
        }

        public static void SetPassword(DependencyObject d,string value)
        {
            d.SetValue(passwordProperty, value);
        }

        //这个方法的作用是当依赖属性的值发生变化的时候会主动推送到Password的密码框中去
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox password = d as PasswordBox;
            password.PasswordChanged -= Password_PasswrodChanged;
            if (!isUpdating)
            {
                password.Password = e.NewValue?.ToString();
            }
            password.PasswordChanged += Password_PasswrodChanged;

        }
        #endregion

        #region 2.  这个依赖属性是为了密码框绑定到依赖属性，当界面的值改变的时候通知到改依赖属性
        private static bool isUpdating=false;

        public static readonly DependencyProperty attachProperty =
          DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordHelper), new
              FrameworkPropertyMetadata(default(bool), new PropertyChangedCallback(OnAttached)));

        public static bool GetAttach(DependencyObject d)
        {
            return (bool)d.GetValue(attachProperty);
        }

        public static void SetAttach(DependencyObject d, bool value)
        {
            d.SetValue(attachProperty, value);
        }

        private static void OnAttached(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox password = d as PasswordBox;
            password.PasswordChanged+=Password_PasswrodChanged;
        }

        private static void Password_PasswrodChanged(object sender,RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            isUpdating = true;
            SetPassword(passwordBox, passwordBox.Password);
            isUpdating = false;
        }
        #endregion
    }
}
