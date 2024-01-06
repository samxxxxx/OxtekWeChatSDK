using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OxetekWeChatSDK.Response;
using OxetekWeChatSDK.Request;
using OxetekWeChatSDK.ThirdParty.Request;
using OxetekWeChatSDK.ThirdParty.Response;

namespace OxetekWeChatSDK.ThirdParty
{
    public class ComponentOAuth
    {
        public static DateTime LastUpdate { private set; get; }
        public static bool IsExpired
        {
            get
            {
                //小于当前时间表示过期
                return DateTime.Now.AddSeconds(ExpiresIn) <= DateTime.Now;
            }
        }
        public static double ExpiresIn
        {
            private set; get;
        }
        private static string _accessToken;

        public static string AccessToken
        {
            get
            {
                if (IsExpired)
                {
                    throw new Exception("session key has expired");
                }

                return _accessToken;
            }
        }

        /// <summary>
        /// 获取第三方平台component_access_token
        /// 第三方平台component_access_token是第三方平台的下文中接口的调用凭据，也叫做令牌（component_access_token）。每个令牌是存在有效期（2小时）的，且令牌的调用不是无限制的，请第三方平台做好令牌的管理，在令牌快过期时（比如1小时50分）再进行刷新。
        /// </summary>
        /// <param name="component_appid">第三方平台appid</param>
        /// <param name="component_appsecret">第三方平台appsecret</param>
        /// <param name="component_verify_ticket">微信后台推送的ticket，此ticket会定时推送，具体请见本页的推送说明</param>
        /// <returns></returns>
        public static async Task<string> GetAccessToken(string component_appid, string component_appsecret, string component_verify_ticket)
        {
            //https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1453779503&token=&lang=
            var url = $"https://api.weixin.qq.com/cgi-bin/component/api_component_token";

            if (!IsExpired)
            {
                return AccessToken;
            }

            var data = new ComponentAccessTokenRequest()
            {
                Component_AppId = component_appid,
                Component_AppSecret = component_appsecret,
                Component_Verify_Ticket = component_verify_ticket
            };
            var res = await Utils.HttpUtil.Post(url, data, new JsonSerializerSettings()
            {
                ContractResolver = new LowerPropertyNameJsonSerializer()
            });

            if (string.IsNullOrWhiteSpace(res))
            {
                return res;
            }

            var resEntity = JsonConvert.DeserializeObject<ComponentAccessTokenResponse>(res);// res.ToObject<ComponentAccessTokenResponse>();// Newtonsoft.Json.JsonConvert.DeserializeObject<AccessTokenResponse>(res);

            LastUpdate = DateTime.Now;
            ExpiresIn = resEntity.Expires_In;
            _accessToken = resEntity.Component_Access_Token;

            return resEntity.Component_Access_Token;
        }

        /// <summary>
        /// 获取预授权码pre_auth_code 该API用于获取预授权码。预授权码用于公众号或小程序授权时的第三方平台方安全验证。
        /// </summary>
        /// <param name="component_access_token"></param>
        /// <param name="component_appid">第三方平台方appid</param>
        /// <returns></returns>
        public static async Task<string> GetPreAuthCode(string component_access_token, string component_appid)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode?component_access_token={component_access_token}";
            var data = new PreAuthCodeRequest()
            {
                Component_AppId = component_appid
            };

            var res = await Utils.HttpUtil.Post(url, data, new JsonSerializerSettings()
            {
                ContractResolver = new LowerPropertyNameJsonSerializer()
            });

            if (string.IsNullOrWhiteSpace(res))
            {
                return res;
            }

            var resEntity = JsonConvert.DeserializeObject<PreAuthCodeReponse>(res);// res.ToObject<PreAuthCodeReponse>();
            return resEntity.Pre_Auth_Code;
        }

        /// <summary>
        /// 使用授权码换取公众号或小程序的接口调用凭据和授权信息
        /// 该API用于使用授权码换取授权公众号或小程序的授权信息，并换取authorizer_access_token和authorizer_refresh_token。 授权码的获取，需要在用户在第三方平台授权页中完成授权流程后，在回调URI中通过URL参数提供给第三方平台方。请注意，由于现在公众号或小程序可以自定义选择部分权限授权给第三方平台，因此第三方平台开发者需要通过该接口来获取公众号或小程序具体授权了哪些权限，而不是简单地认为自己声明的权限就是公众号或小程序授权的权限。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component_access_token"></param>
        /// <param name="component_appid">第三方平台appid</param>
        /// <param name="authorization_code">授权code,会在授权成功时返回给第三方平台，详见第三方平台授权流程说明</param>
        /// <returns></returns>
        public static async Task<T> QueryAuth<T>(string component_access_token, string component_appid, string authorization_code) where T : class
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/component/api_query_auth?component_access_token={component_access_token}";
            var data = new QueryAuthRequest()
            {
                Component_AppId = component_appid,
                Authorization_Code = authorization_code
            };

            var res = await Utils.HttpUtil.Post(url, data, new JsonSerializerSettings()
            {
                ContractResolver = new LowerPropertyNameJsonSerializer()
            });

