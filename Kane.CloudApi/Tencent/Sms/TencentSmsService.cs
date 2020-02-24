#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：常用云服务Api
* 类 名 称 ：TencentSmsService
* 类 描 述 ：腾讯云发送短信Api服务
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/2/23 1:21:19
* 更新时间 ：2020/2/23 1:21:19
* 版 本 号 ：v1.0.0.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Kane.Extension;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kane.CloudApi.Tencent
{
    /// <summary>
    /// 腾讯云发送短信Api服务
    /// </summary>
    public class TencentSmsService : TencentService
    {
        #region 无参构造函数 + TencentSmsService()
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TencentSmsService()
        {
            ServiceHost = "sms.tencentcloudapi.com";
            XtcRegion = "ap-guangzhou";
            XtcVersion = "2019-07-11";
        }
        #endregion

        #region 构造函数 + TencentSmsService(string secretID, string secretKey) : this()
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="secretID">在云API密钥上申请的标识身份的 SecretID，一个 SecretID 对应唯一的 SecretKey</param>
        /// <param name="secretKey">SecretID 对应唯一的 SecretKey</param>
        public TencentSmsService(string secretID, string secretKey) : this()
        {
            SecretID = secretID;
            SecretKey = secretKey;
        }
        #endregion

        #region 发送短信方法，可发送多条 + SendSms(string smsSdkAppid, string templateID, IEnumerable<string> phoneNumbers, params string[] templateParams)
        /// <summary>
        /// 发送短信方法，可发送多条
        /// 单次请求最多支持200个手机号且要求全为境内手机号或全为境外手机号。
        /// </summary>
        /// <param name="smsSdkAppid">短信SdkAppid在 短信控制台 添加应用后生成的实际SdkAppid</param>
        /// <param name="templateID">模板 ID，必须填写已审核通过的模板 ID。模板ID可登录 短信控制台 查看。</param>
        /// <param name="phoneNumbers">下发手机号码，采用 e.164 标准，格式为+[国家或地区码][手机号]</param>
        /// <param name="templateParams">模板参数，若无模板参数，则设置为空。</param>
        /// <returns></returns>
        public async Task<TSendSmsResult> SendSms(string smsSdkAppid, string templateID, IEnumerable<string> phoneNumbers, params string[] templateParams)
        {
            var content = new
            {
                TemplateID = templateID,
                SmsSdkAppid = smsSdkAppid,
                PhoneNumberSet = phoneNumbers.Select(k => k.ToISPhoneNo()),
                TemplateParamSet = templateParams,
            };
            var result = await RequestService(content.ToJson());
            if (result.success)
            {
                var data = result.message.ToObject<TSendSmsResult>();
                if (data.Response.Error.IsNull() && data.Response.SendStatusSet.Length > 0)
                {
                    foreach (var item in data.Response.SendStatusSet)
                    {
                        if (item.Code == "Ok")
                        {
                            data.SuccessCount++;
                            data.SuccessNo.Add(item.PhoneNumber);
                        }
                        else
                        {
                            data.FailCount++;
                            data.FailNo.Add(item.PhoneNumber);
                            data.FailMessage.Add(item.Message);
                        }
                    }
                    if (data.FailCount > 0)
                    {
                        data.AllSuccess = false;
                        data.Message = $"成功数【{data.SuccessCount}】,失败数【{data.FailCount}】，详情请查看【FailMessage】";
                    }
                    else data.Message = $"All Success，成功数【{data.SuccessCount}】";
                    return data;
                }
                else
                {
                    data.AllSuccess = false;
                    data.Message = data.Response.Error.Message;
                    return data;
                }
            }
            else return new TSendSmsResult() { AllSuccess = false, Message = result.message };
        }
        #endregion

        /// <summary>
        /// 发送短信方法，发送单条
        /// </summary>
        /// <param name="smsSdkAppid">短信SdkAppid在 短信控制台 添加应用后生成的实际SdkAppid</param>
        /// <param name="templateID">模板 ID，必须填写已审核通过的模板 ID。模板ID可登录 短信控制台 查看。</param>
        /// <param name="phoneNumber">下发手机号码，采用 e.164 标准，格式为+[国家或地区码][手机号]</param>
        /// <param name="templateParams">模板参数，若无模板参数，则设置为空。</param>
        /// <returns></returns>
        public async Task<TSendSmsResult> SendSms(string smsSdkAppid, string templateID, string phoneNumber, params string[] templateParams)
            => await SendSms(smsSdkAppid, templateID, new string[] { phoneNumber }, templateParams);
    }
}