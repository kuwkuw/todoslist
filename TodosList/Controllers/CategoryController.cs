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

        [Route("api/category")]
        public IEnumerable<TodoCategory> Get()
        {
            return _todosRepository.GetTodosList();
        }

        // GET api/category/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/category
        public IHttpActionResult Post(TodoCategory newCategory)
        {
            if (_todosRepository.AddCategory(newCategory))
            {
                return Ok(newCategory);
            }
            return NotFound();
        }

        // PUT api/category/5
        public void Put(int id, [FromBody]string value)
        {
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