            if (string.IsNullOrWhiteSpace(res))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(res);// res.ToObject<T>();
        }

        /// <summary>
        /// 获取（刷新）授权公众号或小程序的接口调用凭据（令牌）
        /// 该API用于在授权方令牌（authorizer_access_token）失效时，可用刷新令牌（authorizer_refresh_token）获取新的令牌。请注意，此处token是2小时刷新一次，开发者需要自行进行token的缓存，避免token的获取次数达到每日的限定额度
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component_access_token"></param>
        /// <param name="component_appid">第三方平台appid</param>
        /// <param name="authorizer_appid">授权方appid</param>
        /// <param name="authorizer_refresh_token">授权方的刷新令牌，刷新令牌主要用于第三方平台获取和刷新已授权用户的access_token，只会在授权时刻提供，请妥善保存。一旦丢失，只能让用户重新授权，才能再次拿到新的刷新令牌</param>
        /// <returns></returns>
        public static async Task<T> RefreshToken<T>(string component_access_token, string component_appid, string authorizer_appid, string authorizer_refresh_token)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/component/api_authorizer_token?component_access_token={component_access_token}";
            var data = new RefreshTokenRequest()
            {
                Component_AppId = component_appid,
                Authorizer_AppId = authorizer_appid,
                Authorizer_Refresh_Token = authorizer_refresh_token
            };

            var res = await Utils.HttpUtil.Post(url, data, new JsonSerializerSettings()
            {
                ContractResolver = new LowerPropertyNameJsonSerializer()
            });

            if (string.IsNullOrWhiteSpace(res))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(res);// res.ToObject<T>();
        }

        /// <summary>
        /// 获取授权方的帐号基本信息
        /// 该API用于获取授权方的基本信息，包括头像、昵称、帐号类型、认证类型、微信号、原始ID和二维码图片URL。
        /// 需要特别记录授权方的帐号类型，在消息及事件推送时，对于不具备客服接口的公众号，需要在5秒内立即响应；而若有客服接口，则可以选择暂时不响应，而选择后续通过客服接口来发送消息触达粉丝。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component_access_token"></param>
        /// <param name="component_appid">第三方平台appid</param>
        /// <param name="authorizer_appid">授权方appid</param>
        /// <returns></returns>
        public static async Task<T> GetAuthorizerInfo<T>(string component_access_token, string component_appid, string authorizer_appid)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/component/api_get_authorizer_info?component_access_token={component_access_token}";
            var data = new AuthorizerInfoRequest()
            {
                Component_AppId = component_appid,
                Authorizer_AppId = authorizer_appid
            };

            var res = await Utils.HttpUtil.Post(url, data, new JsonSerializerSettings()
            {
                ContractResolver = new LowerPropertyNameJsonSerializer()
            });

            if (string.IsNullOrWhiteSpace(res))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(res);// res.ToObject<T>();
        }
        /// <summary>
        /// 获取授权方的选项设置信息
        /// 该API用于获取授权方的公众号或小程序的选项设置信息，如：地理位置上报，语音识别开关，多客服开关。注意，获取各项选项设置信息，需要有授权方的授权，详见权限集说明
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component_appid">第三方平台appid</param>
        /// <param name="authorizer_appid">授权公众号或小程序的appid</param>
        /// <param name="option_name">选项名称</param>
        /// <returns></returns>
        public static async Task<T> GetAuthorizerOption<T>(string component_access_token, string component_appid, string authorizer_appid, string option_name)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/component/api_get_authorizer_option?component_access_token={component_access_token}";
            var data = new AuthorizerOptionRequest()
            {
                Component_AppId = component_appid,
                Authorizer_AppId = authorizer_appid,
                Option_Name = option_name
            };

            var res = await Utils.HttpUtil.Post(url, data, new JsonSerializerSettings()
            {
                ContractResolver = new LowerPropertyNameJsonSerializer()
            });

            if (string.IsNullOrWhiteSpace(res))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(res);// res.ToObject<T>();
        }

        /// <summary>
        /// 设置授权方的选项信息
        /// 该API用于设置授权方的公众号或小程序的选项信息，如：地理位置上报，语音识别开关，多客服开关。注意，设置各项选项设置信息，需要有授权方的授权，详见权限集说明。
        /// </summary>
        /// <param name="component_access_token"></param>
        /// <param name="component_appid">第三方平台appid</param>
        /// <param name="authorizer_appid">授权公众号或小程序的appid</param>
        /// <param name="option_name">选项名称</param>
        /// <param name="option_value">设置的选项值</param>
        /// <returns></returns>
        public static async Task<T> SetAuthorizerOption<T>(string component_access_token, string component_appid, string authorizer_appid, string option_name, string option_value)
        {
            var url = $"https://api.weixin.qq.com/cgi-bin/component/api_set_authorizer_option?component_access_token={component_access_token}";
            var data = new SetAuthorizerOptionRequest()
            {
                Component_AppId = component_appid,
                Authorizer_AppId = authorizer_appid,
                Option_Name = option_name,
                Option_Value = option_value
            };

            var res = await Utils.HttpUtil.Post(url, data, new JsonSerializerSettings()
            {
                ContractResolver = new LowerPropertyNameJsonSerializer()
            });

            if (string.IsNullOrWhiteSpace(res))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(res);// res.ToObject<T>();
        }
    }
}
