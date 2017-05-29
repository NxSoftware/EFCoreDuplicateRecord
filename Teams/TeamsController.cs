using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace API.Teams
{
    [Route("[controller]/[action]")]
    public class TeamsController : Controller
    {
        readonly ITeamRepository _repository;

        public TeamsController(ITeamRepository repository)
        {
            _repository = repository;
        }

        public object List()
        {
            var teams = _repository.GetAll();

            return teams.Select(t => new
            {
                t.Id,
                t.Name,
                Users = t.TeamUsers.Select(tu => tu.UserName)
            });
        }

        [HttpPost("{teamname}")]
        public object Join([FromHeader] string username, [FromRoute] string teamname)
        {
            return _repository.Join(teamname, username);
        }
    }
}
