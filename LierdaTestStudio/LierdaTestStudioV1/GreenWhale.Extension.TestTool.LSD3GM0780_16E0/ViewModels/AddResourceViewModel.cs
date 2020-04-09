using GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Models;
namespace GreenWhale.Extension.TestTool.LSD3GM0780_16E0.Views
{
    public class AddResourceViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ValueModel Success { get; set; }
        public string SuccessValue { get; set; }
        public ValueModel Failed { get; set; }
        public string FailedValue { get; set; }
        public static implicit operator ResourceDefine<ValueModel, ValueModel>(AddResourceViewModel addResourceViewModel)
        {
            return new ResourceDefine<ValueModel, ValueModel>() {Number=addResourceViewModel.ID,OnFailed=addResourceViewModel.Failed,OnSuccess=addResourceViewModel.Success,Name=addResourceViewModel.Name };
        }
        public static implicit operator AddResourceViewModel(ResourceDefine<ValueModel, ValueModel> addResourceViewModel)
        {
            return new AddResourceViewModel() { ID = addResourceViewModel.Number, Failed = addResourceViewModel.OnFailed, Name = addResourceViewModel.Name, Success = addResourceViewModel.OnSuccess };
        }
    }
}
