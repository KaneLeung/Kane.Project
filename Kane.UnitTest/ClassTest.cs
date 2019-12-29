#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.UnitTest
* 项目描述 ：
* 类 名 称 ：ClassTest
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.UnitTest
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/12/29 23:10:47
* 更新时间 ：2019/12/29 23:10:47
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Kane.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Kane.UnitTest
{
    [TestClass]
    public class ClassTest
    {
        [TestMethod]
        public void DeepCloneTest()
        {
            var source = new DeepCloneClass()
            {
                ID = 1,
                CreateTime = DateTime.Now,
                Name = "Kane",
                Total = 123456789.0123456M
            };
            var target = source.DeepClone();
            Assert.AreNotEqual(source, target);
        }

        public class DeepCloneClass
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public DateTime CreateTime { get; set; }
            public decimal Total { get; set; }
        }
    }
}