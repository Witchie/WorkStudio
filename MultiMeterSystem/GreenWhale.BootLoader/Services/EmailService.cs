using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace GreenWhale.BootLoader.Services
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// 邮件配置信息
        /// </summary>
        private readonly EmailInfo emailInfo;
        /// <summary>
        /// 邮件帮助类
        /// </summary>
        /// <param name="emailInfo">邮件配置信息</param>
        public EmailService(EmailInfo emailInfo)
        {
            this.emailInfo = emailInfo;
        }

        /// <summary>
        /// 发送邮件到指定收件人
        /// </summary>
        /// <param name="to">收件人地址</param>
        /// <param name="subject">主题</param>
        /// <param name="mailBody">正文内容（支持HTML）</param>
        /// <param name="copyTos">抄送地址列表</param>
        /// <returns>是否发送成功</returns>
        public Result Send(string[] to, string subject, string mailBody, params string[] copyTos)
        {
            return Send(to, subject, mailBody, copyTos, new string[] { }, MailPriority.Normal);
        }

        /// <summary>
        /// 发送邮件到指定收件人
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:55 Created By iceStone
        /// </remarks>
        /// <param name="tos">收件人地址列表</param>
        /// <param name="subject">主题</param>
        /// <param name="mailBody">正文内容(支持HTML)</param>
        /// <param name="ccs">抄送地址列表</param>
        /// <param name="bccs">密件抄送地址列表</param>
        /// <param name="priority">此邮件的优先级</param>
        /// <param name="attachments">附件列表</param>
        /// <returns>是否发送成功</returns>
        /// <exception cref="System.ArgumentNullException">attachments</exception>
        public Result Send(string[] tos, string subject, string mailBody, string[] ccs, string[] bccs, MailPriority priority, params Attachment[] attachments)
        {
            try
            {
                if (tos.Length == 0)
                    return new ExceptionResult(new ArgumentException("收件人不得为空"));
                //创建Email实体
                var message = new MailMessage();
                message.From = new MailAddress(emailInfo.SmtpUsername, emailInfo.SmtpDisplayName);
                message.Subject = subject;
                message.Body = mailBody;
                message.HeadersEncoding = Encoding.UTF8;
                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Priority = priority;

                //插入附件
                foreach (var attachent in attachments)
                {
                    message.Attachments.Add(attachent);
                }
                //插入收件人地址，抄送地址和密件抄送地址
                foreach (var to in tos.Where(t => !string.IsNullOrEmpty(t)))
                {
                    message.To.Add(new MailAddress(to));
                }
                foreach (var cc in ccs.Where(c => !string.IsNullOrEmpty(c)))
                {
                    message.CC.Add(new MailAddress(cc));
                }
                foreach (var bcc in bccs.Where(bc => !string.IsNullOrEmpty(bc)))
                {
                    message.Bcc.Add(new MailAddress(bcc));
                }

                //创建Smtp客户端
                var client = new SmtpClient
                {
                    Host = emailInfo.SmtpServer,
                    Credentials = new NetworkCredential(emailInfo.SmtpUsername, emailInfo.SmtpUserPassword),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = emailInfo.SmtpEnableSsl,
                    Port = emailInfo.SmtpServerPort
                };

                //发送邮件
                client.Send(message);
                return new Result(true, "发送成功");
            }
            catch (Exception ex)
            {
                return new ExceptionResult(ex);
            }
        }
    }
}
