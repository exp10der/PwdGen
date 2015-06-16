using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PwdGen.Helpers;

namespace UnitTest
{
    [TestClass]
   
    public class UnitTest
    {
        HelperGenerator _helper = new HelperGenerator();
        [TestMethod]
        public void TestLen()
        {
            
            for (int i = 0; i < 1000; i++)
            {
                var pass = _helper.GetPass();
                int lenpass = pass.Length;
                Assert.AreEqual(8, lenpass);
            }
        }
        [TestMethod]
        public void TestLowerCase()
        {
            for (int i = 0; i < 1000; i++)
            {
                var pass = _helper.GetPass();
                int countLowerCase = pass.Count(Char.IsLower);
                Assert.IsTrue(countLowerCase > 1);
            }
        }
        [TestMethod]
        public void TestSimbol()
        {
            for (int i = 0; i < 1000; i++)
            {
                var pass = _helper.GetPass();
                int countSymbol = pass.Count(Char.IsSymbol);
                Assert.IsTrue(countSymbol <= 3); 
            }
        }
        [TestMethod]
        public void TestFull()
        {
            for (int i = 0; i < 1000; i++)
            {
                var pass = _helper.GetPass();
                int lenpass = pass.Length;
                int countLowerCase = pass.Count(Char.IsLower);
                int countSymbol = pass.Count(Char.IsSymbol);

                Assert.IsTrue(lenpass == 8 && countLowerCase <= 3 && countSymbol <=3);
            }
        }
    }
}
