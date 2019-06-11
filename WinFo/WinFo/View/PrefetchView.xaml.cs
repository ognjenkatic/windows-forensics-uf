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
    /// Interaction logic for PrefetchView.xaml
    /// </summary>
    public partial class PrefetchView : Window
    {
        public PrefetchView()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                if (e.AddedItems[0] is PrefetchEntry entry && this.DataContext is PrefetchViewModel pfvm)
                {
                    pfvm.SelectedEntry = entry;
                }
            }
        }
    }
}
