using GreenWhale.Extensions.TestTools2.ViewModels;
using System;
using System.Threading.Tasks;

namespace GreenWhale.Extensions.TestTools2.Models
{
    public interface IProjectViewServcie
    {
        Task<ProjectViewModel> ReadModel(string fileName);
        Task ReadModelDialog(Action<ProjectViewModel> onComplete);
        Task WriteModel(ProjectViewModel projectViewModel, string fileName);
        Task WriteModelDialog(ProjectViewModel viewmodel);
    }
}