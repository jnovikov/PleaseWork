using BackendApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    public class DeadlinesController : BaseController
    {

        [HttpPost]
        public IActionResult AddDeadline([FromForm]Deadline deadline)
        {
            if (deadline == null)
            {
                ModelState.AddModelError("", "Не указаны параметры дедлайна");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Deadlines.Add(deadline);
            db.SaveChanges();
            return Ok(deadline);
        }

        [HttpPost]
        public IActionResult DeleteDeadline([FromForm]Deadline deadline)
        {
            if (deadline == null)
            {
                ModelState.AddModelError("", "Не выбран дедлайн для удаления");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Deadlines.FirstOrDefault(d => d.Name == deadline.Name);

            db.SaveChanges();
            return Ok(deadline);
        }

        [HttpPost]
        public IActionResult EditDeadline([FromForm]Deadline deadline)
        {
            if (deadline == null)
            {
                ModelState.AddModelError("", "Не выбран дедлайн для редактирования");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Deadlines.FirstOrDefault(d => d.Name == deadline.Name);

            db.SaveChanges();
            return Ok(deadline);
        }
    }
}
