using GalaSoft.MvvmLight.Messaging;
using LS_Designer_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.Model
{
    public class LightStrip : LightElement
    {
        //public static List<string> csRGB = 
        public LightStrip()
        {
            CanDimming = true;
            //Direction = Direction.Up;
        }

        internal int oldPointCount;

        int _pointCount = 170;
        public int PointCount { get { return _pointCount; } set { Set(ref _pointCount, value); } }

        Direction _direction = Direction.Up;
        public Direction Direction { get { return _direction; } set { Set(ref _direction, value); } }

        string _colorSequenceEnum = "RGB";
        public string ColorSequence { get { return _colorSequenceEnum; } set { Set(ref _colorSequenceEnum, value); } }


        // DesignTime property
        List<string> _colorSequenceList = null;
        public List<string> ColorSequenceList { get { return _colorSequenceList; } set { Set(ref _colorSequenceList, value); } }

        public override PointTypeEnum PointType
        {
            get
            {
                return base.PointType;
            }

            set
            {
                base.PointType = value;
            }
        }


        int StartDMX
        {
            get { return (StartPoint - 1) * AppContext.CountByType[PointType] + 1; }
        }

        int EndDMX
        {
            get { return StartDMX + PointCount * AppContext.CountByType[PointType] + 1; }
        }
         
        public override bool Validate()
        {
            int maxCount = 512 / AppContext.CountByType[PointType];
            StringBuilder sb = new StringBuilder();
            //info = new PopUpMessageVM("");
            bool result = true;
            bool rule1 = true, rule2 = true, rule3 = true;

            // Validade fields

            if (StartPoint < 1)
            {
                sb.AppendLine("StartPoint не может быть меньше 1");
                rule1 = false;
            }

            if (PointCount < 1)
            {
                sb.AppendLine("PointCount не может быть меньше 1");
                rule2 = false;
            }

            if (rule1 & rule2)
            {
                int tmp = (StartPoint - 1) * AppContext.CountByType[PointType] + PointCount * AppContext.CountByType[PointType];
                if (tmp > 512)
                {
                    sb.AppendLine("LightStrip выходит за границы Universe");
                    sb.AppendLine("Измените либо StartPoint либо PointCount");
                    rule3 = false;
                }
            }

            result = rule1 & rule2 & rule3;

            if (!result)
            {
                PopUpMessageVM info = new PopUpMessageVM(sb.ToString());
                Messenger.Default.Send(new NotificationMessage(info, ""), AppContext.ShowPopUpMsg);
            }

            return result;
        }

        public override bool IsDirty
        {
            get
            {
                return StartPoint != oldStartPoint || PointCount != oldPointCount;
            }
        }
    }
}
