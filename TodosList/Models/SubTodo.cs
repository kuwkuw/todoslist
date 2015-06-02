using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TodosList.Models
{
    public class SubTodo
    {
        [Key]
        public int SubTodoId { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        
        public virtual int TodoId { get; set; }
    }
}
