﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yizhou.Core
{
    public class DingdanMingxiBaseInfo
    {
        public Chanpin Chanpin { set; get; }

        public int Tongshu { set; get; }

        public double Shuliang { set; get; }

        /// <summary>
        /// 发货数量
        /// </summary>
        public double? FahuoShuliang { set; get; }

        public double XiaoshouDanjia { set; get; }

        public double XiaoshouDijia { set; get; }

        public double Yewulv { set; get; }

        public string YewulvFangshi { set; get; }

        public bool ShifouKaipiao { set; get; }

        public string Beizhu { set; get; }
    }

    public class DingdanMingxiCreateInfo : DingdanMingxiBaseInfo
    {
        public Dingdan Dingdan { set; get; }
    }
}
