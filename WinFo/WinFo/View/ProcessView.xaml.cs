using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WinFo.Model.Usage;
using WinFo.ViewModel;

namespace WinFo.View
{
    /// <summary>
    /// Interaction logic for ProcessView.xaml
    /// </summary>
    public partial class ProcessView : Window
    {
        public ProcessView()
        {
            InitializeComponent();
        }

        //TO-DO make it mvvm acceptable by tracking IsSelected 
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(this.DataContext is ProcessTreeViewModel ptvm && e.NewValue is Process proc)
            {
                ptvm.SelectedProcess = proc;
            }
        }
    }
}
