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

        public DingdanBaseModel(Dingdan chanpin)
        {
            ClassPropertyHelper.ChangeProperty(this, chanpin);
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
    }

    [Serializable]
    public class DingdanDetailsModel : DingdanBaseModel
    {
        public DingdanDetailsModel()
        {

        }

        public DingdanDetailsModel(Dingdan chanpin)
            :base(chanpin)
        {
            this.kehu = new KehuInputModel { id = chanpin.Kehu.Id, name = chanpin.Kehu.Name};
            this.yewuyuan = new UserInputModel(chanpin.Yewuyuan);
            this.jiekuanFangshi = chanpin.JiekuanFangshi;
            this.mingxiList = chanpin.MingxiList;
        }

        public KehuInputModel kehu { set; get; }

        public UserInputModel yewuyuan { set; get; }

        public JiekuanFangshi jiekuanFangshi { set; get; }

        public List<DingdanMingxiWebModel> mingxiList { set; get; }

        public List<Shoukuan> shoukuanList { set; get; }
    }

    [Serializable]
    public class DingdanGridModel : DingdanBaseModel
    {
        public DingdanGridModel()
        {

        }

        public DingdanGridModel(Dingdan chanpin)
            : base(chanpin)
        {
            
        }
    }

    [Serializable]
    public class DingdanFilterModel
    {
        public string keyword;

        public int start;

        public int size;
    }
}
