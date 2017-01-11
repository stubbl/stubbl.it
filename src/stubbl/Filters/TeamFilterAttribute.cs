using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;

namespace stubbl.Filters
{
    public class TeamFilterAttribute : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var stubblClient = new StubblClient();
            var teamResponse = await stubblClient.GetTeams();

            if (teamResponse.IsSuccessStatusCode)
            {
                var teams = JObject.Parse(await teamResponse.Content.ReadAsStringAsync());
                teams.GetValue("teams").ToList().ForEach(x =>
                {
                    (context.HttpContext.User.Identity as ClaimsIdentity)?.AddClaim(new Claim("team", x.Value<string>("id")));
                });
            }
            await next();
        }
    }
}