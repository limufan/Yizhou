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

        public string JiekuanFangshi { set; get; }

        public string Beizhu { set; get; }

        public DateTime CreateTime { set; get; }
    }

    public class DingdanCreateInfo : DingdanBaseInfo
    {
        public string Id { set; get; }
    }

    public class DingdanChangeInfo : DingdanBaseInfo
    {
        public DingdanChangeInfo(Dingdan dingdan)
        {
            ClassPropertyHelper.ChangeProperty(this, dingdan);
        }

        public List<DingdanMingxi> MingxiList { set; get; }

        public List<Shoukuan> ShoukuanList { set; get; }
    }
}
