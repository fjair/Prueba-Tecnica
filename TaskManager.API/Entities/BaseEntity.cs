namespace TaskManager.API.Entities;

public class BaseEntity
{
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedDate { get; set; }
}
