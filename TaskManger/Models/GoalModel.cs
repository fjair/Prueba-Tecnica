using TaskManager.API.Entities;

namespace TaskManger.Models;

public class GoalModel: BaseModel
{
    public int ID { get; set; }

    public string Name { get; set; }

    public List<TaaskModel> TasksList { get; set; } = new();

    public int ProgressValue
    {
        get
        {
            int total = TasksList.Count;
            if (total == 0)
            {
                return 0; // Para evitar división por cero
            }

            int completedCount = TasksList.Count(x => x.IsCompleted);
            int progress = (completedCount * 100) / total;

            return progress;
        }
    }
}
