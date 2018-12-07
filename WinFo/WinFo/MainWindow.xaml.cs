using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinFo.Model.Configuration;
using WinFo.Model.Usage;
using WinFo.Service;
using WinFo.Service.Configuration;
using WinFo.Service.Configuration.Win7;
using WinFo.Service.Usage;
using WinFo.Service.Usage.Win7;
using WinFo.Usage.Model;
using WinFo.ViewModel;

namespace WinFo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            DiskDriveGrid.Visibility = Visibility.Collapsed;
            DiskPartitionGrid.Visibility = Visibility.Collapsed;
            LogicalDiskGrid.Visibility = Visibility.Collapsed;
            if (e.NewValue is DiskDrive)
            {
                DiskDriveGrid.Visibility = Visibility.Visible;
            }else if (e.NewValue is DiskPartition)
            {
                DiskPartitionGrid.Visibility = Visibility.Visible;
            }else if (e.NewValue is LogicalDisk)
            {
                LogicalDiskGrid.Visibility = Visibility.Visible;
            }

            if (this.DataContext is MainViewModel mvm)
            {
                mvm.SelectedItem = e.NewValue;
            }
                
        }
    }
}
