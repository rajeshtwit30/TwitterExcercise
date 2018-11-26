using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Twitterexercise.Models;

namespace Twitterexercise.Controllers
{
    public class TwitterController : ApiController
    {
        // GET api/twitter
        [System.Web.Http.Cors.EnableCors(origins: "https://api.twitter.com/oauth2/token", headers: "*", methods: "*")]
        public TwitterModel Get(int id)
        {
           

            var twitter = new TwitterService
            {
                OAuthConsumerKey = "ck1ZhyPddp9nxHTfJMatknFan",
                OAuthConsumerSecret = "49qmaXbqQpWUXQ3iJofdgungK06FOUNTJ3ldD8md1i9CwZIW2O"
            };
            TwitterModel twitts = twitter.GetTwitts("showtweetmyrgp", 10).Result;
            return twitts;
        }

       

        // DELETE api/twitter/5
        public void Delete(int id)
        {
        }
    }
}
