namespace AdministrationWebApi.Models.Presenter
{
    public class ResponsePresenter
    {
        public bool Success { get; set; }
        public List<object> Value { get; set; }
        public List<object> Errors { get; set; }
        public int Status { get; set; }
        public ResponsePresenter(List<object> value, List<object> errors, bool success = true)
        {
            Success = success;
            Value = value;
            Errors = errors;
        }
        public ResponsePresenter()
        {
            Value = new();
            Errors = new();
        }
    }
}
