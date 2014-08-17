using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yizhou.Core
{
    public class DingdanMingxi
    {
        public DingdanMingxi(DingdanMingxiCreateInfo createInfo)
        {
            ClassPropertyHelper.ChangeProperty(this, createInfo);
        }

        public Dingdan Dingdan { set; get; }

        public string ChanpinName { set; get; }

        public string ChanpinGuige { set; get; }

        public string ChanpinDanwei { set; get; }

        public int Tongshu { set; get; }

        public int Shuliang { set; get; }

        public double XiaoshouDanjia { set; get; }

        public double XiaoshouDijia { set; get; }

        public double Yewulv { set; get; }

        public YewulvFangshi YewulvFangshi { set; get; }

        public bool ShifouKaipiao { set; get; }

        public string Beizhu { set; get; }

        public double ShijiDanjia { private set; get; }

        public double Zongjine { private set; get; }

        public double Ticheng { private set; get; }

        public double Butie { private set; get; }

        public double ShoukuanJine { private set; get; }

        public double Yewufei { private set; get; }

        public void Jisuan()
        {
            this.JisuanZongjine();
            this.JisuanYewufei();
            this.JisuanShijiDanjia();
            this.JisuanButie();
            this.JisuanTicheng();
        }

        private void JisuanZongjine()
        {
            this.Zongjine = this.Shuliang * XiaoshouDanjia;
        }

        public void JisuanYewufei()
        {
            if (this.YewulvFangshi == YewulvFangshi.Jine)
            {
                this.Yewufei = this.Yewulv * this.Zongjine;
            }
            else
            {
                this.Yewufei = this.Yewulv * this.Shuliang;
            }
        }

        private void JisuanShijiDanjia()
        {
            this.ShijiDanjia = (this.Zongjine - this.Yewufei) / this.Shuliang;
            if (this.ShifouKaipiao)
            {
                this.ShijiDanjia = this.ShijiDanjia * 0.83;
            }
        }

        private void JisuanButie()
        {
            this.Butie = this.ShijiDanjia * this.Shuliang * 0.01;
        }

        private void JisuanTicheng()
        {
            this.ShoukuanJine = 0;
            this.Ticheng = 0;
            foreach (Shoukuan shoukuan in this.Dingdan.ShoukuanList)
            {
                double chanpinShoukuan = this.Dingdan.JisuanChanpinShoukuan(this.Zongjine, shoukuan.ShoukuanJine);
                this.ShoukuanJine += chanpinShoukuan;
                this.Ticheng += this.JisuanTicheng(chanpinShoukuan, shoukuan.ShoukuanRiqi);
            }
        }

        public double JisuanTicheng(double shoukuanJine, DateTime shoukuanRiqi)
        {
            //基价提成
            double jijiaTicheng = 0;
            if (this.ShijiDanjia > this.XiaoshouDijia)
            {
                jijiaTicheng = shoukuanJine * 0.03;
            }
            //差价提成
            double chajiaTicheng = 0;
            if (this.ShijiDanjia > this.XiaoshouDijia)
            {
                chajiaTicheng = (this.ShijiDanjia - this.XiaoshouDijia) * 0.2 * shoukuanJine;
            }
            double ticheng = jijiaTicheng + chajiaTicheng;
            //开票提成扣0.08
            if (this.ShifouKaipiao)
            {
                ticheng = ticheng * 0.92;
            }
            int shoukuanZhouqi = (shoukuanRiqi - this.Dingdan.JiekuanRiqi).Days;
            if (shoukuanZhouqi > 30 && shoukuanZhouqi <= 60)
            {
                //收款延期30到60天提成扣0.5
                ticheng = ticheng * 0.5;
            }
            else if (shoukuanZhouqi > 60)
            {
                //收款延期60天以上提成倒扣0.05
                ticheng = ticheng * 0.05 * -1;
            }
            return ticheng;
        }
    }
}
