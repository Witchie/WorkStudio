using System;

namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models
{
    /// <summary>
    /// 带值模型
    /// </summary>
    public class ValueModel : ModelBase
    {
        /// <summary>
        /// 值列表
        /// </summary>
        public ValueCompare Value { get; set; }
    }
    public class ValueCompare
    {
        public ValueCompare(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
        public static implicit operator ValueCompare(string key)
        {
            return new ValueCompare(key, string.Empty);
        }
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Value))
            {
                return $"{Key}";

            }
            else
            {
                return $"{Key}:{Value}";
            }
        }
    }
}
