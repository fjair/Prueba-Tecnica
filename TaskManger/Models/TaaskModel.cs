using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.API.Entities;

namespace TaskManger.Models;

public class TaaskModel: BaseModel
{
    public int ID { get; set; }

    public string Name { get; set; }
    public bool IsCompleted { get; set; } = false;
    public bool IsFavorite { get; set; } = false;
   
    public int GoalID { get; set; }
    public Goal Goal { get; set; }
}
