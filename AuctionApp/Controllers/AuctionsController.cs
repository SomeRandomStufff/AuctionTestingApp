using AuctionApp.Data;
using AuctionApp.Data.Tables;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : Controller
    {
        private AuctionsDatabase _database = new();

        #region Примеры

        [HttpGet("[action]")]
        public string GetFirstAuctionNumber()
        {
            return _database.Auctions.First().Number; 
        }

        [HttpGet("[action]")]
        public IList<Company> GetCompanies()
        {
            return (from company in _database.Companies
                   where company.Phone.StartsWith("+79")
                   select company).ToList();
        }

        [HttpDelete("Action")]
        public void DeleteCompany()
        {
            using (var t = _database.BeginTransaction())
            {
                var companiesToDelete = _database.Companies.Take(2).ToList();

                _database.Delete(companiesToDelete[0]);
                _database.Delete(companiesToDelete[1]);

                t.Commit();
            }
        }

        #endregion

        #region Для задания

        [HttpPost("[action]")]
        public async Task<ActionResult> UploadJson()
        {
            string jsonText;
            var file = Request.Form.Files[0];

            using (var stream = file.OpenReadStream())
            using (var sr = new StreamReader(stream))
                jsonText = sr.ReadToEnd();

            //Заменить на обработку jsonText
            await Task.Delay(1000);

            return Ok();
        }

        [HttpGet("[action]/{search}")]
        public object LoadData(string search)
        {
            // заменить на запрос из задания
            return _database.Companies.ToList();
        }
        #endregion
    }
}
