using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Yizhou.Core;

namespace Yizhou.UnitTest
{
    public class DingdanTester
    {
        [Test]
        public void JisuanInfoTest()
        {
            DingdanCreateInfo createInfo = new DingdanCreateInfo();
            createInfo.XiadanRiqi = new DateTime(2014, 8, 15);
            createInfo.JiekuanFangshi = "1个月月结";
            createInfo.Kehu = new Kehu();
            Dingdan dingdan = new Dingdan(createInfo);
            DingdanChangeInfo changeInfo = new DingdanChangeInfo(dingdan);
            DingdanMingxiCreateInfo mingxiCreateInfo = new DingdanMingxiCreateInfo { Dingdan = dingdan, XiaoshouDijia = 11, XiaoshouDanjia = 10, Shuliang = 100, Yewulv = 0.2, YewulvFangshi = "按金额" };
            DingdanMingxi mingxi = new DingdanMingxi(mingxiCreateInfo);
            changeInfo.MingxiList.Add(mingxi);
            Shoukuan shoukuan = new Shoukuan { ShoukuanJine = 300, ShoukuanRiqi = dingdan.XiadanRiqi };
            changeInfo.ShoukuanList.Add(shoukuan);
            dingdan.Change(changeInfo);
            Assert.AreEqual(dingdan.JiekuanRiqi, new DateTime(2014, 9, 30));
            Assert.AreEqual(dingdan.WeishoukuanJine, 700);
            Assert.AreEqual(dingdan.YingshoukuanJine, 1000);
            Assert.AreEqual(dingdan.YishoukuanJine, 300);
            Assert.AreEqual(dingdan.Ticheng, mingxi.JisuanTicheng(shoukuan.ShoukuanJine, shoukuan.ShoukuanRiqi));
        }
    }
}
