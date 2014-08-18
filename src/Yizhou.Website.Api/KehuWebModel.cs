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
            this.beizhu = kehu.Beizhu;
            this.chuanzhen = kehu.Chuanzhen;
            this.createTime = kehu.CreateTime;
            this.email = kehu.Email;
            this.gongsiDizhi = kehu.GongsiDizhi;
            this.id = kehu.Id;
            this.lianxidianhua = kehu.Lianxidianhua;
            this.name = kehu.Name;
            this.shouhuoDizhi = kehu.ShouhuoDizhi;
            this.shouhuoren = kehu.Shouhuoren;
            this.shouhuorenDianhua = kehu.ShouhuorenDianhua;
            this.yewulv = kehu.Yewulv;
            this.yewulvFangshi = kehu.YewulvFangshi;
            this.jiekuanFangshi = kehu.JiekuanFangshi;
        }

        public string id;

        public string name;

        public DateTime createTime;

        public double yewulv;

        public string lianxidianhua;

        public string gongsiDizhi;

        public string shouhuoDizhi;

        public string shouhuoren;

        public string shouhuorenDianhua;

        public string chuanzhen;

        public string email;

        public string beizhu;

        public string yewulvFangshi;

        public string jiekuanFangshi;
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
