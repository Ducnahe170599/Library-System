using ManagerLibrary.ModelsViews;
using System;
using System.Collections.Generic;

namespace ManagerLibrary.Models;

public partial class BorrowRecord
{
    public int BorrowId { get; set; }

    public string Isbn { get; set; } = null!;

    public int UserId { get; set; }

    public DateOnly BorrowDate { get; set; }

    public DateOnly DueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public int Quantity { get; set; }

    public string? Notes { get; set; }

    public int Status { get; set; }

    public virtual Book IsbnNavigation { get; set; } = null!;

    public virtual Account User { get; set; } = null!;

    public string StatusDisplay => Status switch
    {
        (int)OrderStatus.Borrowed => "Đang mượn",
        (int)OrderStatus.Returned => "Đã trả",
        (int)OrderStatus.Overdue => "Quá hạn",
        (int)OrderStatus.Submitted => "Đã gửi yêu cầu",
        (int)OrderStatus.Confirmed => "Đã xác nhận",
        (int)OrderStatus.Reject => "Từ chối"

    };
}
