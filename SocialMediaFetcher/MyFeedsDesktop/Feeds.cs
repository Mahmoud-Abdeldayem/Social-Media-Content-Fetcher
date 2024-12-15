using ServiceLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFeedsDesktop
{


	public partial class Feeds : Form
	{
		HttpClient httpClient;
		FacebookFetcherService facebookFetcher;
		FeedsManager feedsManager;
		public Feeds()
		{
			InitializeComponent();

			//Inject objects to each others
			httpClient = new HttpClient();
			facebookFetcher = new FacebookFetcherService(httpClient);
			feedsManager = new FeedsManager(facebookFetcher);


			feedsManager.FetchFromAll();
			GenerateFeed();
			// Calling the threads maker 
		}

		private void GenerateFeed()
		{

			//Getting Feeds from the thread safe collection

			var feeds = FeedsStore.GetAllFeeds();


			// Main feed container
			Panel feedPanel = new Panel
			{
				Dock = DockStyle.Fill,
				AutoScroll = true
			};
			this.Controls.Add(feedPanel);



			// Generate feed items  

			//This variable controls the y axis in the form
			int yPosition = 10;

			foreach (var feed in feeds)
			{
				// Create a feed item panel
				Panel feedItem = new Panel
				{
					Size = new Size(feedPanel.Width - 40, 100),
					Location = new Point(10, yPosition),
					BorderStyle = BorderStyle.FixedSingle
				};

				// Profile Image
				PictureBox profilePicture = new PictureBox
				{
					Size = new Size(50, 50),
					Location = new Point(10, 10),

					//Loading images from the path and load the image by the paltform name
					Image = Image.FromFile(@$"D:\FCAI_Level_4_S_1\Parallel Processing\Social_Media_Fetcher\SocialMediaFetcher\MyFeeds\wwwroot\{feed.Platform}.png"),
					SizeMode = PictureBoxSizeMode.Zoom
				};
				feedItem.Controls.Add(profilePicture);

				// Feed Date
				Label dateLabel = new Label
				{
					Text = feed.FeedDate,
					Location = new Point(70, 10),
					AutoSize = true,
					Font = new Font("Arial", 8, FontStyle.Italic)
				};
				feedItem.Controls.Add(dateLabel);

				// Feed Content
				Label contentLabel = new Label
				{
					Text = feed.Content,
					Location = new Point(70, 30),
					Size = new Size(feedItem.Width - 80, 60),
					Font = new Font("Arial", 10),
					AutoEllipsis = true
				};
				feedItem.Controls.Add(contentLabel);

				// Add feed item to the feed panel
				feedPanel.Controls.Add(feedItem);

				yPosition += 110; // Move to the next position
			}
		}

		private void Feeds_Load(object sender, EventArgs e)
		{

		}
	}
}
