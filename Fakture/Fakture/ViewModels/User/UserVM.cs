using Fakture.Entities;
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
        public IEnumerable<Faktura> Fakture { get; set; }
    }
}