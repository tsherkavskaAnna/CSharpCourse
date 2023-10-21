namespace DotnetAPI.Dtos
{
    public partial class UserForLoginDto
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public UserForLoginDto()
        {
            if (Email == null)
            {
                Email = "";
            }
            if (PasswordHash == null)
            {
                PasswordHash = "";
            }
        }

    }
}