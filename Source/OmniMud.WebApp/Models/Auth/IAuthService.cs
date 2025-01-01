namespace OmniMud.WebApp.Models.Auth
{
	public interface IAuthService
	{
		bool ValidatePassword(string username, string password);
		UserData GetUserData(string username);
	}

	public class UserData
	{
		public Guid Id { get; set; }
		public required string Username { get; set; }
		public required string Email { get; set; }
		
	}
}
