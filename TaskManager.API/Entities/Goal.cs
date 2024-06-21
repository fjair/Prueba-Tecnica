using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.API.Entities;

[Table("tblGoals")]
public class Goal:BaseEntity
{
    [Key]
    public int ID { get; set; }

    public string Name { get; set; }

    public List<Taask> TasksList { get; set; } = new();
}
