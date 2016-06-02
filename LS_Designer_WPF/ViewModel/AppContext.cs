using LS_Designer_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.ViewModel
{
    public class AppContext
    {
        //public static Partition;
        public static ControlSpace ControlSpace = null;
        public static Partition Partition = null;

        public static Guid ChanngeContextMsg = Guid.NewGuid();
        public static Guid BlockChangeContextMsg = Guid.NewGuid();
        public static Guid UnBlockChangeContextMsg = Guid.NewGuid();
        public static Guid ShowPopUpMsg = Guid.NewGuid();

        public static Dictionary<PointTypeEnum, int> CountByType = new Dictionary<PointTypeEnum, int>()
        {
            { PointTypeEnum.W, 1 }, { PointTypeEnum.WT, 2 }, { PointTypeEnum.CW, 2 },  { PointTypeEnum.RGB, 3 },
          { PointTypeEnum.RGBW, 4 }, {PointTypeEnum.RGBWT, 5 }, { PointTypeEnum.RGBCW, 5 }
        };

        public static IDataService DataSvc = null;
    }
}
