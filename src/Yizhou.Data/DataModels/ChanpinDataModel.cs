using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core;

namespace Yizhou.Data.DataModels
{
    public class ChanpinDataModel
    {
        public virtual string Id { set; get; }

        public virtual string Name { set; get; }

        public virtual string Guige { set; get; }

        public virtual string Danwei { set; get; }

        public virtual double XiaoshouDijia { set; get; }

        public virtual double Chengbenjia { set; get; }

        public virtual string Beizhu { set; get; }

        public virtual string Xinghao { set; get; }
    }
}
