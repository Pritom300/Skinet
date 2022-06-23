using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string  Name { get; set; }    
        public string Description { get; set; }  
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        public String ProductType { get; set; }
        public String ProductBrand { get; set; }
    
    }
}