using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;

namespace Yizhou.Website.Api
{
    [Serializable]
    public class DingdanMingxiBaseModel
    {
        public string chanpinName { set; get; }

        public string chanpinGuige { set; get; }

        public string chanpinDanwei { set; get; }

        public int tongshu { set; get; }

        public int shuliang { set; get; }

        public double xiaoshouDanjia { set; get; }

        public double xiaoshouDijia { set; get; }

        public double yewulv { set; get; }

        public YewulvFangshi yewulvFangshi { set; get; }

        public bool shifouKaipiao { set; get; }

        public string beizhu { set; get; }

        public double shijiDanjia { private set; get; }

        public double zongjine { private set; get; }

        public double ticheng { private set; get; }

        public double butie { private set; get; }

        public double shoukuanJine { private set; get; }

        public double yewufei { private set; get; }
    }

    [Serializable]
    public class DingdanMingxiGridModel : DingdanMingxiBaseModel
    {
        public DingdanMingxiGridModel(DingdanMingxi mingxi)
        {
            ClassPropertyHelper.ChangeProperty(this, mingxi);
            this.dingdanhao = mingxi.Dingdan.Danhao;
            this.yewuyuan = mingxi.Dingdan.Yewuyuan.Name;
            this.fahuoRiqi = mingxi.Dingdan.FahuoRiqi;
            this.xiadanRiqi = mingxi.Dingdan.XiadanRiqi;
        }

        public string dingdanhao { set; get; }

        public string yewuyuan { set; get; }

        public DateTime fahuoRiqi { set; get; }

        public DateTime xiadanRiqi { set; get; }
    }

    [Serializable]
    public class DingdanMingxiDetailsModel : DingdanMingxiBaseModel
    {
        public DingdanMingxiDetailsModel()
        {
        }

        public DingdanMingxiDetailsModel(DingdanMingxi mingxi)
        {
            ClassPropertyHelper.ChangeProperty(this, mingxi);
        }
    }
}
