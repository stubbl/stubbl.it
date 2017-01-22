using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stubbl
{
    public class TeamsMiddleWare
    {
        private readonly RequestDelegate _next;
        private IRetrieveCurrentTeam _retrieveCurrentTeam;
        private StubblClient _stubblApiClient;

        public TeamsMiddleWare(RequestDelegate next, IRetrieveCurrentTeam retrieveCurrentTeam, StubblClient stubblApiClient)
        {
            _next = next;
            _retrieveCurrentTeam = retrieveCurrentTeam;
            _stubblApiClient = stubblApiClient;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(httpContext.Request.Path == "/teams/create")
            {
                await _next.Invoke(httpContext);
                return;
            }
            if ((httpContext.Session.GetInt32("NoTeam") ?? 0) > 0)
            {
                RedirectToCreateTeam(httpContext);
                return;
            }
            try
            {
                _retrieveCurrentTeam.GetCurrentTeamId();
                await _next.Invoke(httpContext);
                return;
            }
            catch(CurrentTeamNotSetException)
            {
                var teamResponse = await _stubblApiClient.GetTeams();

                if (teamResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    httpContext.Session.SetInt32("NoTeam", 1);
                }
                else
                {
                    var teams = JObject.Parse(await teamResponse.Content.ReadAsStringAsync());
                    httpContext.Session.SetString("CurrentTeam", teams.GetValue("teams").First().Value<string>("id"));
                }

            }
            await _next.Invoke(httpContext);
        }

        private void RedirectToCreateTeam(HttpContext httpContext)
        {
             httpContext.Response.Redirect("/teams/create");
        }
    }
}
