using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;

namespace Yizhou.Data.DataModels
{
    public class DingdanDataModel
    {
        public virtual string Id { set; get; }

        public virtual string Danhao { set; get; }

        public virtual DateTime XiadanRiqi { set; get; }

        public virtual DateTime? FahuoRiqi { set; get; }

        public virtual string Yewuyuan { set; get; }

        public virtual string KehuId { set; get; }

        public virtual string Shouhuoren { set; get; }

        public virtual string ShouhuorenDianhua { set; get; }

        public virtual string ShouhuoDizhi { set; get; }

        public virtual string JiekuanFangshi { set; get; }

        public virtual string MingxiListJson { set; get; }

        public virtual string ShoukuanListJson { set; get; }

        public virtual string Beizhu { set; get; }

        public virtual DateTime CreateTime { set; get; }

        /// <summary>
        /// 发货数量
        /// </summary>
        public virtual double? FahuoShuliang { set; get; }

        /// <summary>
        /// 发货单号
        /// </summary>
        public virtual string FahuoDanhao { set; get; }
    }
}
