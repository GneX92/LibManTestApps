using System;
using System.Collections.Generic;

namespace LibManTest.Models;

public partial class Book
{
    public string Isbn { get; set; } = null!;

    public string Title { get; set; } = null!;

    public DateOnly Year { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;
}
