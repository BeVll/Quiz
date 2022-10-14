using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Quiz2
{
    internal class Question
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Неверный логин. Количетсво символов должно быть от {2} до {1}")]
        public string Name { get; set; }
        public List<string> wrong_answers { get; set; }
        public List<string> correct_answers { get; set; }
        public int count_points { get; set; }
        public Question()
        {
            wrong_answers = new List<string>();
            correct_answers = new List<string>();
            count_points = 0;
        }
       
    }
}
