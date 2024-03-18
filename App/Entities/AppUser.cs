namespace App.Entities;

public class AppUser
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string KnownAs { get; set; }
    public DateTime LastActive { get; set; } = DateTime.Now;
    public string Introduction { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    ////////////////////////////
    /// p' Identity
    public string Email => UserName; // el mismo UserName
    public string NormalizedEmail { get; set; } // el mismo UserName Normalized

    ////////////////////////////
    public List<Picture> Pictures { get; set; } = new();

}
