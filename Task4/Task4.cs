using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Task4_DIP
{
    public interface IProductRepository
    {
        List<string> GetProducts();
    }

    public class DatabaseProductRepository : IProductRepository
    {
        public List<string> GetProducts()
            => new List<string> { "Laptop - DB", "Mouse - DB" };
    }

    public class ApiProductRepository : IProductRepository
    {
        public List<string> GetProducts()
            => new List<string> { "Phone - API", "Tablet - API" };
    }

    public class FileProductRepository : IProductRepository
    {
        public List<string> GetProducts()
            => new List<string> { "Keyboard - File", "Monitor - File" };
    }

    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public void DisplayProducts()
        {
            foreach (var product in _repository.GetProducts())
                Console.WriteLine(product);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose source: db / api / file");
            string choice = Console.ReadLine()?.ToLower();

            var services = new ServiceCollection();

            services.AddTransient<IProductRepository>(provider =>
            {
                return choice switch
                {
                    "db" => new DatabaseProductRepository(),
                    "api" => new ApiProductRepository(),
                    "file" => new FileProductRepository(),
                    _ => throw new ArgumentException("Invalid source")
                };
            });

            services.AddTransient<ProductService>();

            var provider = services.BuildServiceProvider();

            var productService = provider.GetRequiredService<ProductService>();
            productService.DisplayProducts();

            Console.ReadLine();
        }
    }
}