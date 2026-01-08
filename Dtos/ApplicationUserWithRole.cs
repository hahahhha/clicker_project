using System;

namespace CSharpClicker.Dtos;

public class ApplicationUserWithRole
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
}
