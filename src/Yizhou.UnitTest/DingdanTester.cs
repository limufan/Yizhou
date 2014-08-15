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
            Dingdan dingdan = new Dingdan();
            dingdan.XiadanRiqi = new DateTime(2014, 8, 15);
            dingdan.JiekuanFangshi = JiekuanFangshi.Yigeyue;
            DingdanMingxi mingxi = new DingdanMingxi { Dingdan = dingdan, XiaoshouDijia = 11, XiaoshouDanjia = 10, Shuliang = 100, Yewulv = 0.2, YewulvFangshi = YewulvFangshi.Jine };
            dingdan.MingxiList.Add(mingxi);
            Shoukuan shoukuan = new Shoukuan { ShoukuanJine = 300, ShoukuanRiqi = dingdan.XiadanRiqi };
            dingdan.ShoukuanList.Add(shoukuan);

            dingdan.Jisuan();
            Assert.AreEqual(dingdan.JiekuanRiqi, new DateTime(2014, 9, 30));
            Assert.AreEqual(dingdan.WeishoukuanJine, 700);
            Assert.AreEqual(dingdan.YingshoukuanJine, 1000);
            Assert.AreEqual(dingdan.YishoukuanJine, 300);
            Assert.AreEqual(dingdan.Ticheng, mingxi.JisuanTicheng(shoukuan.ShoukuanJine, shoukuan.ShoukuanRiqi));
        }
    }
}
