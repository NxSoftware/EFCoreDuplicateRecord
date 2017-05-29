using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Persistence
{
    public class Team
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<TeamUser> TeamUsers { get; set; }
    }
}
