using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Contract.Register.Response
{
    public class RegisterResponse
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public string Error { get; set; }
    }
}
