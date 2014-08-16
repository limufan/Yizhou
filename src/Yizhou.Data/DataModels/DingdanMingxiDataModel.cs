using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;

namespace Yizhou.Data.DataModels
{
    public class DingdanMingxiDataModel
    {
        public string ChanpinName { set; get; }

        public string ChanpinGuige { set; get; }

        public string ChanpinDanwei { set; get; }

        public int Tongshu { set; get; }

        public int Shuliang { set; get; }

        public double XiaoshouDanjia { set; get; }

        public double XiaoshouDijia { set; get; }

        public double Yewulv { set; get; }

        public YewulvFangshi YewulvFangshi { set; get; }

        public bool ShifouKaipiao { set; get; }

        public string Beizhu { set; get; }
    }
}
