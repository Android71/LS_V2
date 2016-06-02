using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LS_Designer_WPF.Model;

namespace LS_Designer_WPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            AppContext.DataSvc = dataService;

            //Messenger.Default.Register<NotificationMessage>(this, AppContext.BlockChangeContextMsg, BlockContext);
            //Messenger.Default.Register<NotificationMessage>(this, AppContext.UnBlockChangeContextMsg, UnBlockContext);
            //Messenger.Default.Register<NotificationMessage>(this, AppContext.ShowPopUpMsg, ShowPopUp);

            MessageVM = new EmptyPopUp();

            ControlSpacesVM = new ControlSpacesVM(dataService);
            PartitionsVM = new PartitionsVM(dataService);
            //ControlDeviceVM = new ControlDeviceVM(dataService);
            //EventDevicesVM = new EventDevicesVM(dataService);
            //LightElementsVM = new LightElementsVM(dataService);
            //LightZonesVM = new LightZonesVM(dataService);


            //TabItems.Add(PartitionVM);
            //TabItems.Add(ControlSpaceVM);
            //TabItems.Add(ControlDeviceVM);
            //TabItems.Add(EventDevicesVM);
            //TabItems.Add(LightElementsVM);
            //TabItems.Add(LightZonesVM);
        }

        public PartitionsVM PartitionsVM { get; private set; }

        public ControlSpacesVM ControlSpacesVM { get; private set; }

        object _messageVM = null;
        public object MessageVM { get { return _messageVM; } set { Set(ref _messageVM, value); } }
    }

    class EmptyPopUp
    {
        public EmptyPopUp()
        {
            ShowMessage = false;
        }

        //string _message = "Simple message";
        public string Message { get; set; }      //{ get { return _message; } set { Set(ref _message, value); } }

        //bool _showMessage = false;
        public bool ShowMessage { get; set; }    //{ get { return _showMessage; } set { Set(ref _showMessage, value); } }

        public RelayCommand CloseCommand
        {
            get;
            private set;
        }
    }
}