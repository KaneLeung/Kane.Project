using Kane.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kane.UnitTest
{
    [TestClass]
    public class ConvertTest
    {
        [TestMethod]
        public void TestAmountInWords()
        {
            var decimalAmount = 1234567890.12M;
            var decimalResult = ConvertHelper.ToAmoutInWords(decimalAmount);
            Assert.AreEqual(decimalResult, "壹拾贰亿叁仟肆佰伍拾陆万柒仟捌佰玖拾元壹角贰分");
            var doubleAmount = 1234567890.12;
            var doubleResult = ConvertHelper.ToAmoutInWords(doubleAmount);
            Assert.AreEqual(doubleResult, "壹拾贰亿叁仟肆佰伍拾陆万柒仟捌佰玖拾元壹角贰分");
        }
    }
}
