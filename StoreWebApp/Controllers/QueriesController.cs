using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using StoreWebApp.Models;

namespace StoreWebApp.Controllers
{
    public class QueriesController : Controller
    {
        private const string CONN_STR = "Server= DESKTOP-CD5UUTH\\SQLEXPRESS; Database=DBStore; Trusted_Connection=True; Trust Server Certificate=True; MultipleActiveResultSets=true";

        //private const string S1_PATH = @"D:\Женя\Студматериалы\2 КУРС\2 СЕМЕСТР\БД та ІС\Лаба 2\Waresoft\Waresoft\Queries\S1.sql";
        private const string S1_PATH = @"D:\cyber_knu\degree2\DB\laba2\StoreWebApp\StoreWebApp\Queries\S1.sql";
        private const string S2_PATH = @"D:\cyber_knu\degree2\DB\laba2\StoreWebApp\StoreWebApp\Queries\S2.sql";
        private const string S3_PATH = @"D:\cyber_knu\degree2\DB\laba2\StoreWebApp\StoreWebApp\Queries\S3.sql";
        private const string S5_PATH = @"D:\cyber_knu\degree2\DB\laba2\StoreWebApp\StoreWebApp\Queries\S5.sql";
        private const string S6_PATH = @"D:\cyber_knu\degree2\DB\laba2\StoreWebApp\StoreWebApp\Queries\S6.sql";

        private const string A1_PATH = @"D:\cyber_knu\degree2\DB\laba2\StoreWebApp\StoreWebApp\Queries\A1.sql";
        private const string A2_PATH = @"D:\cyber_knu\degree2\DB\laba2\StoreWebApp\StoreWebApp\Queries\A2.sql";
        private const string A3_PATH = @"D:\cyber_knu\degree2\DB\laba2\StoreWebApp\StoreWebApp\Queries\A3.sql";

        private const string T1_PATH = @"D:\Женя\Студматериалы\2 КУРС\2 СЕМЕСТР\БД та ІС\Лаба 2\Waresoft\Waresoft\Queries\T1.sql";
        private const string T2_PATH = @"D:\Женя\Студматериалы\2 КУРС\2 СЕМЕСТР\БД та ІС\Лаба 2\Waresoft\Waresoft\Queries\T2.sql";

        private const string ERR_AVG = "Неможливо обрахувати середню ціну, оскільки продукти відсутні.";
        private const string ERR_CUST = "Покупці, що задовольняють дану умову, відсутні.";
        private const string ERR_PROD = "Програмні продукти, що задовольняють дану умову, відсутні.";
        private const string ERR_DEV = "Розробники, що задовольняють дану умову, відсутні.";
        private const string ERR_COUNTRY = "Країни, що задовольняють дану умову, відсутні.";

        private readonly DbstoreContext _context;

        public QueriesController(DbstoreContext context)
        {
            _context = context;
        }

