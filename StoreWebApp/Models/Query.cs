using System.ComponentModel.DataAnnotations;

namespace StoreWebApp.Models
{
    public class Query
    {
        public string QueryId { get; set; }

        public string Error { get; set; }

        public int ErrorFlag { get; set; }

        public string ManufName { get; set; }

        public string CountryName { get; set; }

        public decimal AvgPrice { get; set; }

        public List<string> CustNames { get; set; }

        public List<string> CustLastnames { get; set; }

        public List<string> CustEmails { get; set; }

        public List<string> ProdNames { get; set; }

        public List<decimal> ProdPrices { get; set; }

        public string CustName { get; set; }

        public string CustLastname { get; set; }

        public string CustEmail { get; set; }

        public List<string> ManufNames { get; set; }

        [Required(ErrorMessage = "Поле необхідно заповнити")]
        //[RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "Введіть коректну вартість")]
        [Display(Name = "Вартість В")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Поле необхідно заповнити")]
        public string ProdName { get; set; }

        public int ManufId { get; set; }

        public List<string> CountryNames { get; set; }
    }
}
