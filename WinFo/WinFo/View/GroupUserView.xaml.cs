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
    /// Interaction logic for GroupUserView.xaml
    /// </summary>
    public partial class GroupUserView : Window
    {
        public GroupUserView()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            UserGrid.Visibility = Visibility.Collapsed;
            GroupGrid.Visibility = Visibility.Collapsed;

            if (e.NewValue is User)
            {
                UserGrid.Visibility = Visibility.Visible;
            }
            else if (e.NewValue is UserGroup)
            {
                GroupGrid.Visibility = Visibility.Visible;
            }

            if (this.DataContext is GroupUserViewModel guvm)
            {
                guvm.SelectedItem = e.NewValue;
            }
        }
    }
}
