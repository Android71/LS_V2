using GalaSoft.MvvmLight;
//using GalaSoft.MvvmLight.Messaging;
//using LS_Designer_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LS_Designer_WPF.Model
{
    public class ArtNetControlChannel : ControlChannel
    {
        public ArtNetControlChannel()
        {
            Name = "Universe";
            //LS_Assignmets = Array.ConvertAll(LS_Assignmets, b => b = 0);
            ConflictList = new List<Conflict>();
        }

        IPAddress _ipAddress;
        public IPAddress IPAddress { get { return _ipAddress; } set { Set(ref _ipAddress, value); } }

        int _portNo;
        public int PortNo { get { return _portNo; } set { Set(ref _portNo, value); } }

        StringBuilder sb = new StringBuilder();

        public byte[] DMXdata = new byte[512];

        // массив для определения занятости при операции назначения LightElement и ControlChannel
        // содержит LightElement.Id
        public byte[] LS_Assignmets = null;

        //List<LightElement> oldLE_List = new List<LightElement>();

        //List<Conflict> _conflictList = null;
        public List<Conflict> ConflictList { get; set; }//{ get { return _conflictList; } set { Set(ref _conflictList, value); } }


        public override void Link(LightElement le)
        {
            base.Link(le);
            //LinkErrorVM info = null;
            //bool result = CheckIntersect(le, out info);
            //if (result)
            //{
            //    AppContext.DataSvc.AddLinkToLightElement(this, le);
            //    return true;
            //}
            //else
            //{
            //    Messenger.Default.Send(new NotificationMessage(info, ""), AppContext.ShowPopUpMsg);
            //    return false;
            //}

        }

        // Это все рассмотреть в LightZone!!!!!
        //protected override bool CheckIntersect(LightElement le, out LinkErrorVM info)
        //{
        //    //bool ok = true;
        //    bool rule1 = true;
        //    bool rule2 = true;
        //    bool rule3 = true;
        //    LightStrip ls = le as LightStrip;
        //    sb.Clear();
        //    info = null;

        //    // Validade fields
        //    if (ls.StartPoint < 1 || ls.StartPoint > 170)
        //    {
        //        sb.AppendLine("Ошибка задания StartPoint");
        //        rule1 = false;
        //    }

        //    if (ls.PointCount < 1 || ls.PointCount > 170)
        //    {
        //        sb.AppendLine("Ошибка задания PointCount");
        //        rule2 = false;
        //    }

        //    // validate 
        //    rule3 = TestIntersection(le as LightStrip);
        //    if (!rule3)
        //        sb.AppendLine("Пересечение с другими элементами");

        //    if (rule1 & rule2 & rule3)
        //        return true;
        //    else
        //    {
        //        info = new LinkErrorVM(sb.ToString());
        //        info.ConflictList = ConflictList;
        //        return false;
        //    }
        //}

        //bool TestIntersection(LightStrip le)
        //{
        //    int dmxCh = 0;
        //    int tmp1 = 0;
        //    int tmp2 = 0;
        //    int oldPointNo = 0;

        //    LightStrip oldLs = null;
        //    ConflictList.Clear();
        //    Dictionary<int, LightStrip> oldLs_List = new Dictionary<int, LightStrip>();
        //    byte[] backUp = null;


        //    // сохраняем предыдущее состояние
        //    if (LS_Assignmets != null)
        //    {
        //        backUp = new byte[512];

        //        for (int i = 0; i < 512; i++)
        //        {
        //            backUp[i] = LS_Assignmets[i];
        //        }
        //    }

        //    // определяем какие LightStrip уже размещены в данной Universe
        //    // формируем oldLS_List
        //    if (LS_Assignmets != null)
        //    {
        //        for (int i = 0; i < 512; i++)
        //        {
        //            if (LS_Assignmets[i] != 0)
        //            {
        //                if (!oldLs_List.ContainsKey(LS_Assignmets[i]))
        //                {
        //                    oldLs = AppContext.DataSvc.GetLightElement(LS_Assignmets[i]) as LightStrip;
        //                    oldLs_List.Add(LS_Assignmets[i], oldLs);
        //                }
        //            }
        //        }
        //    }

        //    // размещаем LightStrip в Universe
        //    // формируем ConflictList
        //    if (oldLs_List.Count != 0) 
        //    {
        //        for (int i = le.StartPoint; i < le.StartPoint + le.PointCount; i++)
        //        {
        //            oldPointNo = 0;
        //            tmp1 = AppContext.CountByType[le.PointType];
        //            for (int j = 1; j <= tmp1; j++)
        //            {
        //                dmxCh = (i - 1) * AppContext.CountByType[le.PointType] + j;

                        

        //                if (LS_Assignmets[dmxCh] != 0)
        //                {
        //                    LightElement oldLe = oldLs_List.FirstOrDefault(p => p.Key == LS_Assignmets[dmxCh]).Value;
        //                    tmp2 = AppContext.CountByType[oldLe.PointType];
        //                    if (dmxCh % tmp2 != 0)
        //                        oldPointNo = dmxCh / tmp2 + 1 - oldLe.StartPoint + 1;
        //                    else
        //                        oldPointNo = dmxCh / tmp2 - oldLe.StartPoint + 1;
                            
        //                    ConflictList.Add(new Conflict(oldLe, oldPointNo, le, i - le.StartPoint + 1, dmxCh));
        //                }
        //                else
        //                    LS_Assignmets[dmxCh] = (byte)le.Id;
        //            }
        //        }
        //        if (ConflictList.Count != 0)
        //        {
        //            // восстанавливаем предыдущее состоянние
        //            for (int i = 0; i < 512; i++)
        //            {
        //                LS_Assignmets[i] = backUp[i];
        //            }
        //            return false;
        //        }
        //        else
        //            return true;
        //        //return false;
        //    }
        //    else
        //    // первый LightStrip в Universe
        //    {
        //        if (LS_Assignmets == null)
        //            LS_Assignmets = new byte[512];
        //        for (int i = le.StartPoint; i < le.StartPoint + le.PointCount; i++)
        //        {
        //            for (int j = 1; j <= AppContext.CountByType[le.PointType]; j++)
        //            {
        //                dmxCh = (i - 1) * AppContext.CountByType[le.PointType] + j;
        //                LS_Assignmets[dmxCh] = (byte)le.Id;
        //            }
        //        }
        //        return true;
        //    }
        //}
    }

    public class Conflict : ObservableObject
    {
        public Conflict (LightElement oldLe, int oldPoint, LightElement currentLe, int currentPoint, int dmxCh)
        {
            OldLe = oldLe;
            OldPointNo = oldPoint;
            CurrentLe = currentLe;
            CurrentPointNo = currentPoint;
            DMXch = dmxCh;
        }
        public LightElement OldLe { get; set; }
        public LightElement CurrentLe { get; set; }
        public int OldPointNo { get; set; }
        public int CurrentPointNo { get; set; }
        public int DMXch { get; set; }
    }
}
