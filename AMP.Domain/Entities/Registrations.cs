using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities;

public class Registrations : EntityBase
{
    private Registrations(string phone, string verificationCode)
    {
        Phone = phone;
        VerificationCode = verificationCode;
    }

    public static Registrations Create(string phone, string verificationCode)
        => new Registrations(phone, verificationCode);

    public string Phone { get; private set; }
    public string VerificationCode { get; private set; }

    public Registrations ForUserWithPhone(string phone)
    {
        Phone = phone;
        return this;
    }

    public Registrations HasVerificationCode(string code)
    {
        VerificationCode = code;
        return this;
    }
}