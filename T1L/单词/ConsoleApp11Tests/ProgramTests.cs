using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordCounts.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void WordsCountTest()
        {
            Program a = new Program();
            string path1 = "C:\\Users\\RAIse\\Desktop\\ss.txt";
            StreamReader b = new StreamReader(path1, Encoding.Default);
            a.WordsCount();
            a.Print1();
            Assert.AreEqual(0, a.characters);

        }
    }
}