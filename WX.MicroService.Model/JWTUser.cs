using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WX.MicroService.Model
{
    public class TokenManagement
    {
        /// <summary>
        /// 密钥
        /// </summary>
        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }

        [JsonProperty("accessExpiration")]
        public int AccessExpiration { get; set; }

        [JsonProperty("refreshExpiration")]
        public int RefreshExpiration { get; set; }
    }

    /// <summary>
    /// 请求的DTO类
    /// </summary>
    public class LoginRequestDTO
    {
        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }


        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }

}
