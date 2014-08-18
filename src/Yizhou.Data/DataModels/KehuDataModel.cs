using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;

namespace Yizhou.Data.DataModels
{
    public class KehuDataModel
    {
        public virtual string Id { set; get; }

        public virtual string Name { set; get; }

        public virtual DateTime CreateTime { set; get; }

        public virtual string Yewuyuan { set; get; }

        public virtual double Yewulv { set; get; }

        public virtual string YewulvFangshi { set; get; }

        public virtual string JiekuanFangshi { set; get; }

        public virtual string Lianxidianhua { set; get; }

        public virtual string GongsiDizhi { set; get; }

        public virtual string ShouhuoDizhi { set; get; }

        public virtual string Shouhuoren { set; get; }

        public virtual string ShouhuorenDianhua { set; get; }

        public virtual string Chuanzhen { set; get; }

        public virtual string Email { set; get; }

        public virtual string Beizhu { set; get; }
    }
}
