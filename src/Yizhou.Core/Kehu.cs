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

        public string YewulvFangshi { set; get; }

        public string JiekuanFangshi { set; get; }

        public string Lianxidianhua { set; get; }

        public string GongsiDizhi { set; get; }

        public string ShouhuoDizhi { set; get; }

        public string Shouhuoren { set; get; }

        public string ShouhuorenDianhua { set; get; }

        public string Shoukuanren { set; get; }

        public string ShoukuanrenDianhua { set; get; }

        public string Chuanzhen { set; get; }

        public string Email { set; get; }

        public string Beizhu { set; get; }

        private string _keywords;

        public string Keywords
        {
            get
            {
                if (string.IsNullOrEmpty(this._keywords))
                {
                    this.BuildKeyword();
                }
                return this._keywords;
            }
        }

        public void Changed()
        {
            this.BuildKeyword();
        }

        private void BuildKeyword()
        {
            this._keywords = this.Beizhu + this.Chuanzhen + this.Email + this.GongsiDizhi + this.JiekuanFangshi
                   + this.Lianxidianhua + this.Name + this.ShouhuoDizhi + this.Shouhuoren + this.ShouhuorenDianhua + this.Yewuyuan.Name
                   + this.YewulvFangshi + this.Shoukuanren + this.ShoukuanrenDianhua;
        }

        public Kehu Clone()
        {
            return this.MemberwiseClone() as Kehu;
        }
    }
}
