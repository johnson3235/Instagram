using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Instagram.Models;
namespace Instagram.ViewModels
{
    public class UpdateLike
    {
        public user user { get; set; }

        public post post { get; set; }

        public like likes { get; set; }
    }
}