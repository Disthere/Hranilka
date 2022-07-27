using Hranilka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hranilka.Data
{
    public class CurrentDataContainer : DataContainer
    {
        public CurrentDataContainer()
        {

        }
        public CurrentDataContainer(DataContainer dataContainer)
        {
            SetCurrentDataContainerProperties(dataContainer);
        }

        public string CategoryName { get; set; }

        private void SetCurrentDataContainerProperties(DataContainer dataContainer)
        {
            string categoryName = ContentCategoryRepozitory.GetCategoryNameById(dataContainer.CategoryId);
            //using (Context hranilkaDbContext = new Context())
            //{
            //    var currentDataContainer = hranilkaDbContext.DataContainers.Join(hranilkaDbContext.ContentCategories,
            //        u => u.CategoryId,
            //        c => c.Id,
            //        (u, c) => new
            //        {
            //            Id = u.Id,
            //            Description = u.Description,
            //            CreateDate = u.CreateDate,
            //            Category = c.Name,
            //            Author = u.Author,
            //            OtherInformation = u.OtherInformation,
            //            WebSiteDescription = u.WebSiteDescription,
            //            DataType = u.DataType
            //        }).Where(x => x.Id == dataContainer.Id).FirstOrDefault();


            //    this.Id = currentDataContainer.Id;
            //    this.Description = currentDataContainer.Description;
            //    this.CreateDate = currentDataContainer.CreateDate;
            //    this.CategoryName = currentDataContainer.Category;
            //    this.OtherInformation = currentDataContainer.OtherInformation;
            //    this.Author = currentDataContainer.Author;
            //    this.WebSiteDescription = currentDataContainer.WebSiteDescription;
            //    this.DataType = currentDataContainer.DataType;

            //}
                       
            this.Id = dataContainer.Id;
            this.Description = dataContainer.Description;
            this.CreateDate = dataContainer.CreateDate;
            this.CategoryName = categoryName;
            this.OtherInformation = dataContainer.OtherInformation;
            this.Author = dataContainer.Author;
            this.WebSiteDescription = dataContainer.WebSiteDescription;
            this.DataType = dataContainer.DataType;
        }

    }
}
