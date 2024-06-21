using Microsoft.EntityFrameworkCore;
using TaskManager.API.EF;
using TaskManager.API.Entities;

namespace TaskManager.API.Services
{
    public interface ITaaskService
    {
        Task<List<Taask>> GetAllTaasks();
        Task<Taask> GetTaaksByID(int id);
        Task<bool> AddTaask(Taask model);
        Task<bool> UpdateTaask(Taask model);
        Task<bool> DeleteTaask(Taask model);
    }


    public class TaaskService : ITaaskService
    {

        readonly AppDbContext dbContext;
        public TaaskService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<bool> AddTaask(Taask model)
        {
            dbContext.Tasks.Add(model);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTaask(Taask model)
        {
            model.IsDeleted = true;
            model.DeletedDate = DateTime.Now;
            dbContext.Update(model);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Taask>> GetAllTaasks()
        {
            return await dbContext.Tasks
                .Where(t => !t.IsDeleted)                
                .ToListAsync();
        }

        public async Task<Taask> GetTaaksByID(int id)
        {
            return await dbContext.Tasks                
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<bool> UpdateTaask(Taask model)
        {            
            dbContext.Update(model);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
