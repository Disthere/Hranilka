using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hranilka.Data
{
    public class CurrentDataContainer
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string OtherInformation { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
        public string WebSiteDescription { get; set; }

        public CurrentDataContainer(DataContainer dataContainer)
        {
            SetCurrentDataContainerProperties(dataContainer);
        }

        public CurrentDataContainer()
        {

        }

        private void SetCurrentDataContainerProperties(DataContainer dataContainer)
        {
            using (Context hranilkaDbContext = new Context())
            {
                var currentDataContainer = hranilkaDbContext.DataContainers.Join(hranilkaDbContext.ContentCategories,
                    u => u.CategoryId,
                    c => c.Id,
                    (u, c) => new
                    {
                        Id = u.Id,
                        Description = u.Description,
                        CreateDate = u.CreateDate,
                        Category = c.Name,
                        Author = u.Author,
                        OtherInformation = u.OtherInformation,
                        WebSiteDescription = u.WebSiteDescription
                    }).Where(x => x.Id == dataContainer.Id).FirstOrDefault();

                this.Id = currentDataContainer.Id;
                this.Description = currentDataContainer.Description;
                this.CreateDate = currentDataContainer.CreateDate;
                this.Category = currentDataContainer.Category;
                this.OtherInformation = currentDataContainer.OtherInformation;
                this.Author = currentDataContainer.Author;
                
            }
        }

    }
}
