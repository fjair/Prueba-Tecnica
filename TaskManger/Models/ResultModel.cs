namespace TaskManger.Models;

public class ResultModel<T>
{
    string message = string.Empty;
    public ResultModel()
    {
        Message = string.Empty;
    }
    public ResultModel(T data)
    {
        Data = data;
    }

    public bool IsSuccess { get; set; }
    public string Message { get; set; }    
    public T Data { get; set; }    
}
