using Amazon.DynamoDBv2.DataModel;

namespace LibraryService.Api.Models
{
    public class LibraryItem
    {
        [DynamoDBProperty]
        public Guid GameId { get; set; }
        [DynamoDBProperty]
        public string GameName { get; set; }
    }
}
