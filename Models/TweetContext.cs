using Microsoft.EntityFrameworkCore;


namespace TwitterApi.Models 
{
  public class TweetContext : DbContext
  {
    public TweetContext(DbContextOptions <TweetContext> options)
    :base(options)
    {

    }
    public DbSet<TwitterApi.Models.TweetItem> TweetItem { get; set; }
  }
}