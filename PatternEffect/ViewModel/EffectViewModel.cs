using GalaSoft.MvvmLight;
using LS_Designer_WPF.Controls;
using PatternEffect.Model;
using System.Collections.ObjectModel;

namespace PatternEffect.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class EffectViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        
        public EffectViewModel(IDataService dataService)
        {
            _dataService = dataService;
            SliderItem si = new SliderItem();
            si.Minimum = 0;
            si.Maximum = 10;
            si.SmallChange = 1;
            si.LargeChange = 1;
            si.SelectionEnd = 8;
            si.SelectionStart = 3;
            si.Value = 5;
            
            SliderItems.Add(si);
        }

        ObservableCollection<SliderItem> _sliderItems = new ObservableCollection<SliderItem>();
        public ObservableCollection<SliderItem> SliderItems
        {
            get { return _sliderItems; }
            set { Set(ref _sliderItems, value); }
        }
    }
}