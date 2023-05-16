using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Microsoft.Build.Framework.Required]
    [EmailAddress]
    public string Email { get; set; }

    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Microsoft.Build.Framework.Required]
    public string Role { get; set; }
}