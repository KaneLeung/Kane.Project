#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.UnitTest
* 项目描述 ：
* 类 名 称 ：FileHelperTest
* 类 描 述 ：
* 所在的域 ：KK-MAGICBOOK
* 命名空间 ：Kane.UnitTest
* 机器名称 ：KK-MAGICBOOK 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2019/10/30 0:29:56
* 更新时间 ：2019/10/30 0:29:56
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2019. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Kane.Extension;

namespace Kane.UnitTest
{
    [TestClass]
    public class FileHelperTest
    {
        [TestMethod]
        [DataRow("D:\\微信截图_20191021005435.png",FileExt.PNG)]
        public void GetFileExtTest(string path,FileExt target)
        {
            var ext =FileHelper.GetFileExt(path);
            Assert.AreEqual(ext, target);
        }

        [TestMethod]
        public void GetPathAllFilesTest()
        {
            var files = FileHelper.GetPathAllFiles("D:\\Temp","*.7z");
            Assert.IsTrue(files.Count > 0);
        }
    }
}
