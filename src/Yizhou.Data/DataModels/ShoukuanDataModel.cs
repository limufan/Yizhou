using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;

namespace Yizhou.Data.DataModels
{
    public class ShoukuanDataModel
    {
        public ShoukuanDataModel()
        {
        
        }

        public ShoukuanDataModel(Shoukuan shoukuan)
        {
            this.Beizhu = shoukuan.Beizhu;
            this.ShoukuanJine = shoukuan.ShoukuanJine;
            this.ShoukuanRiqi = shoukuan.ShoukuanRiqi;
        }

        public DateTime ShoukuanRiqi { set; get; }

        public double ShoukuanJine { set; get; }

        public string Beizhu { set; get; }
    }
}
