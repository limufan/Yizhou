using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Spring.Context.Support;
using System.IO;

namespace Yizhou.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string jinghaiStrpingConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Yizhou.spring.config");
                IApplicationContext ctx1 = new XmlApplicationContext(jinghaiStrpingConfig);

                Console.Out.WriteLine("Server listening...");
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            Console.ReadLine();
        }
    }
}
