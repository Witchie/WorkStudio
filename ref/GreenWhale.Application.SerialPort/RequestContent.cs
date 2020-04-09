namespace GreenWhale.Application.SerialPorts
{
    /// <summary>
    /// 请求的内容
    /// </summary>
    public class RequestContent:RequestContentBase
    {
        /// <summary>
        /// 请求的内容
        /// </summary>
        public RequestContent()
        {
          
        }
        /// <summary>
        /// 请求的内容
        /// </summary>
        /// <param name="content"></param>
        public static implicit operator RequestContent(string msg)
        {
            var content= new RequestContent();
            content.LoadContent(msg);
            return content;
        }
        public static implicit operator string(RequestContent content)
        {
            return content.Content;
        }
        /// <summary>
        /// 是否具有开始符
        /// </summary>
        public bool IsStart 
        {
            get
            {
                if (Content.StartsWith("<"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 是否具有结束符
        /// </summary>
        public bool IsEnd
        {
            get
            {
                if (Content.StartsWith("<"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 是否具有传输值
        /// </summary>
        public bool HasValue => Content.Contains("+");
    }
 
}
