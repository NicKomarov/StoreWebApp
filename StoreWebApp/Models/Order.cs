using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreWebApp.Models;

public partial class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    [Display(Name = "CustomerId")]
    public virtual Customer Customer { get; set; } = null!;

    [Display(Name = "ProductId")]
    public virtual Product Product { get; set; } = null!;
}
