using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.WPF.Common
{
    public class ChooseTaskManager: DependencyObject
    {

        public static int GetMainNodeID(DependencyObject obj)
        {
            return (int)obj.GetValue(MainNodeIDProperty);
        }

        public static void SetMainNodeID(DependencyObject obj, int value)
        {
            obj.SetValue(MainNodeIDProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainNodeIDProperty =
            DependencyProperty.RegisterAttached("MainNodeID", typeof(int), typeof(ChooseTaskManager), new PropertyMetadata(0,OnMainNodeIDChanged));

        private static void OnMainNodeIDChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Button button = d as Button;

        }
    }
}
