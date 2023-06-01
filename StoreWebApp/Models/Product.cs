using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreWebApp.Models;

public partial class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле необхідно заповнити")]
    [StringLength(50)]
    [Display(Name = "Назва")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Поле необхідно заповнити")]
    [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "Введіть коректну вартість (123.45)")]
    [Display(Name = "Вартість")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Поле необхідно заповнити")]
    [StringLength(50)]
    [Display(Name = "Опис")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Поле необхідно заповнити")]
    [StringLength(50)]
    [Display(Name = "Виробник")]
    public int ManufacturerId { get; set; }

    [Display(Name = "Виробник")]
    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
