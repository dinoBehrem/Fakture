using Fakture.Entities;
using Fakture.ViewModels.Fakture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fakture.ViewModels.User
{
    public class UserVM
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public IEnumerable<FakturaVM> Fakture { get; set; }
    }
}