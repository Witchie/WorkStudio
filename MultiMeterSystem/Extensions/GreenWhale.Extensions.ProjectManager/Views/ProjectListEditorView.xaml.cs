using DevExpress.Xpf.Grid;
using GreenWhale.ProjectManager;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
namespace GreenWhale.BootLoader.Implements.ProjectManager.Views
{
    /// <summary>
    /// ProjectListEditorView.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectListEditorView : UserControl
    {
        private readonly ProjectManagerDataSource projectManagerDataSource;
        public ProjectListEditorView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public ProjectListEditorView(ProjectManagerDataSource projectManagerDataSource):this()
        {
            this.projectManagerDataSource = projectManagerDataSource;
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public ObservableCollection<IProjectInfo> DataSources { get; private set; } = new ObservableCollection<IProjectInfo>();

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Query(object sender)
        {
            dxg.ListLoadData(s =>
            { 
                s.ItemsSource= projectManagerDataSource.ProjectInfos(cmb.Text);
            },()=> 
            {
                sender.Disable<UIElement>();
            },()=> 
            {
                sender.Enable<UIElement>();
            });
        }
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Query(sender);
        }

        private void queryText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                Query(sender);
            }
        }
    }
}
