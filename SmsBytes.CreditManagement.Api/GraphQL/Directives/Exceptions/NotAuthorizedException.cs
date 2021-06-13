using System;

namespace SmsBytes.CreditManagement.Api.GraphQL.Directives.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() : base("This operation requires logging in")
        {
        }
    }
}
