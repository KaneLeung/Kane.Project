#region << 版 本 注 释 >>
/*-----------------------------------------------------------------
* 项目名称 ：Kane.CloudApi.Tencent
* 项目描述 ：常用云服务Api
* 类 名 称 ：TencentTtsService
* 类 描 述 ：腾讯云Tts【语音合成】Api服务
* 所在的域 ：KK-HOME
* 命名空间 ：Kane.CloudApi.Tencent
* 机器名称 ：KK-HOME 
* CLR 版本 ：4.0.30319.42000
* 作　　者 ：Kane Leung
* 创建时间 ：2020/3/1 23:46:44
* 更新时间 ：2020/3/21 15:14:44
* 版 本 号 ：v1.0.4.0
*******************************************************************
* Copyright @ Kane Leung 2020. All rights reserved.
*******************************************************************
-----------------------------------------------------------------*/
#endregion
using Kane.Extension;
using System;
using System.Threading.Tasks;
#if NETCOREAPP3_0 ||　NETCOREAPP3_1
using Kane.Extension.Json;
#else
using Kane.Extension.JsonNet;
#endif

namespace Kane.CloudApi.Tencent
{
    /// <summary>
    /// 腾讯云Tts【语音合成】Api服务
    /// </summary>
    public class TencentTtsService : TencentService
    {
        #region 无参构造函数 + TencentTtsService()
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TencentTtsService()
        {
            ServiceHost = "tts.tencentcloudapi.com";
            XtcRegion = "ap-guangzhou";
            XtcVersion = "2019-08-23";
        }
        #endregion

        #region 构造函数 + TencentTtsService(string secretID, string secretKey) : this()
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="secretID">在云API密钥上申请的标识身份的 SecretID，一个 SecretID 对应唯一的 SecretKey</param>
        /// <param name="secretKey">SecretID 对应唯一的 SecretKey</param>
        public TencentTtsService(string secretID, string secretKey) : this()
        {
            SecretID = secretID;
            SecretKey = secretKey;
        }
        #endregion

        #region 语音合成 + TextToVoice(...)
        /// <summary>
        /// 语音合成
        /// </summary>
        /// <param name="text">合成语音的源文本，按UTF-8编码统一计算。</param>
        /// <param name="volume">音量大小，范围：[0，10]，分别对应11个等级的音量，默认为0，代表正常音量。没有静音选项。</param>
        /// <param name="speed">语速，范围：[-2，2]，分别对应不同语速：-2代表0.6倍，-1代表0.8倍，0代表1.0倍（默认），1代表1.2倍，2代表1.5倍，如果需要更细化的语速，可以保留小数点后一位，例如0.5 1.1 1.8等。</param>
        /// <param name="voice">音色，详情见Enum类</param>
        /// <param name="language">主语言类型：1-中文（默认）和 2-英文</param>
        /// <param name="rate">音频采样率</param>
        /// <param name="codec">返回音频格式，可取值：wav（默认），mp3</param>
        /// <returns></returns>
        public async Task<TencentTtsResult> TextToVoice(string text, int volume = 0, float speed = 0,
            Voice voice = Voice.Yunxiaoning,
            Language language = Language.Chinese,
            Rate rate = Rate.K16,
            Codec codec = Codec.Wav)
        {
            text.ThrowIfNull(nameof(text));
            if (volume < 0 || volume > 10) throw new ArgumentOutOfRangeException(nameof(volume), $"【{nameof(volume)}】超出[0 ~ 10]的范围。");
            if (speed < -2 || speed > 2) throw new ArgumentOutOfRangeException(nameof(speed), $"【{nameof(speed)}】超出[-2 ~ 2]的范围。");
            speed = ((int)(speed * 10)) / 10;
            var sendData = new
            {
                Text = text,
                SessionId = RandomHelper.UUID(),//一次请求对应一个SessionId，会原样返回，建议传入类似于uuid的字符串防止重复。
                ModelType = 1,//模型类型，1-默认模型。
                Volume = volume,
                Speed = speed,
                VoiceType = (int)voice,
                PrimaryLanguage = (int)language,
                SampleRate = (int)rate,
                Codec = codec.ToString().ToLower()
            };
            var result = await base.RequestService(sendData.ToJson(), "TextToVoice");
            if (result.success)
            {
                var data = result.message.ToObject<TencentTtsResult>();
                if (data.Response.Error.IsNull())
                {
                    data.Message = "Success";
                    return data;
                }
                else
                {
                    data.Success = false;
                    data.Message = data.Response.Error.Message;
                    return data;
                }
            }
            else return new TencentTtsResult() { Success = false, Message = result.message };
        }
        #endregion
    }
}