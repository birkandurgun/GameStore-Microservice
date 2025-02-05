using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using BasketService.Repositories.DynamoDB;
using BasketService.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BasketService.Repositories
{
    public static class ServiceRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

            services.AddTransient<IBasketRepository, DynamoDBBasketRepository>();
        }
    }
}
