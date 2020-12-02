using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using QuoteBot.DatabaseLayer.Entities;

namespace QuoteBot.Api.Quotes
{
    public record GetQuoteResponseVm(string Body, string AuthorId, IEnumerable<string> AttachmentUrls);

    public class GetQuoteResponseMappingProfile : Profile
    {
        public GetQuoteResponseMappingProfile()
        {
            CreateMap<Quote, GetQuoteResponseVm>()
                .ForCtorParam("Body", opts => opts.MapFrom(src => src.Body))
                .ForCtorParam("AuthorId", opts => opts.MapFrom(src => src.UserId.ToString()))
                .ForCtorParam("AttachmentUrls", opts => opts.MapFrom(src => src.QuoteAttachments.Select(x => x.Url)));
        }
    }
}