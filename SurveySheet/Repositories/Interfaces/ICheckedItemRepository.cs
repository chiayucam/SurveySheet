namespace SurveySheet.Repositories.Interfaces
{
    public interface ICheckedItemRepository
    {
        Task CheckItemAsync(int userId, int id);

        Task<IEnumerable<int>> GetCheckItemAsync(int userId, int start, int end);
    }
}
