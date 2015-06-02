using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using TodosList.Models;

namespace TodosList.Services
{
    public  class TodoRepository: IDisposable
    {
        TodoContext _context = new TodoContext();

        /// <summary>
        /// Get all todos
        /// </summary>
        /// <returns>todo list</returns>
        public IEnumerable<TodoCategory> GetTodosList()
        {
            return _context.TodoCategories.ToList();
        } 

        /// <summary>
        /// Get tofo by Id
        /// </summary>
        /// <param name="id">todo id</param>
        /// <returns>Todo class</returns>
        //public Todo GetTodo(int id)
        //{
        //    return _context.Todos.Find(id);
        //}

        /// <summary>
        /// Add new category
        /// </summary>
        /// <param name="newCategory"> new category object</param>
        /// <returns>result of adding new record to database</returns>
        public bool AddCategory(TodoCategory newCategory)
        {
            try
            {
                _context.TodoCategories.Add(newCategory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Delete category from database
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>result of deleting</returns>
        public bool DeleteCategory(int id)
        {
            try
            {
                var category = _context.TodoCategories.Find(id);
                _context.TodoCategories.Remove(category);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Add new todo item in database
        /// </summary>
        /// <param name="newTodo">new item</param>
        /// <returns>result of adding</returns>
        public bool AddTodo(Todo newTodo)
        {
            try
            {
                _context.Todos.Add(newTodo);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete todo from database
        /// </summary>
        /// <param name="id">Todo id</param>
        /// <returns>result of deleting</returns>
        public bool DeleteTodo(int id)
        {
            try
            {
                var todo = _context.Todos.Find(id);
                _context.Todos.Remove(todo);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Update todo 
        /// </summary>
        /// <param name="newTodo">new state</param>
        /// <returns>result of ubdating</returns>
        public bool UpdateTodo(Todo newTodo, out Todo todo)
        {
            todo = new Todo();
            try
            {
                todo = _context.Todos.Find(newTodo.TodoId);
                if (todo != null)
                {
                    if (todo.IsDone != newTodo.IsDone)//change state of todo
                    {
                        todo.IsDone = newTodo.IsDone;

                        if (todo.IsDone)
                        {
                            //change state of all subtodos to true
                            todo.SubTodos.Where(i => i.IsDone == false).ToList().ForEach(subtodo => subtodo.IsDone = true);
                        }
                        else
                        {
                            //change state of all subtodos to false
                            todo.SubTodos.Where(i => i.IsDone == true).ToList().ForEach(subtodo => subtodo.IsDone = false);
                        }
                    }

                    _context.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            
            return false;
        }

        /// <summary>
        /// Add new Sub todo
        /// </summary>
        /// <param name="newSubTodo">new item</param>
        /// <returns>result of adding</returns>
        public bool AddSubTodo(SubTodo newSubTodo)
        {
            try
            {
                _context.SubTodos.Add(newSubTodo);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }   
        }

        /// <summary>
        /// Delete subtodo from database
        /// </summary>
        /// <param name="id">subtodo id</param>
        /// <returns>result of deleting</returns>
        public bool DeleteSubTodo(int id)
        {
            try
            {
                var subtodo = _context.SubTodos.Find(id);
                _context.SubTodos.Remove(subtodo);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Update subtodo 
        /// </summary>
        /// <param name="newSubTodo">new state</param>
        /// <returns>result of ubdating</returns>
        public bool UpdateSubTodo(SubTodo newSubTodo)
        {
            try
            {
                var todo = _context.Todos.Find(newSubTodo.TodoId);
                if (todo != null)
                {
                    var subTodo = todo.SubTodos.FirstOrDefault(item=>item.SubTodoId==newSubTodo.SubTodoId);

                    if (subTodo != null)
                    {
                        subTodo.IsDone = newSubTodo.IsDone;

                        if (!todo.SubTodos.Where(subtodo => subtodo.IsDone == false).Any() && !todo.IsDone)
                        {
                            todo.IsDone = true;
                        }
                    }

                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public void Dispose()
        {
            _context.Dispose();
        }




    }
}