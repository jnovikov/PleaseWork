using Microsoft.AspNetCore.Mvc;
using BackendApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : BaseController
    {
        public TasksController(MyContext db) : base(db) { }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = db.Tasks.FirstOrDefault(t => t.Id == id);

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
        public IActionResult EditTask(int id)
        {
            var task = db.Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                ModelState.AddModelError("", "Не выбрано задание для редактирования");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //TO DO: редактирование задания
            // Как будет происходить редактирование?


            db.SaveChanges();
            return Ok(task);
        }
    }
}
