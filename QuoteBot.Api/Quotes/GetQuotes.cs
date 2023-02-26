using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using QuoteBot.DatabaseLayer;

namespace QuoteBot.Api.Quotes
{
    [Route("api/v1/quotes")]
    public class GetQuotesController : Controller
    {
        private readonly QuoteContext _context;
        private readonly IMapper _mapper;

        public GetQuotesController(QuoteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{serverId}")]
        public ActionResult<IEnumerable<GetQuoteResponseVm>> GetQuotesByServer(long serverId)
        {
            var quotes = _context.Quotes.Where(x => x.ServerId == serverId)
                .ProjectTo<GetQuoteResponseVm>(_mapper.ConfigurationProvider);
            return Ok(quotes);
        }

        [HttpGet("{serverId}/random")]
        public ActionResult<GetQuoteResponseVm> GetRandomQuoteFromServer(long serverId)
        {
            var quote = _context.Quotes.Where(x => x.ServerId == serverId)
                .ProjectTo<GetQuoteResponseVm>(_mapper.ConfigurationProvider).ToList().OrderBy(__ => Guid.NewGuid())
                .FirstOrDefault();
            return Ok(quote);
        }

        [HttpGet("{serverId}/random/{userId}")]
        public ActionResult<GetQuoteResponseVm> GetRandomQuoteForUserOnServer(long serverId, long userId)
        {
            var quote = _context.Quotes.Where(x => x.ServerId == serverId && x.UserId == userId)
                .ProjectTo<GetQuoteResponseVm>(_mapper.ConfigurationProvider).ToList()
                .OrderBy(__ => Guid.NewGuid()).FirstOrDefault();
            if (quote == default)
            {
                quote = new GetQuoteResponseVm("Look I'm lame and don't have any quotes in quote bot ok?",
                    userId.ToString(), DateTime.UtcNow, null);
            }

            return Ok(quote);
        }
    }
}