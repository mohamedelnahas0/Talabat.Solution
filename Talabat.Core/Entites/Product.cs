using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entites
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set;}

        //[ForeignKey(nameof(Product.Brand))] //not needed -> Data annotation (i will work with FluentApi)
        public int BrandId { get; set; } //forigekey Column From => ProductBrand

        
        public ProductBrand Brand { get; set; } //Navigational Property [One] 

        public int CategoryId { get; set; } //forigekey Column From => ProductCategory
        public ProductCategory Category { get; set; } //Navigational Property [One] 
    }
}
