using MyFeeds.Models;

namespace MyFeeds.Services.Interfaces
{
    public interface IPlatFormFetcher
    {
        List<Feed> FetchFeeds();
    }
}
