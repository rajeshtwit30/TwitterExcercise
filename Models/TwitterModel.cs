
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitterexercise.Models
{
    public class TwitterModel
    {
        public List<Twitte> listTwitte { get; set; }
    }

    public class Twitte
    {
        public string Twittes { get; set; }
    }
}