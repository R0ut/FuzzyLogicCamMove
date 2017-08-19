using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_INZ
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    [Export(typeof(Shell))]
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();
        }
    }
}
