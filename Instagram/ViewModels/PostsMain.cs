using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Instagram.Models;
namespace Instagram.ViewModels
{
    public class PostsMain
    {

        public user Mainuser { get; set; }

        public user user_page { get; set; }
        public List<user> users { get; set; }

        public user user_post { get; set; }
        public List<user_requests> friend1 { get; set; }
        public List<comment_posts> comments { get; set; }
        public post post { get; set; }

        public List<like> likes { get; set; }

    }
}