using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Yizhou.Core;

namespace Yizhou.UnitTest
{
    public class DingdanMingxiTester
    {
        [Test]
        public void JisuanInfoTest()
        {
            Dingdan dingdan = new Dingdan();
            dingdan.XiadanRiqi = new DateTime(2014, 8, 15);
            dingdan.JiekuanFangshi = JiekuanFangshi.Yigeyue;
            DingdanMingxi mingxi = new DingdanMingxi { Dingdan = dingdan, XiaoshouDijia = 9, XiaoshouDanjia = 10, Shuliang = 1000, Yewulv = 0.03, YewulvFangshi = YewulvFangshi.Jine };
            dingdan.MingxiList.Add(mingxi);
            Shoukuan shoukuan = new Shoukuan { ShoukuanJine = 300, ShoukuanRiqi = dingdan.XiadanRiqi };
            dingdan.ShoukuanList.Add(shoukuan);

            dingdan.Jisuan();
            Assert.AreEqual(10000, mingxi.Zongjine);
            Assert.AreEqual(300, mingxi.Yewufei);
            Assert.AreEqual(9.7, Math.Round(mingxi.ShijiDanjia, 2));
            Assert.AreEqual(97, Math.Round(mingxi.Butie, 2));
            Assert.AreEqual(51, Math.Round(mingxi.Ticheng, 2));
        }
    }
}
