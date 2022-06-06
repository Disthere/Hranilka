using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hranilka.Data
{
    class CurrentCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; } = 0;
        public int InfoType { get; set; } = 0;
    }
}
