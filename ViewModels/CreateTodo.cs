using System.ComponentModel.DataAnnotations;

namespace APITubefetch.ViewModels
{
    public class CreateTodoViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}

