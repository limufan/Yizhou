using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Website.Api;

namespace Yizhou.DataImort.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //KehuImporter kehuImporter = new KehuImporter();
            //kehuImporter.Import();
            //ChanpinImporter chanpinImporter = new ChanpinImporter();
            //chanpinImporter.Import();
            DingdanImporter dingdanImporter = new DingdanImporter();
            dingdanImporter.Import();
            Console.WriteLine("imported");
        }
    }
}
