using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;

namespace Yizhou.Website.Api
{
    [Serializable]
    public class ChanpinBaseModel
    {
        public ChanpinBaseModel()
        {

        }

        public ChanpinBaseModel(Chanpin chanpin)
        {
            ClassPropertyHelper.ChangeProperty(this, chanpin);
        }

        public string id { set; get; }

        public string name { set; get; }

        public string guige { set; get; }

        public string danwei { set; get; }

        public double xiaoshouDijia { set; get; }

        public double chengbenjia { set; get; }

        public string beizhu{ set; get; }
    }

    [Serializable]
    public class ChanpinDetailsModel : ChanpinBaseModel
    {
        public ChanpinDetailsModel()
        {

        }

        public ChanpinDetailsModel(Chanpin chanpin)
            :base(chanpin)
        {
            
        }
    }

    [Serializable]
    public class ChanpinGridModel : ChanpinBaseModel
    {
        public ChanpinGridModel()
        {

        }

        public ChanpinGridModel(Chanpin chanpin)
            : base(chanpin)
        {
            
        }
    }

    [Serializable]
    public class ChanpinFilterModel
    {
        public string keyword;

        public int start;

        public int size;
    }
}
