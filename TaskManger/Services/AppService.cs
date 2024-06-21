using System.Net;
using TaskManger.Models;
using TaskManger.RepositoryAPI;

namespace TaskManger.Services;

public interface IAppService
{
    // ------------------ FOR GOALS
    Task<ResultModel<List<GoalModel>>> GetAllGoals();
    Task<ResultModel<GoalModel>> GetGoalByID(int goalId);
    Task<ResultModel<bool>> AddGoal(GoalModel goalModel);
    Task<ResultModel<bool>> UpdateGoal(GoalModel goalModel);
    Task<ResultModel<bool>> DeleteGoal(GoalModel goalModel);

    // ----------------- FOR TAASKS
    Task<ResultModel<List<TaaskModel>>> GetAllTaasks();
    Task<ResultModel<TaaskModel>> GetTaaskByID(int taaskId);
    Task<ResultModel<bool>> AddTaask(TaaskModel taaskModel);
    Task<ResultModel<bool>> UpdateTaask(TaaskModel taaskModel);
    Task<ResultModel<bool>> DeleteTaask(TaaskModel taaskModel);
}


public class AppService: IAppService
{
    HttpClient httpClient;
    private IRepositoryApi repositoryApi;

    static string _baseURL = "https://localhost:7204/api"; //Cambiar puerto local del equipo donde se ejecuta


    public AppService(HttpClient httpClient, IRepositoryApi repositoryApi)
    {
        this.httpClient = httpClient;
        this.repositoryApi = repositoryApi;
    }

    #region METHODS FOR GOALS
    public async Task<ResultModel<List<GoalModel>>> GetAllGoals()
    {
        ResultModel<List<GoalModel>> result = new ResultModel<List<GoalModel>> { Data = new List<GoalModel>() };
        
        var response = await repositoryApi.Get<List<GoalModel>>($"{_baseURL}/Goal/GetAll");
        await ValidateResponse(result, response);

        return result;        
    }
    
    public async Task<ResultModel<GoalModel>> GetGoalByID(int goalId)
    {
        ResultModel<GoalModel> result = new ResultModel<GoalModel>();
        
        var response = await repositoryApi.Get<GoalModel>($"{_baseURL}/Goal/GetByID?id={goalId}");
        await ValidateResponse(result, response);

        return result;        
    }
    
    
    public async Task<ResultModel<bool>> AddGoal(GoalModel goalModel)
    {
        ResultModel<bool> result = new ResultModel<bool>();
        
        var response = await repositoryApi.Post<bool, GoalModel>($"{_baseURL}/Goal/Add", goalModel);
        await ValidateResponse(result, response);

        return result;        
    }
    
    public async Task<ResultModel<bool>> UpdateGoal(GoalModel goalModel)
    {
        ResultModel<bool> result = new ResultModel<bool>();
        
        var response = await repositoryApi.Put<bool, GoalModel>($"{_baseURL}/Goal/Update", goalModel);
        await ValidateResponse(result, response);

        return result;        
    }
    
    public async Task<ResultModel<bool>> DeleteGoal(GoalModel goalModel)
    {
        ResultModel<bool> result = new ResultModel<bool>();
        
        var response = await repositoryApi.Put<bool, GoalModel>($"{_baseURL}/Goal/Delete", goalModel);
        await ValidateResponse(result, response);

        return result;        
    }

    #endregion


    #region METHODS FOR GOALS
    public async Task<ResultModel<List<TaaskModel>>> GetAllTaasks()
    {
        ResultModel<List<TaaskModel>> result = new ResultModel<List<TaaskModel>> { Data = new List<TaaskModel>() };

        var response = await repositoryApi.Get<List<TaaskModel>>($"{_baseURL}/Taask/GetAll");
        await ValidateResponse(result, response);

        return result;
    }

    public async Task<ResultModel<TaaskModel>> GetTaaskByID(int taaskId)
    {
        ResultModel<TaaskModel> result = new ResultModel<TaaskModel>();

        var response = await repositoryApi.Get<TaaskModel>($"{_baseURL}/Taask/GetByID?id={taaskId}");
        await ValidateResponse(result, response);

        return result;
    }


    public async Task<ResultModel<bool>> AddTaask(TaaskModel taaskModel)
    {
        ResultModel<bool> result = new ResultModel<bool>();

        var response = await repositoryApi.Post<bool, TaaskModel>($"{_baseURL}/Taask/Add", taaskModel);
        await ValidateResponse(result, response);

        return result;
    }

    public async Task<ResultModel<bool>> UpdateTaask(TaaskModel taaskModel)
    {
        ResultModel<bool> result = new ResultModel<bool>();

        var response = await repositoryApi.Put<bool, TaaskModel>($"{_baseURL}/Taask/Update", taaskModel);
        await ValidateResponse(result, response);

        return result;
    }

    public async Task<ResultModel<bool>> DeleteTaask(TaaskModel taaskModel)
    {
        ResultModel<bool> result = new ResultModel<bool>();

        var response = await repositoryApi.Put<bool, TaaskModel>($"{_baseURL}/Taask/Delete", taaskModel);
        await ValidateResponse(result, response);

        return result;
    }


    #endregion

    private async Task ValidateResponse<T>(ResultModel<T> resultModel, HttpResponseResult<T> responseApi)
    {
        if (responseApi.Error)
        {
            resultModel.IsSuccess = !responseApi.Error;

            switch (responseApi.HttpResponseMessage.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    resultModel.Message = await responseApi.ErrorMessage();
                    break;
                case HttpStatusCode.NotFound:
                    resultModel.Message = "No se encontraron registros";
                    break;
                default:
                    resultModel.Message = await responseApi.ErrorMessage();
                    break;
            }
        }
        else
        {
            resultModel.IsSuccess = true;
            resultModel.Data = responseApi.Response;
        }
    }
}
