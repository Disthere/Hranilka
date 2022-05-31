using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hranilka
{
    public class DataContainer
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public int? CategoryLevel { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        
    }
}
