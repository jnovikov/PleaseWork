using BackendApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Controllers
{
    public class BaseController: Controller
    {
        protected MyContext db;

        public BaseController(MyContext db)
        {
            this.db = db;
        }

        public BaseController()
        {

        }
    }
}
