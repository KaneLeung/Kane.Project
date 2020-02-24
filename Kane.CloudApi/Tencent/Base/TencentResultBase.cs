#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：常用云服务Api
* 类 名 称 ：TencentResultBase
* 类 描 述 ：腾讯云公共返回结果
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/23 0:51:35
* 更新时间 ：2020/2/23 0:51:35
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion

namespace Kane.CloudApi.Tencent
{
    /// <summary>
    /// 腾讯云公共返回结果
    /// </summary>
    /// <typeparam name="T">T必须继承<see cref="TencentResponseBase"/></typeparam>
    public class TencentResultBase<T> where T : TencentResponseBase
    {
        /// <summary>
        /// 公共返回结果
        /// </summary>
        public T Response { get; set; }
    }
}