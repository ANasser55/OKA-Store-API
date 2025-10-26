using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKA.Application.DTOs.Categories
{
    public class CreateOrUpdateCategoryDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
