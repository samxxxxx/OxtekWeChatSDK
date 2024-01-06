
#region Version Info
/**************************************************************** 
**************************************************************** 
* 作    者：SAM
* 邮    箱:	support@oxetek.com
* CLR 版本：4.0.30319.42000
* 创建时间：2020-1-3 14:23:58
* 当前版本：1.0.0.0
* 
* 描述说明： 
* 
* 修改历史： 
* timestamp: 29e6bfbb-b1c6-4977-8152-78cd096987f6
****************************************************************** 
* Copyright @ SAM 2020 All rights reserved**********************/
#endregion

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OxetekWeChatSDK.Request
{
    public class WXACodeUnLimitRequest
    {
        public WXACodeUnLimitRequest()
        {
            Width = 430;
        }
        /// <summary>
        /// 最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）
        /// </summary>
        public string Scene { get; set; }
        /// <summary>
        /// 必须是已经发布的小程序存在的页面（否则报错），例如 pages/index/index, 根路径前不要填加 /,不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面
        /// </summary>
        public string Page { get; set; }
        /// <summary>
        /// 二维码的宽度，单位 px，最小 280px，最大 1280px
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 自动配置线条颜色，如果颜色依然是黑色，则说明不建议配置主色调，默认 false
        /// </summary>
        [JsonProperty("auto_color")]
        public bool AutoColor { get; set; }
        /// <summary>
        /// auto_color 为 false 时生效，使用 rgb 设置颜色 例如
        /// </summary>
        [JsonProperty("line_color")]
        public string LineColor { get; set; }
        /// <summary>
        /// 是否需要透明底色，为 true 时，生成透明底色的小程序
        /// </summary>
        [JsonProperty("is_hyaline")]
        public bool IsHyaline { get; set; }

    }
}
