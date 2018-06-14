using Microsoft.AspNetCore.Mvc;
using BackendApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Task = BackendApi.Models.Task;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : BaseController
    {
        public TasksController(MyContext db) : base(db)
        {
        }

        [HttpGet]
        [Authorize]
        public IActionResult FindTask()
        {
            var tasks = db.Tasks.Where(x => x.Deadline.UserId == CurrentUser.Id).ToArray();
            return Ok(tasks);
        }

        [HttpPost("{id}/done")]
        [Authorize]
        public IActionResult SetDone(int id)
        {
            var task = findTask(id);
            task.IsDone = true;
            db.SaveChanges();
            return Ok(task);
        }
        
        [HttpPost("{id}/undone")]
        [Authorize]
        public IActionResult SetUndone(int id)
        {
            var task = findTask(id);
            task.IsDone = false;
            db.SaveChanges();
            return Ok(task);
        }
        
        

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteTask(int id)
        {
            var task = findTask(id);

            if (task == null)
            {
                ModelState.AddModelError("", "Не выбрано задание для удаления");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            db.Tasks.Remove(task);
            db.SaveChanges();
            return Ok(task);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult EditTask(int id, [FromForm] Task _task)
        {
            var task = findTask(id);

            if (task == null)
            {
                ModelState.AddModelError("", "Не выбрано задание для редактирования");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            task.Name = _task.Name;
            task.WorkTime = _task.WorkTime;
            db.SaveChanges();
            return Ok(task);
        }

        private Task findTask(int id)
        {
            var task = db.Tasks.FirstOrDefault(x => x.Id == id);
            var deadline = db.Deadlines.FirstOrDefault(x => x.Id == task.DeadlineId && x.UserId == CurrentUser.Id);
            return deadline != null ? task : null;
        }
    }
}