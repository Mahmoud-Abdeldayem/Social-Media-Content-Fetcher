using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
	public class FacebookPost
	{
		public string CreatedDate { get; set; }

		public string? Message { get; set; }
		public string? Id { get; set; }
	}


	public class FacebookPaginationDetails
	{
		public string Next { get; set; }
		public string Previous { get; set; }
	}

	//يارب نخلص
	public class FacebookPosts
	{
		public List<FacebookPost> Data { get; set; }
		public FacebookPaginationDetails Paging { get; set; }
	}

	public class FPosts
	{
		public FacebookPosts Posts { get; set; }
	}
}
