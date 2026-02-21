using System;
using System.Collections.Generic;

namespace Task3_DIP
{
    //Abstraction
    public interface IProductRepository
    {
        List<string> GetProducts();
    }

    //Database Implementation
    public class DatabaseProductRepository : IProductRepository
    {
        public List<string> GetProducts()
        {
            return new List<string>
            {
                "Laptop - From Database",
                "Mouse - From Database"
            };
        }
    }

    //API Implementation
    public class ApiProductRepository : IProductRepository
    {
        public List<string> GetProducts()
        {
            return new List<string>
            {
                "Phone - From API",
                "Tablet - From API"
            };
        }
    }

    //File Implementation
    public class FileProductRepository : IProductRepository
    {
        public List<string> GetProducts()
        {
            return new List<string>
            {
                "Keyboard - From File",
                "Monitor - From File"
            };
        }
    }

    //Factory
    public class RepositoryFactory
    {
        public static IProductRepository Create(string type)
        {
            switch (type.ToLower())
            {
                case "db":
                    return new DatabaseProductRepository();

                case "api":
                    return new ApiProductRepository();

                case "file":
                    return new FileProductRepository();

                default:
                    throw new ArgumentException("Invalid data source type.");
            }
        }
    }

    //High-Level Module
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public void DisplayProducts()
        {
            var products = _repository.GetProducts();

            Console.WriteLine("\nProduct List:");
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose data source: db / api / file");
            string choice = Console.ReadLine();

            IProductRepository repository = RepositoryFactory.Create(choice);

            var productService = new ProductService(repository);
            productService.DisplayProducts();

            Console.ReadLine();
        }
    }
}