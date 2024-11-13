using ManagerLibrary.ModelsViews;
using System;
using System.Collections.Generic;

namespace ManagerLibrary.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public int AuthorId { get; set; }

    public string? Image { get; set; }


    public int CategoryId { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int Quantity { get; set; }

    public int Status { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();

    public virtual Category Category { get; set; } = null!;


    
    }
