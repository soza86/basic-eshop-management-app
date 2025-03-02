﻿using CSharpApp.Application.Mappings;
using CSharpApp.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpApp.Application.Configuration
{
    public static class MappingConfiguration
    {
        public static IServiceCollection AddMappingConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IMapper<ProductServiceModel, Product>, CustomProductMapper>();
            services.AddTransient<IMapper<Product, ProductServiceModel>, CustomProductMapper>();
            services.AddTransient<IMapper<CreateProduct, CreateProductServiceModel>, CustomProductMapper>();
            services.AddTransient<IMapper<CategoryServiceModel, Category>, CustomCategoryMapper>();
            services.AddTransient<IMapper<Category, CategoryServiceModel>, CustomCategoryMapper>();
            services.AddTransient<IMapper<CreateCategory, CreateCategoryServiceModel>, CustomCategoryMapper>();
            return services;
        }
    }
}
