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
    public class CategoryController : ApiController
    {
        private TodoRepository _todosRepository;

        public CategoryController()
        {
            _todosRepository = new TodoRepository();
        }

        [HttpGet]
        [Route("api/category")]
        public IEnumerable<TodoCategory> Get()
        {
            return _todosRepository.GetTodosList();
        }


        // POST api/category
        [HttpPost]
        [Route("api/category")]
        public IHttpActionResult Post(TodoCategory newCategory)
        {
            if (_todosRepository.AddCategory(newCategory))
            {
                return Ok(newCategory);
            }
            return NotFound();
        }

        // POST api/category/5
        [HttpPost]
        [Route("api/category/{Id}")]
        public IHttpActionResult Post(int id)
        {
            if (_todosRepository.CleanCategory(id))
            {
                return Ok(_todosRepository.GetCategory(id));
            }
            return NotFound();
        }

        // DELETE api/category/5
        public IHttpActionResult Delete(int id)
        {
            if (_todosRepository.DeleteCategory(id))
            {
                return Ok(id);
            }
            return NotFound();
        }
    }
}
