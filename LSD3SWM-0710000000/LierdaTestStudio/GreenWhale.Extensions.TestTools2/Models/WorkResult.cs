namespace GreenWhale.Extensions.TestTools2.Views
{
    public class WorkResult
    {
        public string State { get; set; }
        public string WorkName { get; set; }
        public bool IsDefineValue { get; set; } = false;
        public string TestResult { get; set; }
        public override string ToString()
        {
            if (IsDefineValue)
            {
                return State;
            }
            if (string.IsNullOrEmpty(TestResult))
            {
                return $"【{State}】";
            }
            else
            {
                return $"【{State}】{TestResult}";
            }
        }
    }
}
