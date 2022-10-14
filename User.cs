using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Quiz2
{
    internal class User
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Неверный логин. Количетсво символов должно быть от {2} до {1}")]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime Birthday_date { get; set; }  
        public string Type { get; set; }

    }
}
