namespace Site6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Post
    {
        public Post()
        {
            this.Comment = new HashSet<Comment>();
        }
    
        public int Id { get; set; }
        [Display(Name = "Оглавление")]
        public string Name { get; set; }
        [Display(Name = "Текст поста")]
        public string Text { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
