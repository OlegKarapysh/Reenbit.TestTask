namespace TestTask.WebAPI.Services.AzureFunctionTriggerService;

public interface IAzureFunctionTriggerService
{
    Task CallEmailNotificationFunction(Uri fileUri, string email);
}