namespace Rika_WebApp.Models;

public class UpdateUserModel
{
	public string UserId { get; set; } = null!;
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Password { get; set; } = null!;
	public string Phonenumber { get; set; } = null!;
	public string ProfileImageUrl { get; set; } = null!;
	public int Age { get; set; }
	public string Gender { get; set; } = null!;
}
