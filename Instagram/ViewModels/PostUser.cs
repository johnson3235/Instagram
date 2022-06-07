using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Instagram.Models;

namespace Instagram.ViewModels
{
    public class PostUser
    {

        public user Mainuser { get; set; }
        public List<user> user { get; set; }
        public user user_page { get; set; }
        public List<user_requests> Request { get; set; }
        public List<user_requests> friend1 { get; set; }
        public List<post> post { get; set; }
        public List<comment_posts> comments { get; set; }
        public comment_posts comment { get; set; }
        public List<like> likes { get; set; }


    }
}