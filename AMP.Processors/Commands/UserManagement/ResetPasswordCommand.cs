namespace AMP.Processors.Commands.UserManagement;

public class ResetPasswordCommand
{
    /// <summary>
    /// Substring of old passwordKey starting from 5th index letter
    /// </summary>
    public string ConfirmCode { get; set; }
    public string Phone { get; set; }
    public string NewPassword { get; set; }
}