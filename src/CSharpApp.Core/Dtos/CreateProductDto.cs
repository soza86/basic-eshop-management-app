﻿namespace CSharpApp.Core.Dtos
{
    public class CreateProduct
    {
        public string? Title { get; set; }

        public int? Price { get; set; }

        public string? Description { get; set; }

        public List<string> Images { get; set; } = [];

        public int? CategoryId { get; set; }
    }
}
