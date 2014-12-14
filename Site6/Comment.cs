namespace Site6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string UserName { get; set; }
        [Display(Name = "Текст сообщения")]
        public string Text { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual Post Post { get; set; }
    }
}
