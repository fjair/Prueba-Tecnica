using Blazorise.DataGrid;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using TaskManger.Components.Shared;
using TaskManger.Models;
using TaskManger.Services;


namespace TaskManger.Pages;

public partial class IndexTasks
{
    [Inject] IAppService AppService { get; set; }
    [Inject] MessageService MessageService { get; set; }
    
    List<TaaskModel> TaasksList = new();
    List<GoalModel> GoalsList = new();

    bool Loading;

    protected override async Task OnInitializedAsync()
    {
        Loading = true;

        await LoadGoals();
        await LoadTaasks();        

        Loading = false;
    }

    async Task LoadGoals()
    {
        var result = await AppService.GetAllGoals();
        if (result.IsSuccess)
            GoalsList = result.Data ?? new();
        else
            ErrorMessage(result.Message);
    }
    
    async Task LoadTaasks()
    {
        var result = await AppService.GetAllTaasks();
        if (result.IsSuccess)
            TaasksList = result.Data ?? new();
        else
            ErrorMessage(result.Message);
    }    


    void SuccessMessage()
    {
        MessageService.ShowNotification($"Registro Guardado", Enums.NotificationEnum.Success);
    }
    
    void InfoMessage()
    {
        MessageService.ShowNotification($"Registro Actualizado", Enums.NotificationEnum.Info);
    }
    
    void WarningMessage(string message)
    {
        MessageService.ShowNotification($"{message}", Enums.NotificationEnum.Warning);
    }
    
    void DeleteMessage()
    {
        MessageService.ShowNotification($"Registro Eliminado", Enums.NotificationEnum.Warning);
    }
    
    void ErrorMessage(string message)
    {
        MessageService.ShowNotification($"Ocurrio un Error: {message}", Enums.NotificationEnum.Error);
    }


    #region FOR GOALS
    GoalModel GoalModel = new();    
    GoalModel GoalSelected = new();    
    Modal ModalGoalRef;
    DeleteModal DeleteModalRef;
    Validations ValidationsRef;

    string GoalSelectedID = string.Empty;
    void OnItemSelectedChanged(string selectedID)
    {
        GoalSelectedID = selectedID;
        if (int.TryParse(GoalSelectedID, out int goalId))
        {
            GoalSelected = GoalsList.FirstOrDefault(x => x.ID == goalId);
        }
    }

    Task AddGoalModal()
    {
        GoalModel = new();
        return ModalGoalRef.Show();
    }
    
    Task UpdateGoalModal(GoalModel model)
    {
        GoalModel = model;
        return ModalGoalRef.Show();
    }
    
    async Task SaveGoal()
    {
        if (await ValidationsRef.ValidateAll())
        {
            if (!GoalsList.Any(x => x.Name == GoalModel.Name))
            {

                if (GoalModel.ID == 0)
                {
                    var resultAdd = await AppService.AddGoal(GoalModel);
                    if (resultAdd.IsSuccess)
                        SuccessMessage();
                    else
                        ErrorMessage(resultAdd.Message);
                }
                else
                {
                    var resultUpdate = await AppService.UpdateGoal(GoalModel);
                    if (resultUpdate.IsSuccess)
                        InfoMessage();
                    else
                        ErrorMessage(resultUpdate.Message);
                }

                await ModalGoalRef.Hide();
                await LoadGoals();
            }
            else
                WarningMessage("Ya existe esta Meta!");
        }
    }

    Task DeleteGoalModal(GoalModel model)
    {
        GoalModel = model;
        return DeleteModalRef.Open();
    }

    async Task OnDeleteGoal(GoalModel model)
    {
        var resultDelete = await AppService.DeleteGoal(model);
        if (resultDelete.IsSuccess)
        {
            DeleteMessage();
            GoalSelected = null;
            GoalSelectedID = string.Empty;
        }
        else
            ErrorMessage(resultDelete.Message);

        await DeleteModalRef.Close();
        await LoadGoals();
    }

    #endregion


    #region FOR TAASKS
    TaaskModel TaaskModel = new();
    TaaskModel TaaskSelected = new();    
    Modal ModalTaaskRef;
    DeleteModal DeleteModalRef2;
    Validations ValidationsRef2;    

    Task AddTaaskModal()
    {
        TaaskModel = new();
        return ModalTaaskRef.Show();
    }

    Task UpdateTaaskModal(TaaskModel model)
    {
        TaaskModel = model;
        return ModalTaaskRef.Show();
    }

    async Task SaveTaask()
    {
        if (await ValidationsRef2.ValidateAll())
        {
            if (!TaasksList.Any(X => X.Name == TaaskModel.Name))
            {
                TaaskModel.GoalID = GoalSelected.ID;
                if (TaaskModel.ID == 0)
                {
                    var resultAdd = await AppService.AddTaask(TaaskModel);
                    if (resultAdd.IsSuccess)
                        SuccessMessage();
                    else
                        ErrorMessage(resultAdd.Message);
                }
                else
                {                    
                    var resultUpdate = await AppService.UpdateTaask(TaaskModel);
                    if (resultUpdate.IsSuccess)
                        InfoMessage();
                    else
                        ErrorMessage(resultUpdate.Message);
                }
                
                await ModalTaaskRef.Hide();
                await LoadGoals();
                StateHasChanged();
            }
            else
                WarningMessage("Ya existe esta Tarea!");
        }
    }


    async Task OnCompleteTask(TaaskModel model)
    {
        model.IsCompleted = true;
        var resultUpdate = await AppService.UpdateTaask(model);

        if (resultUpdate.IsSuccess)
        {
            
            await LoadGoals();
            InfoMessage();
        }
        else
            ErrorMessage(resultUpdate.Message);
    }  

    Task DeleteTaaskModal(TaaskModel model)
    {
        TaaskModel = model;
        return DeleteModalRef.Open();
    }

    async Task OnDeleteTaaskl(TaaskModel model)
    {
        var resultDelete = await AppService.DeleteTaask(model);
        if (resultDelete.IsSuccess)
            DeleteMessage();
        else
            ErrorMessage(resultDelete.Message);

        await DeleteModalRef2.Close();
        await LoadGoals();
    }

    #endregion


    List<TaaskModel> selectedTaasks = new();

    private bool RowSelectableHandler(RowSelectableEventArgs<TaaskModel> rowSelectableEventArgs)
        => rowSelectableEventArgs.SelectReason is not DataGridSelectReason.RowClick;
}
