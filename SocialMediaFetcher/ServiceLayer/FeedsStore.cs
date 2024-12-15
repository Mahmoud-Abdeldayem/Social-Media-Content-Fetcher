using ServiceLayer.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public static class FeedsStore
	{
		public static ConcurrentBag<Feed> Feeds { get; private set; } = new();

		public static void AddFeeds(IEnumerable<Feed> feeds)
		{
			foreach (var post in feeds)
			{
				Feeds.Add(post);
			}
		}

		public static List<Feed> GetAllFeeds() => Feeds.Where(f => f.Content != null).ToList();
	}
}
