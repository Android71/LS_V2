using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;
using System.Windows.Input;

namespace LS_Designer_WPF.Controls
{

    public partial class PartitionsUC : UserControl
    {

        public PartitionsUC()
        {
            InitializeComponent();
            Messenger.Default.Register<string>(this, "PartitionFocus", DoFocus);
        }

        void DoFocus(string msg)
        {
            if (msg == "focus")
            {
                nameTb.Focus();
                nameTb.CaretIndex = nameTb.Text.Length;
            }
        }
    }

}
