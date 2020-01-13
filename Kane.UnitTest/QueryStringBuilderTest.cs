#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.UnitTest
* 项目描述 ：
* 类 名 称 ：QueryStringBuilderTest
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.UnitTest
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/1/13 0:53:37
* 更新时间 ：2020/1/13 0:53:37
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Kane.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kane.UnitTest
{
    [TestClass]
    public class QueryStringBuilderTest
    {
        [TestMethod]
        public void BuilderTest()
        {
            var builder = new QueryStringBuilder();
            builder.Add("name", "Kane");
            var test1 = builder.ToString();
            builder.Add("time", "2020-01-13");
            builder.Add("total", "123456.123");
            builder.Add("chinese", "您好");
            var test2 = builder.ToString();
            Assert.AreEqual(test1, "?name=Kane");
            Assert.AreEqual(test2, "?name=Kane&time=2020-01-13&total=123456.123&chinese=%E6%82%A8%E5%A5%BD");
        }
    }
}