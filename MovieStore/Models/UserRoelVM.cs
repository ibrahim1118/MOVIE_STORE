namespace MovieStore.Models
{
    public class UserRoelVM
    {
        public string id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public List<RoleVM> Roles {get ; set;}  
    }
}
