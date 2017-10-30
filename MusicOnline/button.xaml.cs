using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace MusicOnline
{
    public sealed partial class button : UserControl
    {

        public button()
        {
            this.InitializeComponent();

        }
        public void setbtn_content(String name)
        {
            btn_Content.Content = name;
        }
        public delegate void OnButClick();
        public event OnButClick btnHandle;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (btnHandle != null)
                btnHandle();

        }
    }
}
