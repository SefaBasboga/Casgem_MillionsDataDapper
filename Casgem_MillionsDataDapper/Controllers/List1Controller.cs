using Casgem_MillionsDataDapper.DAL;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Casgem_MillionsDataDapper.Controllers
{
    public class List1Controller : Controller
    {
        private readonly string _connectionString = "Server = DESKTOP-H5NLS4J; initial catalog = CARPLATES; integrated security = true";

        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                var query = "SELECT TOP 100 * FROM PLATES"; // İlk başta 100 veriyi getir

                // Eğer arama yapılıyorsa ve searchString doluysa, filtreleme yapacak sorguyu oluştur
                if (!string.IsNullOrEmpty(searchString))
                {
                    // Marka (BRAND) değeri arama kriterine göre filtrelenir
                    query = "SELECT * FROM PLATES WHERE BRAND LIKE @SearchString";
                    // % işareti aramanın başında veya sonunda olabilir, böylece kısmi eşleşmeyi destekler
                    searchString = $"%{searchString}%";
                }

                var values = await connection.QueryAsync<Plates>(query, new { SearchString = searchString });
                return View(values);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajı göstermek için gerekli işlemleri burada yapabilirsiniz.
                return View("Error", ex.Message);
            }
        }



    }
}
