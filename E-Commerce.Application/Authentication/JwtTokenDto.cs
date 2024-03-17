using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Authentication
{
    public class JwtTokenDto
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public string Error { get; set; }
    }
}
