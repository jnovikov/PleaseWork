using BackendApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    public class DeadlinesController : BaseController
    {
        public DeadlinesController(MyContext db) : base(db)
        {
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddDeadline([FromForm] Deadline deadline)
        {
            if (deadline == null)
            {
                ModelState.AddModelError("", "Не указаны параметры дедлайна");
                return BadRequest(ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = CurrentUser;

            deadline.UserId = user.Id;
            db.Deadlines.Add(deadline);
            db.SaveChanges();
            return Ok(new {message = "Deadline added"});
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetDeadlines()
        {
            var user = CurrentUser;
            var deadlines = db.Deadlines.Where(x => x.UserId == user.Id).ToArray();
            return Ok(new {deadlines = deadlines});
        }

//        [HttpPost]
//        public IActionResult DeleteDeadline([FromForm]Deadline deadline)
//        {
//            if (deadline == null)
//            {
//                ModelState.AddModelError("", "Не выбран дедлайн для удаления");
//                return BadRequest(ModelState);
//            }

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            db.Deadlines.FirstOrDefault(d => d.Name == deadline.Name);

//            db.SaveChanges();
//            return Ok(deadline);
//        }

//        [HttpPost]
//        public IActionResult EditDeadline([FromForm]Deadline deadline)
//        {
//            if (deadline == null)
//            {
//                ModelState.AddModelError("", "Не выбран дедлайн для редактирования");
//                return BadRequest(ModelState);
//            }

//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            db.Deadlines.FirstOrDefault(d => d.Name == deadline.Name);

//            db.SaveChanges();
//            return Ok(deadline);
//        }
//    }
    }
}