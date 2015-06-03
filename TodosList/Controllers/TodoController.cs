using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.SqlServer.Server;
using TodosList.Models;
using TodosList.Services;

namespace todolist.Controllers
{
    public class TodoController : ApiController
    {
        private TodoRepository _todosRepository;

        public TodoController()
        {
            _todosRepository = new TodoRepository();
        }

        // POST api/todo
        [HttpPost]
        [Route("api/todo")]
        public IHttpActionResult PostTodo(Todo newTodo)
        {
            Todo td;
            if (_todosRepository.AddTodo(newTodo,out td))
            {
                return Ok(td);
            }

            return NotFound();
        }

        // PUT api/todo/5
        [HttpPost]
        [Route("api/todo/{id}")]
        public IHttpActionResult Put(int id, Todo todo)
        {
            Todo newTodo;
            if (_todosRepository.UpdateTodo(todo, out newTodo))
            {
                return Ok(newTodo);
            }

            return NotFound();
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            if (_todosRepository.DeleteTodo(id))
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