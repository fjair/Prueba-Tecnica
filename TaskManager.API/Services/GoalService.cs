using Microsoft.EntityFrameworkCore;
using TaskManager.API.EF;
using TaskManager.API.Entities;

namespace TaskManager.API.Services
{
    public interface IGoalService
    {
        Task<bool> AddGoal(Goal model);
        Task<bool> DeleteGoal(Goal model);
        Task<List<Goal>> GetAllGoals();
        Task<Goal> GetGoalByID(int id);
        Task<bool> UpdateGoal(Goal model);
    }

    public class GoalService: IGoalService
    {
        readonly AppDbContext dbContext;
        public GoalService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<bool> AddGoal(Goal model)
        {
            dbContext.Goals.Add(model);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteGoal(Goal model)
        {
            model.IsDeleted = true;
            model.DeletedDate = DateTime.Now;
            dbContext.Update(model);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Goal>> GetAllGoals()
        {
            return await dbContext.Goals
                .Where(x => !x.IsDeleted)
                .Include(x => x.TasksList)
                .ToListAsync();
        }     

        public async Task<Goal> GetGoalByID(int id)
        {
            return await dbContext.Goals
                .Include(x => x.TasksList)
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<bool> UpdateGoal(Goal model)
        {
            dbContext.Update(model);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
