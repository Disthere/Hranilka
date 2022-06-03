using Hranilka.Data;
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
        public string Description { get; set; }

        //[DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string OtherInformation { get; set; }

        public int CategoryId { get; set; }
        public ContentCategory Category { get; set; }
    }
}
