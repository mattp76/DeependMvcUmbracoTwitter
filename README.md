# DeependMvcUmbracoTwitter

My solution has been built using MVC against Umbraco version 7.4.3. The homepage should display a Twitter feed.

Note: The CMS has been used to add afew details such as the Twitter keys, tokens, caching duration, twitter account name(s), 
and max feed count. The code then pulls these out the CMS and into the Twitter API. The library I used is TweetSharp.

To log in to the CMS please go to localhost:.../umbraco/ ...

Username: mattp_76@hotmail.com
Password: default

Please enable nuget restore to bring down the relevant dependancies. I am hoping this runs for you at first build, if not please re-compile and try
again.

I did a tiny bit of front end, just to tidy up the feed. Bootstrap was used to help with this.


