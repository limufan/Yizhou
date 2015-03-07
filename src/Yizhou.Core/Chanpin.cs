using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yizhou.Core
{
    public class Chanpin
    {
        public string Id { set; get; }

        public string Name { set; get; }

        public string Xinghao { set; get; }

        public string Guige { set; get; }

        public string Danwei { set; get; }

        public double XiaoshouDijia { set; get; }

        public double Chengbenjia { set; get; }

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

        public void Changed()
        {
            this.BuildKeyword();
        }

        private void BuildKeyword()
        {
            this._keywords = this.Beizhu + this.Danwei + this.Guige + this.Name + this.Xinghao;
        }

        public Chanpin Clone()
        {
            return this.MemberwiseClone() as Chanpin;
        }

    }
}
