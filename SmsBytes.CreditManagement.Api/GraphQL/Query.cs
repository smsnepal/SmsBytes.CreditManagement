using System.Threading.Tasks;
using Micro.GraphQL.Federation;
using SmsBytes.CreditManagement.Api.GraphQL.Types;
using SmsBytes.CreditManagement.Storage;

namespace SmsBytes.CreditManagement.Api.GraphQL
{
    public sealed class Query : Query<EntityType>
    {
        public Query()
        {
            Field<WeatherType, Weather>()
                .Name("weather")
                .ResolveAsync(x => Task.FromResult(new Weather
                {
                    Id = "id",
                    Temperature = 23.3
                }));
        }
    }
}
