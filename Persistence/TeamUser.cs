namespace API.Persistence
{
    public class TeamUser
    {
        public string TeamId { get; set; }
        public Team Team { get; set; }

        public string UserName { get; set; }
        public User User { get; set; }
    }
}
