using System;
using Micro.GraphQL.Federation;
using SmsBytes.CreditManagement.Api.GraphQL.Directives;
using SmsBytes.CreditManagement.Api.GraphQL.Types;

namespace SmsBytes.CreditManagement.Api.GraphQL
{
    public class StarterSchema : Schema<EntityType>
    {
        public StarterSchema(IServiceProvider services, Query query, Mutation mutation) : base(services)
        {
            Query = query;
            Mutation = mutation;
            Directives.Register(new AuthorizeDirective());
            Directives.Register(new RequirePermissionDirective());
            RegisterVisitor(typeof(AuthorizeDirectiveVisitor));
            RegisterVisitor(typeof(RequirePermissionDirectiveVisitor));
        }
    }
}
