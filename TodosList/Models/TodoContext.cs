using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TodosList.Models
{
    public class TodoContext: DbContext
    {
        public TodoContext(): base("todolist"){}
        public DbSet<TodoCategory> TodoCategories { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<SubTodo> SubTodos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}