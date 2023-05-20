namespace SurveySheet.Controllers.Requests
{
    public class AddItemsRequest
    {
        public IEnumerable<Item> Items { get; set; }

        public class Item
        {
            public string Title { get; set; }
        }
    }
}
