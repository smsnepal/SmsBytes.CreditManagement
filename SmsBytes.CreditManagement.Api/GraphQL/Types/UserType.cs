using System.Collections.Generic;
using GraphQL.Types;
using Micro.GraphQL.Federation;
using SmsBytes.CreditManagement.Api.GraphQL.Extensions;
using SmsBytes.CreditManagement.Business.History;
using SmsBytes.CreditManagement.Storage;
using SmsBytes.CreditManagement.Storage.Balance;

namespace SmsBytes.CreditManagement.Api.GraphQL.Types
{
    public sealed class UserType : Micro.GraphQL.Federation.ObjectGraphType<User>
    {
        public UserType(IBalanceRepository balanceRepository, ITransactionHistoryService transactionHistoryService)
        {
            Name = "User";
            ExtendByKeys("id");
            Field("id", x => x.Id).External();
            Field<NonNullGraphType<StringGraphType>>()
                .Name("available_balance")
                .ResolveAsync(async x => await balanceRepository.GetSmsBalance(x.Source.Id))
                .Authorize();

            Field<NonNullGraphType<ListGraphType<TransactionType>>, IEnumerable<Transaction>>()
                .Name("topup_history")
                .ResolveAsync(async x => await transactionHistoryService.GetTopupHistory(x.Source.Id))
                .Authorize();

            ResolveReferenceAsync(async ctx =>
            {
                var id = ctx.Arguments["id"].ToString();
                return new User
                {
                    Id = id,
                };
            });
        }
    }

    public class User
    {
        public string Id { set; get; }
    }
}
