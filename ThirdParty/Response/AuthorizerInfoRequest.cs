using OxetekWeChatSDK.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.ThirdParty.Response
{
    public class AuthorizerInfoResponse : ErrorMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public AuthorizerInfo Authorizer_Info { get; set; }
        public AuthorizationInfo Authorization_Info { get; set; }
    }

    public class AuthorizerInfo
    {
        /// <summary>
        /// 授权方昵称
        /// </summary>
        public string Nick_Name { get; set; }
        /// <summary>
        /// 授权方头像
        /// </summary>
        public string Head_Img { get; set; }
        /// <summary>
        /// 授权方公众号类型，0代表订阅号，1代表由历史老帐号升级后的订阅号，2代表服务号
        /// </summary>
        public ServiceTypeInfo Service_Type_Info { get; set; }
        /// <summary>
        /// 授权方认证类型，-1代表未认证，0代表微信认证，1代表新浪微博认证，2代表腾讯微博认证，3代表已资质认证通过但还未通过名称认证，4代表已资质认证通过、还未通过名称认证，但通过了新浪微博认证，5代表已资质认证通过、还未通过名称认证，但通过了腾讯微博认证
        /// </summary>
        public VerifyTypeInfo Verify_Type_Info { get; set; }
        /// <summary>
        /// 授权方公众号的原始ID
        /// </summary>
        public string User_Name { get; set; }
        /// <summary>
        /// 授权方公众号所设置的微信号，可能为空
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 二维码图片的URL，开发者最好自行也进行保存
        /// </summary>
        public string QRCode_Url { get; set; }
        /// <summary>
        /// 用以了解以下功能的开通状况（0代表未开通，1代表已开通）： open_store:是否开通微信门店功能 open_scan:是否开通微信扫商品功能 open_pay:是否开通微信支付功能 open_card:是否开通微信卡券功能 open_shake:是否开通微信摇一摇功能
        /// </summary>
        public BusinessInfo Business_Info { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Idc { get; set; }
        /// <summary>
        /// 公众号的主体名称
        /// </summary>
        public string Principal_Name { get; set; }
        /// <summary>
        /// 帐号介绍
        /// </summary>
        public string Signature { get; set; }
    }
    public class HasId
    {
        public int Id { get; set; }
    }
    public class ServiceTypeInfo : HasId
    {

    }
    public class VerifyTypeInfo : HasId
    {

    }
    public class BusinessInfo
    {
        public int Open_Pay { get; set; }
        public int Open_Shake { get; set; }
        public int Open_Scan { get; set; }
        public int Open_Card { get; set; }
        public int Open_Store { get; set; }
    }
}
