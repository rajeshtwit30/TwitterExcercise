using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System;
using Twitterexercise.Models;

namespace Twitterexercise
{
    public class TwitterService
    {
        public string OAuthConsumerSecret { get; set; }
        public string OAuthConsumerKey { get; set; }

        public async Task<TwitterModel> GetTwitts(string userName, int count, string accessToken = null)
        {
            TwitterModel twittModel=new TwitterModel();
            if (accessToken == null)
            {
                accessToken = await GetAccessToken().ConfigureAwait(false);
            }

            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name=Narendermodi_PM&trim_user=1&exclude_replies=1", count, userName));
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline).ConfigureAwait(false);
            var serializer = new JavaScriptSerializer();
            dynamic json = serializer.Deserialize<object>(await responseUserTimeLine.Content.ReadAsStringAsync());
            var enumerableTwitts = (json as IEnumerable<dynamic>);

            if (enumerableTwitts == null)
            {
                return null;
            }

            List<Twitte> lstTwitt = new List<Twitte>();
            foreach (string twitt in enumerableTwitts.Select(t => (string)(t["text"].ToString())))
            {
                Twitte temp = new Twitte();
                temp.Twittes = twitt;

                lstTwitt.Add(temp);


               
            }
            twittModel.listTwitte = lstTwitt;
  

       
             return twittModel;
           
        }
        



















        public async Task<string> GetAccessToken()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token");
            var customerInfo = Convert.ToBase64String(new UTF8Encoding().GetBytes(OAuthConsumerKey + ":" + OAuthConsumerSecret));
            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false);
             string json = await response.Content.ReadAsStringAsync();
            var serializer = new JavaScriptSerializer();
            dynamic item = serializer.Deserialize<object>(json);
            return item["access_token"];
        }
    }
}