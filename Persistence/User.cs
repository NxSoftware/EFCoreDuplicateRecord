using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Persistence
{
    public class User
    {
        [Key]
        public string Name { get; set; }

        public ICollection<TeamUser> TeamUsers { get; set; }
    }
}
