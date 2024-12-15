using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class FeedsManager
	{
		private readonly FacebookFetcherService _facebookFetcher;
		private List<Thread> fetchingThreads;

		public FeedsManager(FacebookFetcherService facebookFetcher)
		{
			_facebookFetcher = facebookFetcher;
		}

		public void FetchFromAll()
		{
			fetchingThreads = new List<Thread>();

			for (int i = 0; i < 3; i++)
			{
				var thread = new Thread(() =>
				{
					var posts = _facebookFetcher.FetchPosts();
					FeedsStore.AddFeeds(posts);
				});
				fetchingThreads.Add(thread);
				thread.Start();
			}

			// Wait for all threads to complete
			foreach (var thread in fetchingThreads)
			{
				thread.Join();
			}

		}

	}
}
