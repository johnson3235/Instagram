using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Instagram.Models;

namespace Instagram.ViewModels
{
    public class UserRequests
    {

        public user user_page { get; set; }
        public List<user> user_list { get; set; }
        public List<user_requests> friend1 { get; set; }
    }
}