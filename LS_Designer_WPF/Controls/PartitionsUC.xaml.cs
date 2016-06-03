using LS_Designer_WPF.Model;
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
    //public class PartitionTst
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
    /// <summary>
    /// Логика взаимодействия для PartitionsUC.xaml
    /// </summary>
    public partial class PartitionsUC : UserControl
    {

        public PartitionsUC()
        {
            InitializeComponent();
            //ObservableCollection<Partition> list = new ObservableCollection<Partition>();
            //list.Add(new Partition() { Id = 1, Name = "Кухня" });
            //list.Add(new Partition() { Id = 2, Name = "Столовая" });
            //list.Add(new Partition() { Id = 3, Name = "Детская" });
            //list.Add(new Partition() { Id = 4, Name = "Прихожая" });
            //ListItems = list;
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
            Partition x = (Partition)uc.SelectedItem;
            if (x != null)
            {
                uc.objectPanel.Visibility = Visibility.Visible;
            }
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

        #region AddModeDP

        public static readonly DependencyProperty AddModeProperty =
            DependencyProperty.Register("AddMode", typeof(bool), typeof(PartitionsUC), new PropertyMetadata(false));

        public bool AddMode
        {
            get { return (bool)GetValue(AddModeProperty); }
            set { SetValue(AddModeProperty, value); }
        }

        #endregion

        #region CommandsDP

        public static readonly DependencyProperty AddCmdProperty = DependencyProperty.Register(
                                                                    "AddCmd",
                                                                    typeof(ICommand),
                                                                    typeof(PartitionsUC));

        public ICommand AddCmd
        {
            get { return (ICommand)GetValue(AddCmdProperty); }
            set { SetValue(AddCmdProperty, value); }
        }

        public static readonly DependencyProperty SaveCmdProperty = DependencyProperty.Register(
                                                                    "SaveCmd",
                                                                    typeof(ICommand),
                                                                    typeof(PartitionsUC));

        public ICommand SaveCmd
        {
            get { return (ICommand)GetValue(SaveCmdProperty); }
            set { SetValue(SaveCmdProperty, value); }
        }

        #endregion



        #endregion

        /************************************************************************/

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedItem != null)
            {
                EditUI();
                name.Focus();
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            int ix = (ListItems as ObservableCollection<Partition>).IndexOf((Partition)SelectedItem);
            NormalUI();
            if (SaveCmd != null)
            {
                SaveCmd.Execute(null);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            NormalUI();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddUI();
            SelectedItem = null;
            name.Focus();
            AddMode = true;
            if (AddCmd != null)
            {
                AddCmd.Execute(null);
            }
        }

        void NormalUI()
        {
            objectCurtain.Visibility = Visibility.Visible;
            objectButtons.Visibility = Visibility.Collapsed;
            listCurtain.Visibility = Visibility.Collapsed;
            addButton.IsEnabled = true;
        }

        void AddUI()
        {
            objectCurtain.Visibility = Visibility.Collapsed;
            objectButtons.Visibility = Visibility.Visible;
            listCurtain.Visibility = Visibility.Visible;
            objectPanel.Visibility = Visibility.Visible;
            addButton.IsEnabled = false;
        }

        void EditUI()
        {
            objectCurtain.Visibility = Visibility.Collapsed;
            objectButtons.Visibility = Visibility.Visible;
            listCurtain.Visibility = Visibility.Visible;
            addButton.IsEnabled = false;
        }
    }

}
