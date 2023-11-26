using InsideLine.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Security.Claims;
using Azure.Data.Tables;
using Microsoft.Azure.Cosmos;
using Azure.Identity;
using InsideLine.Database;
using User = InsideLine.Database.User;

namespace InsideLine.Services
{
    public partial class DatabaseService
    {


        private readonly HttpClient httpClient;

        private readonly NavigationManager navigationManager;
        private readonly TableServiceClient tableServiceClient;
        //private readonly CosmosClient cosmosClient;


        private TableClient driversTable;
        private TableClient usersTable;


        public DatabaseService(NavigationManager navigationManager, IHttpClientFactory factory, IConfiguration configuration)
        {
            httpClient = factory.CreateClient("InsideLine");
            this.navigationManager = navigationManager;

            //cosmosClient = new(accountEndpoint: configuration["AccountEndpoint"]!, tokenCredential: new DefaultAzureCredential());
            tableServiceClient = new TableServiceClient(configuration["DatabaseConnectionString"]);            
        }


        public void HandleUserLogin()
        {
            driversTable = tableServiceClient.GetTableClient(tableName: "drivers2");
            var result = driversTable.CreateIfNotExistsAsync().Result;
        }

        internal User GetUser(string name, string emailAddress)
        {
            usersTable = tableServiceClient.GetTableClient("Users");
            var result = usersTable.CreateIfNotExistsAsync().Result;

            // Check if the User exists

            var users = usersTable.Query<User>(x => x.EmailAddress.Equals(emailAddress, StringComparison.CurrentCultureIgnoreCase)).ToList();

            if (users.Count == 0)
            {
                // There is an existing matching User entry
                var newUser = new User
                {
                    EmailAddress = emailAddress,
                    Timestamp = DateTime.UtcNow,
                    CustomerId = "",
                    DisplayName = name,
                    PartitionKey = "user",
                    RowKey = emailAddress
                };

                usersTable.AddEntity(newUser);
                return newUser;
            }
            else
            {
                return users.FirstOrDefault();
            }            
        }

        internal void HandleUserAuthenticated(ClaimsIdentity identity)
        {
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Retrieve the email address from the ClaimsPrincipal
            var emailAddress = claimsPrincipal.FindFirstValue("preferred_username");
            var name = claimsPrincipal.FindFirstValue("name");

            if (!string.IsNullOrEmpty(emailAddress) && !string.IsNullOrEmpty(name))
            {
                // The user has a specified email address, let's see if they are in the database as a previous driver

                var user = GetUser(name, emailAddress);
                if (string.IsNullOrEmpty(user.CustomerId))
                {
                    // The user has not set their CustomerID yet. Make sure they do this before letting them go much further
                   // navigationManager.NavigateTo("UnregisteredDriver");
                }

            }
        }
    }
}
