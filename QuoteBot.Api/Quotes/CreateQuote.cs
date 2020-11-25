using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuoteBot.DatabaseLayer;
using QuoteBot.DatabaseLayer.Entities;

namespace QuoteBot.Api.Quotes
{
    [Route("api/v1/quote")]
    public class CreateQuoteController : Controller
    {
        public record CreateQuoteRequestVm(string ServerId, string UserId, string ReporterId, string Body);

        private class CreateQuoteMappingProfile : Profile
        {
            public CreateQuoteMappingProfile()
            {
                CreateMap<CreateQuoteRequestVm, Quote>()
                    .ForMember(x => x.Id, opts => opts.Ignore())
                    .ForMember(x => x.CreatedAt, opts => opts.MapFrom(__ => DateTime.UtcNow))
                    .ForMember(x => x.ReporterId, opts => opts.MapFrom(src => long.Parse(src.ReporterId)))
                    .ForMember(x => x.ServerId, opts => opts.MapFrom(src => long.Parse(src.ServerId)))
                    .ForMember(x => x.UserId, opts => opts.MapFrom(src => src.UserId));
            }
        }

        private readonly QuoteContext _context;
        private readonly IMapper _mapper;

        public CreateQuoteController(QuoteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<GetQuoteResponseVm>> CreateQuote([FromBody] CreateQuoteRequestVm toCreate)
        {
            var quote = _mapper.Map<Quote>(toCreate);
            await _context.AddAsync(quote);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<GetQuoteResponseVm>(quote));
        }
    }
}