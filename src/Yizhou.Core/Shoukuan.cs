using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yizhou.Core
{
    public class Shoukuan
    {
        public Shoukuan(ShoukuanCreateInfo createInfo)
        {
            ClassPropertyHelper.ChangeProperty(this, createInfo);
        }

        public Dingdan Dingdan { set; get; }

        public DateTime ShoukuanRiqi { set; get; }

        public double ShoukuanJine { set; get; }

        public string Beizhu { set; get; }

        public double Ticheng { private set; get; }

        public void Jisuan()
        {
            double ticheng = 0;
            foreach (DingdanMingxi mingxi in this.Dingdan.MingxiList)
            {
                double chanpinShoukuan = this.Dingdan.JisuanChanpinShoukuan(mingxi.Zongjine, this.ShoukuanJine);
                ticheng += mingxi.JisuanTicheng(chanpinShoukuan, this.ShoukuanRiqi);
            }
            this.Ticheng = ticheng;
        }
    }
}
