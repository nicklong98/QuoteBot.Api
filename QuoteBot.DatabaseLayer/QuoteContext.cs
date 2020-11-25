using Microsoft.EntityFrameworkCore;
using QuoteBot.DatabaseLayer.Entities;

namespace QuoteBot.DatabaseLayer
{
    public class QuoteContext : DbContext
    {
        public DbSet<Quote> Quotes { get; set; }
        
        public QuoteContext(DbContextOptions<QuoteContext> options) : base(options)
        {
            
        }
    }
}