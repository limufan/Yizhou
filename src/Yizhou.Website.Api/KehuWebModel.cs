using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;

namespace Yizhou.Website.Api
{
    [Serializable]
    public class KehuBaseModel
    {
        public KehuBaseModel()
        {

        }

        public KehuBaseModel(Kehu kehu)
        {
            ClassPropertyHelper.ChangeProperty(this, kehu);
        }

        public string id { set; get; }

        public string name{ set; get; }

        public double yewulv{ set; get; }

        public string lianxidianhua{ set; get; }

        public string gongsiDizhi{ set; get; }

        public string shouhuoDizhi{ set; get; }

        public string shouhuoren{ set; get; }

        public string shouhuorenDianhua{ set; get; }

        public string shoukuanren{ set; get; }

        public string shoukuanrenDianhua{ set; get; }

        public string chuanzhen{ set; get; }

        public string email{ set; get; }

        public string beizhu{ set; get; }

        public string yewulvFangshi{ set; get; }

        public string jiekuanFangshi{ set; get; }
    }

    [Serializable]
    public class KehuDetailsModel : KehuBaseModel
    {
        public KehuDetailsModel()
        {

        }

        public KehuDetailsModel(Kehu kehu)
            :base(kehu)
        {
            this.yewuyuan = new UserInputModel(kehu.Yewuyuan);
        }

        public UserInputModel yewuyuan;
    }

    [Serializable]
    public class KehuGridModel : KehuBaseModel
    {
        public KehuGridModel()
        {

        }

        public KehuGridModel(Kehu kehu)
            : base(kehu)
        {
            this.yewuyuan = kehu.Yewuyuan.Name;
        }

        public string yewuyuan;
    }

    [Serializable]
    public class KehuFilterModel
    {
        public string keyword;

        public int start;

        public int size;
    }

    [Serializable]
    public class KehuInputModel
    {
        public string id;

        public string name;
    }
}
