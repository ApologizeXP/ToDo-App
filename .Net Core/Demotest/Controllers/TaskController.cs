using Demotest.Data;
using Demotest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Demotest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private DemoContext _dbContext;
        public TaskController(DemoContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetTasks")]
        public IActionResult Get()
        {
            try
            {
                var tasks = _dbContext.Tasks.ToList();
                if (tasks.Count == 0)
                {
                    return StatusCode(404, "No user found");
                }
                return Ok(tasks);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error has occurred");
            }
        }

        [HttpGet("GetTasks/{Id}")]
        public IActionResult Getid(int Id)
        {
            try
            {
                var tasks = _dbContext.Tasks.Where(x => x.userId == Id).ToList();
                if (tasks == null)
                {
                    return StatusCode(404, "No user found");
                }
                return Ok(tasks);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error has occurred");
            }
        }

        [HttpGet("EditTasks/{userId}/{Id}")]
        public IActionResult EditTask([FromRoute] int Id, int userId)
            {
            try
            {
                var tasks = _dbContext.Tasks.Where(x => x.userId == userId).FirstOrDefault(x => x.taskId == Id);
                if (tasks == null)
                {
                    return StatusCode(404, "No user found");
                }
                return Ok(tasks);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error has occurred");
            }
        }

        [HttpPost("CreateTask/{Id}")]
        public IActionResult CreateTask(int Id , [FromBody] TaskRequest request)
        {
            var users = _dbContext.Users.FirstOrDefault(x => x.userid == Id);
            DateTime date = DateTime.Now;
            Tasks tasks = new Tasks();
            tasks.taskName = request.taskName;
            tasks.status = request.status;
            tasks.userId = Id;
            tasks.createDate = date;
            tasks.createBy = users.username;
            tasks.updateDate = date;
            tasks.updateBy = users.username;
            try
            {
                
                _dbContext.Tasks.Add(tasks);
                _dbContext.SaveChanges();
                
            }
            catch (Exception)
            {

                return StatusCode(500, "An error has occurred");
            }
            var task = _dbContext.Tasks.Where(x => x.userId == Id).ToList();
            return Ok(task);
        }

        [HttpPut("UpdateTask/{userId}/{Id}")]
        public IActionResult Update(int Id,int userId, [FromBody] TaskRequest request)
        {
            try
            {
                var users = _dbContext.Users.FirstOrDefault(x => x.userid == userId);
                DateTime date = DateTime.Now;
                var tasks = _dbContext.Tasks.Where(x => x.userId == userId).FirstOrDefault(x => x.taskId == Id);
                if (tasks == null)
                {
                    return StatusCode(404, "No user found");
                }
                tasks.taskName = request.taskName;
                tasks.status = request.status;
                tasks.userId = userId;
                tasks.createDate = date;
                tasks.createBy = users.username;
                tasks.updateDate = date;
                tasks.updateBy = users.username;

                _dbContext.Entry(tasks).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error has occurred");
            }
            var task = _dbContext.Tasks.Where(x => x.userId == Id).ToList();
            return Ok(task);
        }

        [HttpDelete("DeleteTask/{userId}/{Id}")]
        public IActionResult Delete([FromRoute] int Id , int userId)
        {
            try
            {
                var tasks = _dbContext.Tasks.Where(x => x.userId == userId).FirstOrDefault(x => x.taskId == Id);
                if (tasks == null)
                {
                    return StatusCode(404, "No user found");
                }
                _dbContext.Entry(tasks).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error has occurred");
            }
            var task = _dbContext.Tasks.Where(x => x.userId == Id).ToList();
            return Ok(task);
        }
    }
}
