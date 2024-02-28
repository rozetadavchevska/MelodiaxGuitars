﻿using MelodiaxGuitarsAPI.Models;

namespace MelodiaxGuitarsAPI.DTOs
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductDto> Products { get; set; }
    }
}