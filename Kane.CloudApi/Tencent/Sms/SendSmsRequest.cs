#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent.Sms
* 项目描述 ：
* 类 名 称 ：SendSmsRequest
* 类 描 述 ：
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent.Sms
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/21 23:04:13
* 更新时间 ：2020/2/21 23:04:13
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace Kane.CloudApi.Tencent.Sms
{
    public class SendSmsRequest
    {
        /// <summary>
        /// 短信SdkAppid在 [短信控制台](https://console.cloud.tencent.com/sms/smslist)  添加应用后生成的实际SdkAppid，示例如1400006666。
        /// </summary>
        public string SmsSdkAppid { get; set; }
        /// <summary>
        /// 模板 ID，必须填写已审核通过的模板 ID。模板ID可登录 [短信控制台](https://console.cloud.tencent.com/sms/smslist) 查看。
        /// </summary>
        public string TemplateID { get; set; }
        /// <summary>
        /// 下发手机号码，采用 e.164 标准，+[国家或地区码][手机号] ，示例如：+8613711112222， 其中前面有一个+号 ，86为国家码，13711112222为手机号，最多不要超过200个手机号。
        /// </summary>
        public IEnumerable<string> PhoneNumberSet { get; set; }
        /// <summary>
        /// 模板参数，若无模板参数，则设置为空。
        /// </summary>
        public IEnumerable<string> TemplateParamSet { get; set; }
    }
}