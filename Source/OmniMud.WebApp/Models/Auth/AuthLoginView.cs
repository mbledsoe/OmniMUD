using System.ComponentModel.DataAnnotations;

namespace OmniMud.WebApp.Models.Auth
{
	public class AuthLoginView
	{
		public required string ReturnUrl { get; set; }
	}
}
