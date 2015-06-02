using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodosList.Models;

namespace TodosList.Services
{
    public class ContextInitialiser: DropCreateDatabaseIfModelChanges<TodoContext>
    {
        protected override void Seed(TodoContext context)
        {
            context.TodoCategories.AddRange(new TodoCategory[]
            {
                new TodoCategory{Name = "Home"
                    , Todos = new List<Todo>()
                {
                    new Todo{DateTime = new DateTime(2015, 6, 4), Text = "cleaning in home", IsDone = false, SubTodos = new List<SubTodo>()
                    {
                        new SubTodo{IsDone = false, Text = "wash dishes"},
                        new SubTodo{IsDone = false, Text = "cleaning in bathroom"},
                        new SubTodo{IsDone = false, Text = "cleaning in bedroom"}
                    }}
                }
                }
            });


            context.SaveChanges();
        }
    }
}