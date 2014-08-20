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
            this.BuildKeyword();
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

        private void BuildKeyword()
        {
            this._keywords = this.Dingdan.Keywords + this.Beizhu;
        }
    }
}
