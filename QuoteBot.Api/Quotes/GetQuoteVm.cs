using AutoMapper;
using QuoteBot.DatabaseLayer.Entities;

namespace QuoteBot.Api.Quotes
{
    public record GetQuoteResponseVm(string Body);

    public class GetQuoteResponseMappingProfile : Profile
    {
        public GetQuoteResponseMappingProfile()
        {
            CreateMap<Quote, GetQuoteResponseVm>();
        }
    }
}