using System;
using Micro.GraphQL.Federation;
using SmsBytes.CreditManagement.Api.GraphQL.Directives;
using SmsBytes.CreditManagement.Api.GraphQL.Types;

namespace SmsBytes.CreditManagement.Api.GraphQL
{
    public class StarterSchema : Schema<EntityType>
    {
        public StarterSchema(IServiceProvider services, Query query) : base(services)
        {
            Query = query;
            Directives.Register(new AuthorizeDirective());
            RegisterVisitor(typeof(AuthorizeDirectiveVisitor));
        }
    }
}
