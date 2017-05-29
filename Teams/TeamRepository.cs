﻿﻿﻿using System.Linq;
using System;
using API.Users;
using API.Persistence;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Teams
{
    public interface ITeamRepository
    {
        string Join(string teamName, string userName);
        ICollection<Team> GetAll();
    }

    public class TeamRepository : ITeamRepository
    {
        readonly Database _db;
        readonly IUserRepository _userRepository;

        public TeamRepository(Database db, IUserRepository userRepository)
        {
            _db = db;
            _userRepository = userRepository;
        }

        public ICollection<Team> GetAll()
        {
            return _db.Teams.Include(t => t.TeamUsers).ToList();
        }

        public string Join(string teamName, string userName)
        {
            var team = CreateOrGetTeam(teamName);
            var user = _userRepository.CreateOrGetUser(userName);

            team.TeamUsers.Add(new TeamUser
            {
                Team = team,
                User = user
            });

            try
            {
				_db.SaveChanges();            
            }
            catch (Exception ex)
            {
				// Assume any exceptions mean we're already a member.
				// Obviously that's not guaranteed but until there's a
				// way to detect duplicate key violations I can't see
				// an alternative.
				// https://stackoverflow.com/questions/44229695/handle-duplicate-key-violations-in-entityframework-core
                System.Diagnostics.Debug.WriteLine(ex);
			}

            return team.Id;
        }

        Team CreateOrGetTeam(string teamName)
        {
            var team = _db.Teams
                          .Include(u => u.TeamUsers)
                          .FirstOrDefault(t => t.Name.ToLower().Equals(teamName.ToLower()));
			if (team == null)
			{
                team = new Team()
				{
					Name = teamName,
                    TeamUsers = new List<TeamUser>()
				};
				_db.Teams.Add(team);
			}
            return team;
        }
    }
}
