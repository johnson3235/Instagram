using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Instagram.Models;

namespace Instagram.ViewModels
{
    public class FriendUser
    {
        public user Mainuser { get; set; }

        public user user_page { get; set; }
        public List<user> user { get; set; }

        public List<user_requests> friend1 { get; set; }

        public List<user_requests> friend2 { get; set; }




    }
}