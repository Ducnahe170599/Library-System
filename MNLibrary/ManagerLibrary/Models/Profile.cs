using System;
using System.Collections.Generic;

namespace ManagerLibrary.Models;

public partial class Profile
{
    public int ProfileId { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    public string? ReaderCode { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual Account User { get; set; } = null!;
}
