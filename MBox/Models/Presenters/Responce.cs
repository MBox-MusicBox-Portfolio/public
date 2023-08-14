namespace MBox.Models.Presenters;

public class Responce
{
    public bool Success { get; set; }
    public List<object> Value { get; set; }
    public List<object> Errors { get; set; }
    public int Status { get; set; }
    public Responce(List<object> value, List<object> errors, bool success = true)
    {
        Success = success;
        Value = value;
        Errors = errors;
    }
    public Responce()
    {
        Value = new();
        Errors = new();
    }
}