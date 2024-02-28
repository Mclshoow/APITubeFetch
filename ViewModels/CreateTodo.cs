using System.ComponentModel.DataAnnotations;

namespace APITubefetch.ViewModels
{
    public class CreateTodoViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}

