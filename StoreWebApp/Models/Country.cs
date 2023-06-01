using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreWebApp.Models;

public partial class Country
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле необхідно заповнити")]
    [StringLength(50)]
    [Display(Name = "Країна")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Manufacturer> Manufacturers { get; set; } = new List<Manufacturer>();
}
