using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afisha.Models
{
    class FilmSession
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public Movie Movie { get; set; }
        public Theater Theater { get; set; }
    }
}
