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
        public ContentCategory()
        {
            this.DataContainer = new List<DataContainer>();
        }
        public ContentCategory(int id, string name) => (Id, Name) = (id, name);
        
        public int Id { get; set; }
        public string Name { get; set; }

        [DefaultValue(0)]
        public int ParentId { get; set; }

        [DefaultValue(0)]
        public int? DataType { get; set; }


        public ICollection<DataContainer> DataContainer { get; set; }





    }
}
