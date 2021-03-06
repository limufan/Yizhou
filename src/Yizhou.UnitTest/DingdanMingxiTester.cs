﻿using System;
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
            DingdanCreateInfo createInfo = new DingdanCreateInfo();
            createInfo.XiadanRiqi = new DateTime(2014, 8, 15);
            createInfo.JiekuanFangshi = "1个月月结";
            createInfo.Kehu = new Kehu();
            Dingdan dingdan = new Dingdan(createInfo);
            DingdanChangeInfo changeInfo = new DingdanChangeInfo(dingdan);
            DingdanMingxiCreateInfo mingxiCreateInfo = new DingdanMingxiCreateInfo { Dingdan = dingdan, XiaoshouDijia = 9, XiaoshouDanjia = 10, Shuliang = 1000, Yewulv = 0.03, YewulvFangshi = "按金额" };
            DingdanMingxi mingxi = new DingdanMingxi(mingxiCreateInfo);
            changeInfo.MingxiList.Add(mingxi);
            ShoukuanCreateInfo shoukuanCreateInfo = new ShoukuanCreateInfo { Dingdan = dingdan, ShoukuanJine = 300, ShoukuanRiqi = dingdan.XiadanRiqi };
            Shoukuan shoukuan = new Shoukuan(shoukuanCreateInfo);
            changeInfo.ShoukuanList.Add(shoukuan);
            dingdan.Change(changeInfo);

            Assert.AreEqual(10000, mingxi.Zongjine);
            Assert.AreEqual(300, mingxi.Yewufei);
            Assert.AreEqual(9.7, Math.Round(mingxi.ShijiDanjia, 2));
            Assert.AreEqual(97, Math.Round(mingxi.Butie, 2));
            Assert.AreEqual(51, Math.Round(mingxi.Ticheng, 2));
        }
    }
}
