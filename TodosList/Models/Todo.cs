using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodosList.Models
{
    public class Todo
    {
        [Key]
        public int TodoId { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public DateTime DateTime { get; set; }


        public virtual int CategoryId { get; set; }

        public virtual List<SubTodo> SubTodos { get; set; }
    }
}