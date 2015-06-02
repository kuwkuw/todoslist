using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodosList.Models;
using TodosList.Services;

namespace TodosList.Controllers
{
    public class SubTodoController : ApiController
    {
        private TodoRepository _todosRepository;
        public SubTodoController()
        {
            _todosRepository = new TodoRepository();
        }
        // GET api/subtodo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/subtodo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/subtodo
        [HttpPost]
        [Route("api/subtodo")]
        public IHttpActionResult Post(SubTodo newSubTodo)
        {
            if (_todosRepository.AddSubTodo(newSubTodo))
            {
                return Ok(newSubTodo);
            }
            return NotFound();
        }

        // PUT api/subtodo/5
        [HttpPost]
        [Route("api/subtodo/{id}")]
        public IHttpActionResult Put(int id, SubTodo subTodo)
        {
            if (_todosRepository.UpdateSubTodo(subTodo))
            {
                return Ok(subTodo);
            }

            return NotFound();
        }

        // DELETE api/subtodo/5
        public IHttpActionResult Delete(int id)
        {
            if (_todosRepository.DeleteSubTodo(id))
            {
                return Ok(id);
            }
            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_todosRepository != null)
                {
                    _todosRepository.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
