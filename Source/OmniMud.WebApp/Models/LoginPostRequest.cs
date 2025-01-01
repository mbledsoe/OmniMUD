using System.ComponentModel.DataAnnotations;

namespace OmniMud.WebApp.Models
{
	public class LoginPostRequest
	{
		[Required]
		public string UserName { get; set; } = string.Empty;

		[Required]
		public string Password { get; set; } = string.Empty;

		[Required]
		public string ReturnUrl { get; set; } = string.Empty;
	}
}
