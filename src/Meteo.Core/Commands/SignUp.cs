namespace Meteo.Core.Commands
{
    public class SignUp : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }        
    }
}