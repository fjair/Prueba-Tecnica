namespace TaskManger.Models;

public class BaseModel
{
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedDate { get; set; }
}