        public IActionResult Index(int errorCode)
        {
            var customers = _context.Customers.Select(c => c.Name).Distinct().ToList();
            if (errorCode == 1)
            {
                ViewBag.ErrorFlag = 1;
                ViewBag.PriceError = "Введіть коректну вартість";
            }
            if (errorCode == 2)
            {
                ViewBag.ErrorFlag = 2;
                ViewBag.ProdNameError = "Поле необхідно заповнити";
            }

            var empty = new SelectList(new List<string> { "--Пусто--" });
            var anyCusts = _context.Customers.Any();
            var anyManuf = _context.Manufacturers.Any();

            ViewBag.ManufIds = anyManuf ? new SelectList(_context.Manufacturers, "Id", "Id") : empty;
            ViewBag.ManufNames = anyManuf ? new SelectList(_context.Manufacturers, "Name", "Name") : empty;
            ViewBag.CustNames = anyCusts ? new SelectList(customers) : empty;
            ViewBag.CustEmails = anyCusts ? new SelectList(_context.Customers, "Email", "Email") : empty;
            ViewBag.CustLastnames = anyCusts ? new SelectList(_context.Customers, "LastName", "LastName") : empty;
            ViewBag.Countries = _context.Countries.Any() ? new SelectList(_context.Countries, "Name", "Name") : empty;
            return View(); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery1(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(S1_PATH);
            string new_par = "'" + queryModel.ManufName + "'";
            query = query.Replace("X", new_par);

            queryModel.QueryId = "S1";

            using (var connection = new SqlConnection(CONN_STR))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        queryModel.AvgPrice = Convert.ToDecimal(result);
                    }
                    else
                    {
                        queryModel.ErrorFlag = 1;
                        queryModel.Error = ERR_AVG;
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery2(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(S2_PATH);
            string new_par = "'" + queryModel.ManufName + "'";
            query = query.Replace("X", new_par);
       

            queryModel.QueryId = "S2";
            queryModel.CustNames = new List<string>();
            queryModel.CustLastnames = new List<string>();

            using (var connection = new SqlConnection(CONN_STR))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.CustNames.Add(reader.GetString(0));
                            queryModel.CustLastnames.Add(reader.GetString(1));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR_CUST;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery3(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(S3_PATH);
            string new_par = "'" + queryModel.CountryName + "'";
            query = query.Replace("X", new_par);

            queryModel.QueryId = "S3";
            queryModel.ProdNames = new List<string>();
            queryModel.ProdPrices = new List<decimal>();

            using (var connection = new SqlConnection(CONN_STR))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.ProdNames.Add(reader.GetString(0));
                            queryModel.ProdPrices.Add(reader.GetDecimal(1));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR_PROD;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery5(Query queryModel)
        {
            if (ModelState.IsValid)
            {
                string query = System.IO.File.ReadAllText(S5_PATH);
                string new_par = "'" + queryModel.Price.ToString() + "'";
                query = query.Replace("X", new_par);

                queryModel.QueryId = "S5";
                queryModel.ManufNames = new List<string>();

                using (var connection = new SqlConnection(CONN_STR))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        using (var reader = command.ExecuteReader())
                        {
                            int flag = 0;
                            while (reader.Read())
                            {
                                queryModel.ManufNames.Add(reader.GetString(0));
                                flag++;
                            }

                            if (flag == 0)
                            {
                                queryModel.ErrorFlag = 1;
                                queryModel.Error = ERR_DEV;
                            }
                        }
                    }
                    connection.Close();
                }
                return RedirectToAction("Result", queryModel);
            }
            return RedirectToAction("Index", new { errorCode = 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SimpleQuery6(Query queryModel)
        {
            if (ModelState.IsValid)
            {
                string query = System.IO.File.ReadAllText(S6_PATH);
                string new_par = "'" + "Z2 88" + "'";
                query = query.Replace("X", new_par);
                
                queryModel.QueryId = "S6";
                queryModel.ManufNames = new List<string>();

                using (var connection = new SqlConnection(CONN_STR))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                        using (var reader = command.ExecuteReader())
                        {
                            int flag = 0;
                            while (reader.Read())
                            {
                                queryModel.ManufNames.Add(reader.GetString(0));
                                flag++;
                            }

                            if (flag == 0)
                            {
                                queryModel.ErrorFlag = 1;
                                queryModel.Error = ERR_DEV;
                            }
                        }
                    }
                    connection.Close();
                }
                return RedirectToAction("Result", queryModel);
            }

            return RedirectToAction("Index", new { errorCode = 2 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdvancedQuery1(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(A1_PATH);
            string new_par = "'" + queryModel.ManufId.ToString() + "'";
            query = query.Replace("Q", new_par);

            queryModel.QueryId = "A1";
            queryModel.CountryNames = new List<string>();

            using (var connection = new SqlConnection(CONN_STR))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.CountryNames.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR_COUNTRY;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdvancedQuery2(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(A2_PATH);
            string new_par = "'" + queryModel.CustLastname.ToString() + "'";
            query = query.Replace("Y", new_par);
            
            queryModel.QueryId = "A2";
            queryModel.CustLastnames = new List<string>();

            using (var connection = new SqlConnection(CONN_STR))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.CustLastnames.Add(reader.GetString(0));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR_CUST;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }


        public IActionResult AdvancedQuery3(Query queryModel)
        {
            string query = System.IO.File.ReadAllText(A3_PATH);
            string new_par = "'" + queryModel.CustName.ToString() + "'";
            query = query.Replace("Y", new_par);

            queryModel.QueryId = "A3";
            queryModel.CustLastnames = new List<string>();
            queryModel.CustEmails = new List<string>();

            using (var connection = new SqlConnection(CONN_STR))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        int flag = 0;
                        while (reader.Read())
                        {
                            queryModel.CustLastnames.Add(reader.GetString(0));
                            queryModel.CustEmails.Add(reader.GetString(1));
                            flag++;
                        }

                        if (flag == 0)
                        {
                            queryModel.ErrorFlag = 1;
                            queryModel.Error = ERR_CUST;
                        }
                    }
                }
                connection.Close();
            }
            return RedirectToAction("Result", queryModel);
        }

        public IActionResult Result(Query queryResult)
        {
            return View(queryResult);
        }
    }
}
