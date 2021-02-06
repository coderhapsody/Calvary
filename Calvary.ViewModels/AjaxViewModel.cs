namespace Calvary.ViewModels
{
    public class AjaxViewModel
    {
        public AjaxViewModel(bool isSuccess, object data, string message)
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
        }
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
