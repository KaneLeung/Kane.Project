#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：常用云服务Api
* 类 名 称 ：TencentResponseBase
* 类 描 述 ：公共返回结果实体模型基类
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/23 0:43:53
* 更新时间 ：2020/2/23 0:43:53
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion

namespace Kane.CloudApi.Tencent
{
    /// <summary>
    /// 公共返回结果
    /// </summary>
    public class TencentResponseBase
    {
        /// <summary>
        /// Error 的出现代表着该请求调用失败。Error 字段连同其内部的 Code 和 Message 字段在调用失败时是必定返回的。
        /// </summary>
        public Error Error { get; set; }
        /// <summary>
        /// 用于一个 API 请求的唯一标识，如果 API 出现异常，可以联系我们，并提供该 ID 来解决问题
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// TotalCount 为 DescribeInstancesStatus 接口定义的字段，由于调用请求的用户暂时还没有云服务器实例，因此 TotalCount 在此情况下的返回值为 0
        /// </summary>
        public int TotalCount { get; set; }
    }

    /// <summary>
    /// 错误返回结果
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Code 表示具体出错的错误码，当请求出错时可以先根据该错误码在公共错误码和当前接口对应的错误码列表里面查找对应原因和解决方案
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Message 显示出了这个错误发生的具体原因，随着业务发展或体验优化，此文本可能会经常保持变更或更新，用户不应依赖这个返回值。
        /// </summary>
        public string Message { get; set; }
    }
}