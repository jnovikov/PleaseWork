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
            foreach (var deadline in deadlines)
            {
                deadline.Tasks = db.Tasks.Where(t => t.DeadlineId == deadline.Id).ToArray();
            }
            return Ok(deadlines);
        }

        [HttpPost("{id}")]
        [Authorize]
        public IActionResult AddTask(int id, [FromForm] Models.Task task)
        {
            if (task == null)
            {
                ModelState.AddModelError("", "Не указаны параметры задания");
                return BadRequest(ModelState);
            }

            var deadline = FindDeadline(id);

            if (deadline == null)
            {
                return NotFound(new {message = "Дедлайн не найден"});
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            task.Deadline = deadline;
            db.Tasks.Add(task);
            db.SaveChanges();
            return Ok(task);
        }
        
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetTasksForDeadline(int id)
        {
            var deadline = FindDeadline(id);

            if (deadline == null)
            {
                return NotFound(new {message = "Дедлайн не найден"});
            }

            var tasks = db.Tasks.Where(x => x.DeadlineId == deadline.Id).ToArray();
            return Ok(tasks);
        }
        


        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteDeadline(int id)
        {
            var deadline = FindDeadline(id);

            if (deadline == null)
            {
                ModelState.AddModelError("", "Не выбран дедлайн для удаления");
                return BadRequest(ModelState);
            }

            db.Deadlines.Remove(deadline);
            db.SaveChanges();
            return Ok(deadline);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult EditDeadline(int id, [FromForm] Deadline _deadline)
        {
            var deadline = FindDeadline(id);

            if (deadline == null)
            {
                ModelState.AddModelError("", "Не выбран дедлайн для редактирования");
                return NotFound(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            deadline.Finish = _deadline.Finish;
            deadline.Name = _deadline.Name;
            db.SaveChanges();
            return Ok(deadline);
        }

        private Deadline FindDeadline(int id)
        {
            return db.Deadlines.FirstOrDefault(d => d.Id == id && d.UserId == CurrentUser.Id);
        }
    }
}