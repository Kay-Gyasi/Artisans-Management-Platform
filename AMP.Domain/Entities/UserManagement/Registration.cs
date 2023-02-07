using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities.UserManagement;

public class Registration : Entity
{
    private Registration(string phone, string verificationCode)
    {
        Phone = phone;
        VerificationCode = verificationCode;
    }

    public static Registration Create(string phone, string verificationCode)
        => new Registration(phone, verificationCode);

    public string Phone { get; private set; }
    public string VerificationCode { get; private set; }

    public Registration ForUserWithPhone(string phone)
    {
        Phone = phone;
        return this;
    }

    public Registration HasVerificationCode(string code)
    {
        VerificationCode = code;
        return this;
    }
}