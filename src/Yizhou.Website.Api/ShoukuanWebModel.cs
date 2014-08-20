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
            this.jiekuanRiqi = shoukuan.Dingdan.JiekuanRiqi;
            this.xiadanRiqi = shoukuan.Dingdan.XiadanRiqi;
            this.kehu = shoukuan.Dingdan.Kehu.Name;
        }

        public string dingdanhao { set; get; }

        public string yewuyuan { set; get; }

        public string kehu { set; get; }

        public DateTime xiadanRiqi { set; get; }

        public DateTime jiekuanRiqi { set; get; }
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

    [Serializable]
    public class ShoukuanListModel
    {
        public List<ShoukuanGridModel> shoukuanList;

        public int totalCount;

        public double shoukuanJineSum;

        public double tichengSum;
    }

    [Serializable]
    public class ShoukuanFilterModel
    {
        public string keyword;

        public DateRange xiadanRiqi { set; get; }

        public DateRange jiekuanRiqi { set; get; }

        public DateRange shoukuanRiqi { set; get; }

        public int start;

        public int size;
    }
}
