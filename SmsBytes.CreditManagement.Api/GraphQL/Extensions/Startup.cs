using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Execution;
using GraphQL.Server;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using Micro.GraphQL.Federation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SmsBytes.CreditManagement.Api.GraphQL.Directives;
using SmsBytes.CreditManagement.Api.GraphQL.Inputs;
using SmsBytes.CreditManagement.Api.GraphQL.Types;

namespace SmsBytes.CreditManagement.Api.GraphQL.Extensions
{
    public static class Startup
    {
        public static void ConfigureGraphql(this IServiceCollection services)
        {
            services.EnableFederation<EntityType>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<IDocumentExecutionListener, DataLoaderDocumentListener>();
            services.AddTransient<ISchema, StarterSchema>();
            services.AddTransient<EnumerationGraphType<Storage.AccountType>, AccountTypeEnum>();
            services.AddTransient<EnumerationGraphType<Storage.TransactionType>, TransactionTypeEnum>();
            services.AddTransient<TransactionType>();
            services.AddTransient<TopupInputType>();
            services.AddTransient<UserType>();
            services.AddTransient<DeductionInputType>();
            services.AddTransient<Query>();
            services.AddTransient<Mutation>();
            services.AddScoped<AuthorizeDirectiveVisitor>();
            services.AddScoped<RequirePermissionDirectiveVisitor>();
            services
                .AddGraphQL(options =>
                {
                    options.UnhandledExceptionDelegate = ctx =>
                    {
                        ctx.ErrorMessage = ctx.OriginalException.Message;
                    };
                })
                .AddDataLoader()
                .AddSystemTextJson()
                .AddErrorInfoProvider(opts => opts.ExposeExceptionStackTrace = true);
        }
        public static void SetupGraphQl(this IApplicationBuilder app)
        {
            app.UseGraphQL<ISchema>();
            app.UseGraphQLGraphiQL(new GraphiQLOptions
            {
                SubscriptionsEndPoint = null,
                GraphQLEndPoint = "/graphql"
            }, "/ui/graphql");
            app.UseGraphQLPlayground(new PlaygroundOptions
            {
                GraphQLEndPoint = "/graphql",
            }, "/ui/playground");
        }
    }
}
