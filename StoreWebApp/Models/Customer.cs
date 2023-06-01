using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreWebApp.Models;

public partial class Customer
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле необхідно заповнити")]
    [StringLength(50)]
    [Display(Name = "Ім\'я")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Поле необхідно заповнити")]
    [StringLength(50)]
    [Display(Name = "Прізвище")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Поле необхідно заповнити")]
    [StringLength(50)]
    [EmailAddress(ErrorMessage = "Некоректна email адреса")]
    public string Email { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
