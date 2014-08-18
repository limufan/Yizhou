using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Api;
using Yizhou.Core.Organization;

namespace Yizhou.Core
{
    public class Dingdan
    {
        public Dingdan(DingdanCreateInfo createInfo)
        {
            ClassPropertyHelper.ChangeProperty(this, createInfo);
            if (this.MingxiList == null)
            {
                this.MingxiList = new List<DingdanMingxi>();
            }
            if (this.ShoukuanList == null)
            {
                this.ShoukuanList = new List<Shoukuan>();
            } 
            this.BuildKeyword();
            this.Jisuan();
        }

        public string Id { set; get; }

        public string Danhao { set; get; }

        public DateTime CreateTime { set; get; }

        public DateTime XiadanRiqi { set; get; }

        public DateTime FahuoRiqi { set; get; }

        public User Yewuyuan { set; get; }

        public Kehu Kehu { set; get; }

        public string Shouhuoren { set; get; }

        public string ShouhuorenDianhua { set; get; }

        public string ShouhuoDizhi { set; get; }

        public string JiekuanFangshi { set; get; }

        public DateTime JiekuanRiqi { private set; get; }

        public double YingshoukuanJine
        {
            get
            {
                return this.MingxiList.Sum(x => x.Zongjine);
            }
        }

        public double YishoukuanJine
        {
            get
            {
                return this.ShoukuanList.Sum(x => x.ShoukuanJine);
            }
        }

        public double WeishoukuanJine
        {
            get
            {
                return this.YingshoukuanJine - this.YishoukuanJine;
            }
        }

        public bool ShifouShouwan
        {
            get
            {
                return this.WeishoukuanJine <= 0;
            }
        }

        public double Ticheng
        {
            get
            {
                return this.MingxiList.Sum(m => m.Ticheng);
            }
        }

        public List<DingdanMingxi> MingxiList { set; get; }

        public List<Shoukuan> ShoukuanList { set; get; }

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

        public void Change(DingdanChangeInfo changeInfo)
        {
            ClassPropertyHelper.ChangeProperty(this, changeInfo);
            this.BuildKeyword();
            this.Jisuan();
        }

        private void BuildKeyword()
        {
            string kehuName = this.Kehu == null ? "" : this.Kehu.Name;
            string yewuyuanName = this.Yewuyuan == null ? "" : this.Yewuyuan.Name;
            this._keywords = this.Beizhu + this.Danhao + this.JiekuanFangshi + kehuName + this.ShouhuoDizhi +
                this.Shouhuoren + this.ShouhuorenDianhua + yewuyuanName;
        }

        public Dingdan Clone()
        {
            return this.MemberwiseClone() as Dingdan;
        }

        public void Jisuan()
        {
            this.JisuanJiekuanRiqi();
            this.MingxiList.ForEach(m => m.Jisuan());
        }

        /// <summary>
        /// 计算结款日期
        /// </summary>
        private void JisuanJiekuanRiqi()
        {
            if (this.JiekuanFangshi == "1个月月结")
            {
                DateTime nextMonth = this.XiadanRiqi.AddMonths(1);
                this.JiekuanRiqi = new DateTime(nextMonth.Year, nextMonth.Month, DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month)).Date;
            }
            else if (this.JiekuanFangshi == "2个月月结")
            {
                DateTime nextMonth = this.XiadanRiqi.AddMonths(2);
                this.JiekuanRiqi = new DateTime(nextMonth.Year, nextMonth.Month, DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month)).Date;
            }
            else if (this.JiekuanFangshi == "3个月月结")
            {
                DateTime nextMonth = this.XiadanRiqi.AddMonths(3);
                this.JiekuanRiqi = new DateTime(nextMonth.Year, nextMonth.Month, DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month)).Date;
            }
        }

        /// <summary>
        /// 计算收款提成
        /// </summary>
        /// <param name="chanpinList"></param>
        /// <param name="shoukuan"></param>
        /// <param name="dingdanJine"></param>
        /// <returns></returns>
        public double JisuanTicheng(Shoukuan shoukuan)
        {
            double ticheng = 0;
            foreach (DingdanMingxi mingxi in this.MingxiList)
            {
                double chanpinShoukuan = this.JisuanChanpinShoukuan(mingxi.Zongjine, shoukuan.ShoukuanJine);
                ticheng += mingxi.JisuanTicheng(chanpinShoukuan, shoukuan.ShoukuanRiqi);
            }
            return ticheng;
        }

        /// <summary>
        /// 计算产品收款金额
        /// </summary>
        /// <param name="chanpin"></param>
        /// <param name="shoukuanJine"></param>
        /// <returns></returns>
        public double JisuanChanpinShoukuan(double chanpinZongjine, double shoukuanJine)
        {
            double chanpinShoukuan = shoukuanJine * (chanpinZongjine / this.YingshoukuanJine);
            return Math.Round(chanpinShoukuan, 2);
        }
    }
}
