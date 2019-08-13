using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Production.Lib.Services.IdentityService
{
    public class IdentityService : IIdentityService
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public int TimezoneOffset { get; set; }
    }
}
