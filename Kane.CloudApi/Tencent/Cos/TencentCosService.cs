#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent.Cos
* 项目描述 ：
* 类 名 称 ：TencentCosService
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent.Cos
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/3/1 23:46:44
* 更新时间 ：2020/3/1 23:46:44
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace Kane.CloudApi.Tencent.Cos
{
    public class TencentCosService
    {
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceHost { get; set; }
        /// <summary>
        /// 在云API密钥上申请的标识身份的 SecretID，一个 SecretID 对应唯一的 SecretKey
        /// </summary>
        public string SecretID { private get; set; }
        /// <summary>
        /// SecretID 对应唯一的 SecretKey
        /// </summary>
        public string SecretKey { private get; set; }

    }
}