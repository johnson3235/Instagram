using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Instagram.Models;

namespace Instagram.ViewModels
{
    public class UserSearch
    {
        public List<user> user { get; set; }
        public List<user_requests> friend1 { get; set; }
    }
}