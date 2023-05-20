namespace SurveySheet.Repositories.Interfaces
{
    public interface ICheckedItemRepository
    {
        Task CheckItemAsync(int userId, int id);
    }
}
