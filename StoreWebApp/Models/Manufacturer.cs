using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreWebApp.Models;

public partial class Manufacturer
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле необхідно заповнити")]
    [StringLength(50)]
    [Display(Name = "Назва виробника")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Поле необхідно заповнити")]
    [StringLength(50)]
    [Display(Name = "Країна")]
    public int CountryId { get; set; }

    [Display(Name = "Країна")]
    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
