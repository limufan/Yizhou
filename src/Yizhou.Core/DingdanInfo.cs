using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core.Organization;

namespace Yizhou.Core
{
    public class DingdanBaseInfo
    {
        public string Danhao { set; get; }

        public DateTime XiadanRiqi { set; get; }

        public DateTime FahuoRiqi { set; get; }

        public User Yewuyuan { set; get; }

        public Kehu Kehu { set; get; }

        public string Shouhuoren { set; get; }

        public string ShouhuorenDianhua { set; get; }

        public string ShouhuoDizhi { set; get; }

        public JiekuanFangshi JiekuanFangshi { set; get; }

        public DateTime JiekuanRiqi { private set; get; }

        public double YingshoukuanJine { set; get; }

        public double YishoukuanJine { set; get; }

        public double WeishoukuanJine { set; get; }

        public List<DingdanMingxi> MingxiList { set; get; }

        public List<Shoukuan> ShoukuanList { set; get; }

        public string Beizhu { set; get; }
    }

    public class DingdanNewInfo
    {
        public string Id { set; get; }
    }

    public class DingdanXiugaiInfo
    {

    }
}
