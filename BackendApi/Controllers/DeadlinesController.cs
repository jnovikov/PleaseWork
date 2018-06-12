using BackendApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    public class DeadlinesController : BaseController
    {
        public DeadlinesController(MyContext db) : base(db) { }

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
            return Ok(new ObjectResult(deadlines));
        }

        [HttpPost("{id}")]
        [Authorize]
        public IActionResult AddTask(int id, [FromForm]Models.Task task)
        {
            if (task == null)
            {
                ModelState.AddModelError("", "Не указаны параметры задания");
                return BadRequest(ModelState);
            }

            var deadline = db.Deadlines.FirstOrDefault(d => d.Id == id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            task.DeadlineId = deadline.Id;
            db.Tasks.Add(task);
            db.SaveChanges();
            return Ok(task);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteDeadline(int id)
        {
            var deadline = db.Deadlines.FirstOrDefault(d => d.Id == id);

            if (deadline == null)
            {
                ModelState.AddModelError("", "Не выбран дедлайн для удаления");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            db.Deadlines.Remove(deadline);
            db.SaveChanges();
            return Ok(deadline);
        }

        [HttpPut("{id}")]
        public IActionResult EditDeadline(int id)
        {
            var deadline = db.Deadlines.FirstOrDefault(d => d.Id == id);

            if (deadline == null)
            {
                ModelState.AddModelError("", "Не выбран дедлайн для редактирования");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //TO DO: редактирование дедлайна
            // Как будет происходить редактирование?


            db.SaveChanges();
            return Ok(deadline);
        }
    }
}
