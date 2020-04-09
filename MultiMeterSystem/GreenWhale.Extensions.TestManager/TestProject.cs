using System.Text;
using System.Linq;
namespace GreenWhale.Extensions.TestManager
{
    /// <summary>
    /// 测试项目
    /// </summary>
    public class TestProject<TTestPlan> where TTestPlan: TestPlan
    {
        /// <summary>
        /// 公司信息
        /// </summary>
        public CompanyInfo CompanyInfo { get; set; } = new CompanyInfo();
        /// <summary>
        /// 项目名称信息
        /// </summary>
        public DocumentNameInfo DocumentNameInfo { get;  set; } = new DocumentNameInfo();
        /// <summary>
        /// 项目标题信息
        /// </summary>
        public ProjectTitleInfo ProjectTitleInfo { get;  set; } = new ProjectTitleInfo();
        /// <summary>
        /// 文档修改信息
        /// </summary>
        public DocumentModifyInfo DocumentModifyInfo { get;  set; } = new DocumentModifyInfo();
        /// <summary>
        /// 描述信息
        /// </summary>
        public DescriptionInfo DescriptionInfo { get;  set; } = new DescriptionInfo();
        /// <summary>
        /// 测试需求分析
        /// </summary>
        public TestAnalysis TestAnalysis { get;  set; } = new TestAnalysis();
        /// <summary>
        /// 测试计划
        /// </summary>
        public TestPlansCore<TTestPlan> TestPlans { get; set; } = new TestPlansCore<TTestPlan>();
    }
}
