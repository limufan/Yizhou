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
        public DingdanMingxiBaseModel()
        {

        }

        public DingdanMingxiBaseModel(DingdanMingxi mingxi)
        {
            ClassPropertyHelper.ChangeProperty(this, mingxi);
            this.danwei = mingxi.Chanpin.Danwei;
            this.guige = mingxi.Chanpin.Guige;
        }

        public string guige { set; get; }

        public string danwei { set; get; }

        public int tongshu { set; get; }

        public int shuliang { set; get; }

        public double xiaoshouDanjia { set; get; }

        public double xiaoshouDijia { set; get; }

        public double yewulv { set; get; }

        public string yewulvFangshi { set; get; }

        public bool shifouKaipiao { set; get; }

        public string beizhu { set; get; }

        public double shijiDanjia { set; get; }

        public double zongjine { set; get; }

        public double ticheng { set; get; }

        public double butie { set; get; }

        public double shoukuanJine { set; get; }

        public double yewufei { set; get; }
    }

    [Serializable]
    public class DingdanMingxiGridModel : DingdanMingxiBaseModel
    {
        public DingdanMingxiGridModel(DingdanMingxi mingxi)
            :base(mingxi)
        {
            this.dingdanhao = mingxi.Dingdan.Danhao;
            this.yewuyuan = mingxi.Dingdan.Yewuyuan.Name;
            this.fahuoRiqi = mingxi.Dingdan.FahuoRiqi;
            this.xiadanRiqi = mingxi.Dingdan.XiadanRiqi;
            this.chanpinName = mingxi.Chanpin.Name;
        }

        public string chanpinName { set; get; }

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
            : base(mingxi)
        {
            this.chanpin = new ChanpinInputModel(mingxi.Chanpin);
        }

        public ChanpinInputModel chanpin { set; get; }
    }
}
