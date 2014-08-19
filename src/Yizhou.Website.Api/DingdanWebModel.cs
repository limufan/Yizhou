using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;

namespace Yizhou.Website.Api
{
    [Serializable]
    public class DingdanBaseModel
    {
        public DingdanBaseModel()
        {

        }

        public string id { set; get; }

        public string danhao { set; get; }

        public DateTime xiadanRiqi { set; get; }

        public DateTime fahuoRiqi { set; get; }

        public string shouhuoren { set; get; }

        public string shouhuorenDianhua { set; get; }

        public string shouhuoDizhi { set; get; }

        public DateTime jiekuanRiqi { set; get; }

        public double yingshoukuanJine { set; get; }

        public double yishoukuanJine { set; get; }

        public double weishoukuanJine { set; get; }

        public bool shifouShouwan { set; get; }

        public double ticheng { set; get; }

        public string beizhu { set; get; }

        public string jiekuanFangshi { set; get; }
    }

    [Serializable]
    public class DingdanDetailsModel : DingdanBaseModel
    {
        public DingdanDetailsModel()
        {

        }

        public DingdanDetailsModel(Dingdan chanpin)
        {
            ClassPropertyHelper.ChangeProperty(this, chanpin);
            this.kehu = new KehuInputModel { id = chanpin.Kehu.Id, name = chanpin.Kehu.Name};
            this.yewuyuan = new UserInputModel(chanpin.Yewuyuan);
            this.jiekuanFangshi = chanpin.JiekuanFangshi;
            this.mingxiList = chanpin.MingxiList.Select(m => new DingdanMingxiDetailsModel(m)).ToList();
            if (chanpin.ShoukuanList != null)
            {
                this.shoukuanList = chanpin.ShoukuanList.Select(m => new ShoukuanDetailsModel(m)).ToList();
            }
        }

        public KehuInputModel kehu { set; get; }

        public UserInputModel yewuyuan { set; get; }

        public List<DingdanMingxiDetailsModel> mingxiList { set; get; }

        public List<ShoukuanDetailsModel> shoukuanList { set; get; }
    }

    [Serializable]
    public class DingdanGridModel : DingdanBaseModel
    {
        public DingdanGridModel(Dingdan chanpin)
        {
            ClassPropertyHelper.ChangeProperty(this, chanpin);
            this.kehu = chanpin.Kehu.Name;
            this.yewuyuan = chanpin.Yewuyuan.Name;
        }

        public string kehu { set; get; }

        public string yewuyuan { set; get; }
    }

    [Serializable]
    public class DingdanFilterModel
    {
        public string keyword;

        public int start;

        public int size;
    }

    [Serializable]
    public class DingdanListModel
    {
        public List<DingdanGridModel> dingdanList;

        public int totalCount;

        public double yingshoukuanJineSum;

        public double yishoukuanJineSum;

        public double weishoukuanJineSum;

        public double tichengSum;
    }
}
