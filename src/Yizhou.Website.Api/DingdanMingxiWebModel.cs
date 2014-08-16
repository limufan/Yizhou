using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yizhou.Website.Api
{
    [Serializable]
    public class DingdanMingxiWebModel
    {
        public Dingdan Dingdan { set; get; }

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

        public double ShijiDanjia { private set; get; }

        public double Zongjine { private set; get; }

        public double Ticheng { private set; get; }

        public double Butie { private set; get; }

        public double ShoukuanJine { private set; get; }

        public double Yewufei { private set; get; }
    }
}
