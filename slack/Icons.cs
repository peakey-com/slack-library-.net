using System;


namespace Slack
{


	//https://api.slack.com/events/bot_added


	public class Icons
	{


		private String _image_48;


		public Icons(dynamic Data)
		{
			_image_48 = Data.images_48;
		}


		public String image_48
		{
			get
			{
				return _image_48;
			}
		}


	}


}
