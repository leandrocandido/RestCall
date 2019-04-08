using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestCall;

namespace TestCall
{
    [TestClass]
    public class CallRestTest
    {
        [TestMethod]
        public void Input001()
        {
            int result = Countries.GetCountries("un", 100090);
            Assert.IsTrue(result ==  8);
        }

        [TestMethod]
        public void Input002()
        {
            int result = Countries.GetCountries("united", 200);
            Assert.IsTrue(result == 4);
        }

        [TestMethod]
        public void Input003()
        {
            int result = Countries.GetCountries("in", 1000000);
            Assert.IsTrue(result == 21);
        }
    }
}
