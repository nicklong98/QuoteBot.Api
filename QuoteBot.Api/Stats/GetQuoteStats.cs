using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuoteBot.DatabaseLayer;

namespace QuoteBot.Api.Stats
{
    [Route("api/v1/stats")]
    public class GetQuoteStatsController : Controller
    {
        public record QuoteStat(string UserId, int NumberOfQuotes);

        private readonly QuoteContext _context;

        public GetQuoteStatsController(QuoteContext context)
        {
            _context = context;
        }

        [HttpGet("{serverId}")]
        public ActionResult<IEnumerable<QuoteStat>> GetQuoteStats(long serverId)
        {
            var groupedQuotes = _context.Quotes.Where(x => x.ServerId == serverId).GroupBy(x => x.UserId)
                .Select(x => new QuoteStat(x.Key.ToString(), x.Count())).ToList().OrderByDescending(x => x.NumberOfQuotes);
            return Ok(groupedQuotes);
        }
    }
}