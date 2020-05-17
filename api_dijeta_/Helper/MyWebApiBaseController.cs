using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_dijeta_data;
using Microsoft.AspNetCore.Mvc;

namespace api_dijeta_.Helper
{
    [Route("api/[controller]/[action]")]
    public abstract class MyWebApiBaseController : Controller
        
    {
        protected readonly MyContext myContext;
        protected MyWebApiBaseController(MyContext myContext) { this.myContext = myContext; }
    }
}