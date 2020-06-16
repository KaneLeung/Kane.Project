#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.UnitTest
* 项目描述 ：
* 类 名 称 ：CryptoTest
* 类 描 述 ：加密解密单元测试
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.UnitTest
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/12/27 0:39:55
* 更新时间 ：2020/02/23 23:39:55
* 版 本 号 ：v1.0.1.0
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
    public class CryptoTest
    {
        [TestMethod]
        public void TestDES()
        {
            string key = "Fa410cOr=+)^*()d";
            string data = "Copyright @ Kane Leung 2020. All rights reserved.";
            var des = new DesHelper();
            var encryptValue = des.Encrypt(data, key);
            var decryptValue = des.Decrypt(encryptValue, key);
            Assert.AreEqual(data, decryptValue);
        }
    }
}