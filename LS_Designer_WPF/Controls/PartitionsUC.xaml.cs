using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace LS_Designer_WPF.Controls
{
    public class PartitionTst
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для PartitionsUC.xaml
    /// </summary>
    public partial class PartitionsUC : UserControl
    {

        public PartitionsUC()
        {
            InitializeComponent();
            ObservableCollection<PartitionTst> list = new ObservableCollection<PartitionTst>();
            list.Add(new PartitionTst() { Id = 1, Name = "Кухня" });
            list.Add(new PartitionTst() { Id = 2, Name = "Столовая" });
            list.Add(new PartitionTst() { Id = 3, Name = "Детская" });
            list.Add(new PartitionTst() { Id = 4, Name = "Прихожая" });
            ListItems = list;
        }

        /************************************************************************/

        #region DP Properties

        #region ListItemsDP

        public static readonly DependencyProperty ListItemsProperty =
            DependencyProperty.Register("ListItems", typeof(Object), typeof(PartitionsUC), new PropertyMetadata(null, OnListItemsChanged));

        public Object ListItems
        {
            get { return GetValue(ListItemsProperty); }
            set { SetValue(ListItemsProperty, value); }
        }

        private static void OnListItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PartitionsUC uc = (PartitionsUC)d;
            if (uc.ListItems != null)
            {
                uc.objectPanel.Visibility = Visibility.Collapsed;
                uc.addButton.IsEnabled = true;
            }
            //ms.UpdateModel();
        }

        #endregion

        #region SelectedItemDP

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(Object), typeof(PartitionsUC), new PropertyMetadata(null, OnSelectedItemChanged));

        public Object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PartitionsUC uc = (PartitionsUC)d;
            PartitionTst x = (PartitionTst)uc.SelectedItem;
            if (x != null)
            {
                PartitionTst p = new PartitionTst() { Id = x.Id, Name = x.Name };
                uc.objectPanel.Visibility = Visibility.Visible;
                uc.CurrentObject = p;
            }
            //ms.UpdateModel();
        }

        #endregion

        #region CurrentObjectDP

        public static readonly DependencyProperty CurrentObjectProperty =
            DependencyProperty.Register("CurrentObject", typeof(Object), typeof(PartitionsUC), new PropertyMetadata(null, OnCurrentObjectChanged));

        public Object CurrentObject
        {
            get { return GetValue(CurrentObjectProperty); }
            set { SetValue(CurrentObjectProperty, value); }
        }

        private static void OnCurrentObjectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PartitionsUC uc = (PartitionsUC)d;
            var x = uc.CurrentObject;
            //ms.UpdateModel();
        }

        #endregion

        #endregion

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedItem != null)
            {
                objectCurtain.Visibility = Visibility.Collapsed;
                objectButtons.Visibility = Visibility.Visible;
                listCurtain.Visibility = Visibility.Visible;
                addButton.IsEnabled = false;
                name.Focus();
            }
        }



        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            int ix = (ListItems as ObservableCollection<PartitionTst>).IndexOf((PartitionTst)SelectedItem);
            (ListItems as ObservableCollection<PartitionTst>)[ix] = (PartitionTst)CurrentObject;
            SelectedItem = CurrentObject;
            objectCurtain.Visibility = Visibility.Visible;
            objectButtons.Visibility = Visibility.Collapsed;
            listCurtain.Visibility = Visibility.Collapsed;
            addButton.IsEnabled = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            objectCurtain.Visibility = Visibility.Visible;
            objectButtons.Visibility = Visibility.Collapsed;
            listCurtain.Visibility = Visibility.Collapsed;
            addButton.IsEnabled = true;
        }
    }

}
