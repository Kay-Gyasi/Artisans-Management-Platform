namespace AMP.Processors.QueryObjects;

public class LoginQueryObject
{
    public byte[] Password { get; set; }
    public byte[] PasswordKey { get; set; }
    public string Contact_PrimaryContact { get; set; }
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string FamilyName { get; set; }
    public string ImageUrl { get; set; }
    public string Contact_EmailAddress { get; set; }
    public string Address_StreetAddress { get; set; }
    public string Type { get; set; }
}