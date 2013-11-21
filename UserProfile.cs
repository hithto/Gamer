using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth1._0a_OpenTQQ_Demo
{
    [Serializable]
    public class UserProfile
    {
        public UserProfile() { }
        public string result { get; set; }
        public string resultMsg { get; set; }
        public string userid { get; set; }
        public string nickname { get; set; }
        public string race { get; set; }
        public string career { get; set; }
        public string level { get; set; }
        public string hp { get; set; }
        public string mp { get; set; }
        public string str { get; set; }
        public string vit { get; set; }
        public string dex { get; set; }
        public string agi { get; set; }
        public string inta { get; set; }
        public string mnd { get; set; }
        public string luk { get; set; }

    }
}