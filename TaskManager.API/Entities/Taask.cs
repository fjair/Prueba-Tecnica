using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.Entities;

[Table("tblTasks")]
public class Taask: BaseEntity
{
    [Key]
    public int ID { get; set; }

    public string Name { get; set; }
    public bool IsCompleted { get; set; } = false;
    public bool IsFavorite { get; set; } = false;

    [ForeignKey("GoalID")]
    public int GoalID { get; set; }
    [NotMapped]
    public Goal Goal { get; set; }
}
