namespace PicShopper.Web.Models
{
    public enum UserType
    {
        None,
        Admin,
        User
    }
    public class Login
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public UserType UType { get; set; }
        public string Password { get; set; }
        public string RetUrl { get; set; }
    }
}
