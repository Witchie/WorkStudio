using System.Collections.Generic;

namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 文档修改信息
    /// </summary>
    public class DocumentModifyInfo : ArrayObject<ModifyInfo>
    {
        public DocumentModifyInfo()
        {
        }

        public DocumentModifyInfo(params ModifyInfo[] collection) : base(collection)
        {
        }

        public DocumentModifyInfo(int capacity) : base(capacity)
        {
        }
    }
}
