using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.Web.Identity
{
    public class TokenWrap
    {
        public string Token { set; get; }
        public DateTime ExpireDate { set; get; }
        public DateTime CreateDate { set; get; }
        public double LifeHours { set; get; }
        public bool IsAdmin { set; get; }
        public string Email { set; get; }
        public string UserId { set; get; }
    }
}
