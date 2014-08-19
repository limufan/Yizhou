using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;

namespace Yizhou.Website.Api
{
    [Serializable]
    public class ShoukuanBaseModel
    {
        public DateTime shoukuanRiqi { set; get; }

        public double shoukuanJine { set; get; }

        public double ticheng { set; get; }

        public string beizhu { set; get; }
    }

    [Serializable]
    public class ShoukuanGridModel : ShoukuanBaseModel
    {
        public ShoukuanGridModel(Shoukuan shoukuan)
        {
            ClassPropertyHelper.ChangeProperty(this, shoukuan);
            this.dingdanhao = shoukuan.Dingdan.Danhao;
            this.yewuyuan = shoukuan.Dingdan.Yewuyuan.Name;
            this.fahuoRiqi = shoukuan.Dingdan.FahuoRiqi;
            this.xiadanRiqi = shoukuan.Dingdan.XiadanRiqi;
        }

        public string dingdanhao { set; get; }

        public string yewuyuan { set; get; }

        public DateTime fahuoRiqi { set; get; }

        public DateTime xiadanRiqi { set; get; }
    }

    [Serializable]
    public class ShoukuanDetailsModel : ShoukuanBaseModel
    {
        public ShoukuanDetailsModel()
        {

        }

        public ShoukuanDetailsModel(Shoukuan shoukuan)
        {
            ClassPropertyHelper.ChangeProperty(this, shoukuan);
        }
    }
}
