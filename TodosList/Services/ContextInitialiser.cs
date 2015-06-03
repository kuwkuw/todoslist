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
                new TodoCategory{
                    Name = "Home",
                    Todos = new List<Todo>()
                    {
                        new Todo{DateTime = new DateTime(2015, 6, 4), Text = "Clean the house", IsDone = false, SubTodos = new List<SubTodo>()
                        {
                            new SubTodo{IsDone = false, Text = "Wash dishes"},
                            new SubTodo{IsDone = false, Text = "Cleaning in bathroom"},
                            new SubTodo{IsDone = false, Text = "Cleaning in bedroom"}
                        }},
                        new Todo{DateTime = new DateTime(2015,6,4), Text = "Sort books"}
                    }
                },
                new TodoCategory
                {
                     Name = "Work",
                     Todos = new List<Todo>()
                     {
                         new Todo
                         {
                             DateTime = new DateTime(2015,6,4), Text = "Do my work", IsDone = false
                         }
                         
                     }
                },
                new TodoCategory
                {
                    Name = "Other",
                    Todos = new List<Todo>()
                    {
                        new Todo{DateTime = new DateTime(2015,6,4),Text = "Shopping", IsDone = false, SubTodos = new List<SubTodo>()
                        {
                            new SubTodo{IsDone = false, Text = "Buy new shirt"}
                        }}
                    }
                }
            });


            context.SaveChanges();
        }
    }
}