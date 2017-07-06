using Microsoft.AspNetCore.Authorization; // [Authorize]
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginDemo.Controllers
{
    [Authorize]
    public class HomeController
    {
        [AllowAnonymous]
        public string Index()
        {
            return "我是首页";
        }

        public string Jimi()
        {
            return "这是国家机密，非授权不能访问。";
        }
    }
}
