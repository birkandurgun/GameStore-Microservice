namespace GameService.Application.Features.Games.Queries.GetGameDetails
{
    public class GetGameDetailsQueryResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PublisherName { get; set; }
        public List<string> Genres { get; set; } = new List<string>();
    }
}
