using Microsoft.AspNetCore.Http;

namespace stubbl
{
    public interface IRetrieveCurrentTeam
    {
        string GetCurrentTeamId();
    }
    public class RetrieveCurrentTeamFromHttpContext:IRetrieveCurrentTeam
    {
        private IHttpContextAccessor _httpContextAccessor;

        public RetrieveCurrentTeamFromHttpContext(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }
        public string GetCurrentTeamId() {
            string currentTeam = _httpContextAccessor?.HttpContext?.Session?.GetString("currentTeam");
            if(string.IsNullOrEmpty(currentTeam))
            {
                throw new CurrentTeamNotSetException();
            }
            return currentTeam;
        }
    }
}