using System;
using AMP.Domain.Enums;

namespace AMP.Processors.PageDtos;

public class InvitationPageDto
{
    public string InvitedPhone { get; set; }
    public UserType Type { get; set; }
    public DateTime DateCreated { get; set; }
}