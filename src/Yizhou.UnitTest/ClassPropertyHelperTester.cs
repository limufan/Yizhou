using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Yizhou.Core;

namespace Yizhou.UnitTest
{
    class C1
    {
        public string p1;
        public DateTime p2;
        public List<string> p3;
    }

    class C2: C1
    {
        public string p4 { set; get; }
    }

    class C3 : C1
    {
        public string p4 { set; get; }
    }

    public class C4
    {
        public string P1 { set; get; }
        public DateTime P2 { private set; get; }
        public List<string> P3 { set; get; }
    }

    public class ClassPropertyHelperTester
    {

        [Test]
        public void TestChangeProperty()
        {
            C1 c1 = new C1();
            c1.p1 = "11";
            c1.p3 = new List<string> {"1", "2" };

            C2 c2 = new C2();
            ClassPropertyHelper.ChangeProperty(c2, c1);
            TestHelper.AreEqual(c1, c2);

            C4 c4 = new C4();
            ClassPropertyHelper.ChangeProperty(c4, c1);
            TestHelper.AreEqual(c1, c4);


            ClassPropertyHelper.ChangeProperty(c1, c4);
            TestHelper.AreEqual(c1, c4);
        }
    }
}
