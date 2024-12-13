namespace Comp584TaskApi.DTO
{
    public class CreateUserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; } = null!;
    }
}
