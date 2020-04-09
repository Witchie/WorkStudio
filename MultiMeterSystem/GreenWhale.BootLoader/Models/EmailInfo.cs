namespace GreenWhale.BootLoader.Services
{
    /// <summary>
    /// 邮件信息
    /// </summary>
    public class EmailInfo
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string SmtpServer { get; set; }
        /// <summary>
        /// SMTP端口
        /// </summary>
        public int SmtpServerPort { get; set; }
        /// <summary>
        /// 是否气用SSL
        /// </summary>
        public bool SmtpEnableSsl { get; set; }
        /// <summary>
        /// 发件人邮箱地址
        /// </summary>
        public string SmtpUsername { get; set; }
        /// <summary>
        /// 发件人昵称
        /// </summary>
        public string SmtpDisplayName { get; set; }
        /// <summary>
        /// 授权码
        /// </summary>
        public string SmtpUserPassword { get; set; }
    }
}
