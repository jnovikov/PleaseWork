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

        protected User CurrentUser => db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
        public BaseController(MyContext db)
        {
            this.db = db;
        }

        public BaseController()
        {

        }
    }
}
