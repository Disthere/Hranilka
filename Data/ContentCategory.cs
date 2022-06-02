using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hranilka.Data
{
    public class ContentCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DefaultValue(0)]
        public int ParentId { get; set; }


        public ICollection<DataContainer> DataContainer { get; set; }
        public ContentCategory()
        {
            this.DataContainer = new List<DataContainer>(); 
        }

    }
}
