using GreenWhale.RunTime.Scripts;

namespace GreenWhale.Extensions.TestTools2.Extensions
{
    public static class EnumExtension
    {
        public static string Description(this State state)
        {
            switch (state)
            {
                case State.Qualified:
                    return "通过";
                case State.Unqualified:
                    return "拒绝";
                case State.None:
                    return "待执行";
                case State.FrameValidatePassed:
                    return "通过";
                case State.FrameValidateFailed:
                    return "无效包";
                default:
                    return "未知";
            }
        }
    }
}
