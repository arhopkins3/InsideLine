using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

using InsideLine.Models;
using InsideLine.Services;
using Microsoft.AspNetCore.Components;

namespace InsideLine
{
    public class ApplicationAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly SecurityService securityService;
        private readonly DatabaseService databaseService;
        private readonly NavigationManager navigationManager;

        private ApplicationAuthenticationState authenticationState;

        public ApplicationAuthenticationStateProvider(SecurityService securityService, DatabaseService databaseService, NavigationManager navigationManager)
        {
            this.securityService = securityService;
            this.databaseService = databaseService;
            this.navigationManager = navigationManager;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();

            try
            {
                var state = await GetApplicationAuthenticationStateAsync();

                if (state.IsAuthenticated)
                {
                    identity = new ClaimsIdentity(state.Claims.Select(c => new Claim(c.Type, c.Value)), "InsideLine");

                    //// Retrieve the email address from the claims
                    //var emailAddress = state.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

                    //// Add the email address as a claim
                    //if (!string.IsNullOrEmpty(emailAddress))
                    //{
                    //    identity.AddClaim(new Claim(ClaimTypes.Email, emailAddress));
                    //}


                    // Retrieve the email address from the ClaimsPrincipal
                    var claimsPrincipal = new ClaimsPrincipal(identity);
                    var emailAddress = claimsPrincipal.FindFirstValue("preferred_username");
                    var name = claimsPrincipal.FindFirstValue("name");

                    if (!string.IsNullOrEmpty(emailAddress) && !string.IsNullOrEmpty(name))
                    {
                        // The user has a specified email address, let's see if they are in the database as a previous driver

                        var user = databaseService.GetUser(name, emailAddress);
                        if (string.IsNullOrEmpty(user.CustomerId))
                        {
                            // The user has not set their CustomerID yet. Make sure they do this before letting them go much further
                            identity.AddClaim(new Claim(ClaimTypes.Role, "UnregisteredDriver"));
                            //navigationManager.NavigateTo("UnregisteredDriver");
                        }

                    }

                    databaseService.HandleUserAuthenticated(identity);
                }
            }
            catch (HttpRequestException ex)
            {
            }

            var result = new AuthenticationState(new ClaimsPrincipal(identity));

            securityService.Initialize(result);

            return result;
        }

        private async Task<ApplicationAuthenticationState> GetApplicationAuthenticationStateAsync()
        {
            if (authenticationState == null)
            {
                authenticationState = await securityService.GetAuthenticationStateAsync();
            }

            return authenticationState;
        }
    }
}