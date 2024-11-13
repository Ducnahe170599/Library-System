using System;
using System.Collections.Generic;

namespace ManagerLibrary.Models;

public partial class Account
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();

    public virtual Role Role { get; set; } = null!;
}
