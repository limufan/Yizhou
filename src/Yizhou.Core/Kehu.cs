using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core.Organization;

namespace Yizhou.Core
{
    public class Kehu
    {
        public string Id { set; get; }

        public string Name { set; get; }

        public DateTime CreateTime { set; get; }

        public User Yewuyuan { set; get; }

        public double Yewulv { set; get; }

        public YewulvFangshi YewulvFangshi { set; get; }

        public JiekuanFangshi JiekuanFangshi { set; get; }

        public string Lianxidianhua { set; get; }

        public string GongsiDizhi { set; get; }

        public string ShouhuoDizhi { set; get; }

        public string Shouhuoren { set; get; }

        public string ShouhuorenDianhua { set; get; }

        public string Chuanzhen { set; get; }

        public string Email { set; get; }

        public string Beizhu { set; get; }

        public string Keywords { private set; get; }

        public void Changed()
        {
            this.Keywords = this.Beizhu + this.Chuanzhen +  this.Email + this.GongsiDizhi + YizhouHelper.GetName(this.JiekuanFangshi)
                + this.Lianxidianhua + this.Name + this.ShouhuoDizhi + this.Shouhuoren + this.ShouhuorenDianhua + this.Yewuyuan.Name
                + YizhouHelper.GetName(this.YewulvFangshi);
        }

        public Kehu Clone()
        {
            return this.MemberwiseClone() as Kehu;
        }
    }
}
