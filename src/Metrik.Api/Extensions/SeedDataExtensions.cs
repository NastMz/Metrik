using Bogus;
using Dapper;
using Metrik.Application.Abstractions.Interfaces.Data;
using Metrik.Domain.Entities.Accounts.Enums;
using Metrik.Domain.Entities.Transactions.Enums;

namespace Metrik.Api.Extensions
{
    public static class SeedDataExtensions
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
            using var connection = sqlConnectionFactory.CreateConnection();

            var faker = new Faker();

            List<dynamic> users = new List<dynamic>();

            for (int i = 0; i < 10; i++)
            {
                var user = new
                {
                    Id = Guid.NewGuid(),
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    Email = faker.Internet.Email()
                };
                users.Add(user);
            }

            var sql = @"
                    INSERT INTO users (id, first_name, last_name, email, created_at, updated_at)
                    VALUES (@Id, @FirstName, @LastName, @Email, NOW(), NOW())";

            connection.Execute(sql, users);

            List<dynamic> accounts = [];

            for (int i = 0; i < 10; i++)
            {
                var account = new
                {
                    Id = Guid.NewGuid(),
                    UserId = users[i].Id,
                    Name = faker.Finance.Account(),
                    Type = faker.Random.Enum<AccountType>(),
                    BalanceAmount = faker.Finance.Amount(),
                    BalanceCurrency = "USD"
                };
                accounts.Add(account);
            }

            sql = @"
                    INSERT INTO accounts (id, user_id, name, type, balance_amount, balance_currency, created_at, updated_at)
                    VALUES (@Id, @UserId, @Name, @Type, @BalanceAmount, @BalanceCurrency, NOW(), NOW())";

            connection.Execute(sql, accounts);

            List<dynamic> categories = [];

            for (int i = 0; i < 10; i++)
            {
                var category = new
                {
                    Id = Guid.NewGuid(),
                    UserId = users[i].Id,
                    Name = faker.Commerce.Categories(1)[0],
                    Type = faker.Finance.TransactionType()
                };
                categories.Add(category);
            }

            sql = @"
                    INSERT INTO categories (id, user_id, name, type, created_at, updated_at)
                    VALUES (@Id, @UserId, @Name, @Type, NOW(), NOW())";

            connection.Execute(sql, categories);

            List<dynamic> transactions = [];

            for (int i = 0; i < 10; i++)
            {
                var transaction = new
                {
                    Id = Guid.NewGuid(),
                    UserId = users[i].Id,
                    AccountId = accounts[i].Id,
                    CategoryId = categories[i].Id,
                    ValueAmount = faker.Finance.Amount(),
                    ValueCurrency = accounts[i].BalanceCurrency,
                    Type = faker.Random.Enum<TransactionType>(),
                    Description = faker.Lorem.Sentence(),
                    Date = faker.Date.Past()
                };
                transactions.Add(transaction);
            }

            sql = @"
                    INSERT INTO transactions (id, user_id, account_id, category_id, value_amount, value_currency, type, description, date, created_at, updated_at)
                    VALUES (@Id, @UserId, @AccountId, @CategoryId, @ValueAmount, @ValueCurrency, @Type, @Description, @Date, NOW(), NOW())";

            connection.Execute(sql, transactions);
        }
    }
}
