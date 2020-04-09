using System.Net.Mail;

namespace GreenWhale.BootLoader.Services
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="mailBody"></param>
        /// <param name="copyTos"></param>
        /// <returns></returns>
        Result Send(string[] to, string subject, string mailBody, params string[] copyTos);
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="tos"></param>
        /// <param name="subject"></param>
        /// <param name="mailBody"></param>
        /// <param name="ccs"></param>
        /// <param name="bccs"></param>
        /// <param name="priority"></param>
        /// <param name="attachments"></param>
        /// <returns></returns>
        Result Send(string[] tos, string subject, string mailBody, string[] ccs, string[] bccs, MailPriority priority, params Attachment[] attachments);
    }
}