using Amazon.DynamoDBv2.DataModel;

namespace LibraryService.Api.Models
{
    [DynamoDBTable("UserGames")]
    public class UserGames
    {
        [DynamoDBHashKey(AttributeName = "userId")]
        public Guid UserId { get; set; }
        
        [DynamoDBProperty]
        public List<LibraryItem> Games { get; set; }
    }
}
