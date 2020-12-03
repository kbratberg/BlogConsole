using System;
using NLog.Web;
using System.IO;
using System.Linq;
using NorthwindConsole.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NorthwindConsole
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

            try
            {
                string choice;
                do
                {
                    Console.WriteLine("1) Edit a Category");
                    Console.WriteLine("2) Add Category");
                    Console.WriteLine("3) Display Category and related products");
                    Console.WriteLine("4) Display all Categories");
                    Console.WriteLine("5) Display Products");
                    Console.WriteLine("6) Add Product");
                    Console.WriteLine("7) Edit a Product");
                    Console.WriteLine("8) Display all Products");
                    Console.WriteLine("9) Display Individual Product");
                    Console.WriteLine("\"q\" to quit");
                    choice = Console.ReadLine();
                    Console.Clear();


                    logger.Info($"Option {choice} selected");
                    if (choice == "4")
                    {
                        var db = new NorthwindConsole_32_KMBContext();
                        var query = db.Categories.OrderBy(p => p.CategoryName);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{query.Count()} records returned");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        foreach (var item in query)
                        {
                            Console.WriteLine($"{item.CategoryName} - {item.Description}");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (choice == "8")
                    {
                        
                        Console.WriteLine("Select Which Products to Display");
                        Console.WriteLine("1)Display all Products");
                        Console.WriteLine("2)Display Active Products");
                        Console.WriteLine("3)Display Discontinued Products");

                        var displayChoice = Console.ReadLine();
                        logger.Info($"Ypur choice: {displayChoice}");
                        if(displayChoice != "1" || displayChoice != "2" || displayChoice != "3" ){
                            logger.Info("Invalid Choice");
                        }
                        var db = new NorthwindConsole_32_KMBContext();

                        if (displayChoice == "1")
                        {

                            var query = db.Products.OrderBy(p => p.ProductName);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{query.Count()} records returned");
                            Console.ForegroundColor = ConsoleColor.White;
                            foreach (var item in query)
                            {
                                if (item.Discontinued == true)
                                {
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                Console.WriteLine($"{item.ProductName}");
                            }
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (displayChoice == "2")
                        {
                            var query = db.Products.Where(p => p.Discontinued == false).OrderBy(p => p.ProductName);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{query.Count()} records returned");
                            Console.ForegroundColor = ConsoleColor.White;
                            foreach (var item in query)
                            {
                                
                                    Console.WriteLine($"{item.ProductName}");
                                
                            }

                        }
                        if (displayChoice == "3")
                        {
                            var query = db.Products.Where(p => p.Discontinued == true).OrderBy(p => p.ProductName);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{query.Count()} records returned");
                            Console.ForegroundColor = ConsoleColor.White;
                            foreach (var item in query)
                            {
                                
                                    Console.WriteLine($"{item.ProductName}");
                                
                            }

                        }
                    }
                    else if (choice == "2")
                    {
                        var db = new NorthwindConsole_32_KMBContext();
                        Category category = InputCategory(db);

                        if (category != null)
                        {
                            db.Categories.Add(category);
                            db.SaveChanges();
                        }
                    }
                    else if (choice == "3")
                    {
                        var db = new NorthwindConsole_32_KMBContext();
                        var query = db.Categories.OrderBy(p => p.CategoryId);

                        Console.WriteLine("Select the category whose products you want to display:");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        foreach (var item in query)
                        {
                            Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        int id = int.Parse(Console.ReadLine());
                        Console.Clear();
                        logger.Info($"CategoryId {id} selected");
                        Category category = db.Categories.Include("Products").FirstOrDefault(c => c.CategoryId == id);
                        Console.WriteLine($"{category.CategoryName} - {category.Description}");
                        foreach (Product p in category.Products)
                        {
                            Console.WriteLine(p.ProductName);
                        }
                    }
                    else if (choice == "4")
                    {
                        var db = new NorthwindConsole_32_KMBContext();
                        var query = db.Categories.Include("Products").OrderBy(p => p.CategoryId);
                        foreach (var item in query)
                        {
                            Console.WriteLine($"{item.CategoryName}");
                            foreach (Product p in item.Products)
                            {
                                Console.WriteLine($"\t{p.ProductName}");
                            }
                        }
                    }
                    Console.WriteLine();

                } while (choice != "q");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }
        public static Category GetCategory(NorthwindConsole_32_KMBContext db)
        {

            // display all blogs
            var categories = db.Categories.OrderBy(b => b.CategoryId);
            foreach (Category c in categories)
            {
                Console.WriteLine($"{c.CategoryId}: {c.CategoryName}");
            }
            if (int.TryParse(Console.ReadLine(), out int CategoryId))
            {
                Category category = db.Categories.FirstOrDefault(c => c.CategoryId == CategoryId);
                if (category != null)
                {
                    return category;
                }
            }
            logger.Error("Invalid Category Id");
            return null;
        }
        public static Product GetProduct(NorthwindConsole_32_KMBContext db)
        {

            // display all blogs
            var products = db.Products.OrderBy(b => b.ProductId);
            foreach (Product p in products)
            {
                Console.WriteLine($"{p.ProductId}: {p.ProductName}");
            }
            if (int.TryParse(Console.ReadLine(), out int ProductId))
            {
                Product product = db.Products.FirstOrDefault(p => p.ProductId == ProductId);
                if (product != null)
                {
                    return product;
                }
            }
            logger.Error("Invalid Category Id");
            return null;
        }

        public static Category InputCategory(NorthwindConsole_32_KMBContext db)
        {

            Category category = new Category();
            Console.WriteLine("Enter the Category name");
            category.CategoryName = Console.ReadLine();

            ValidationContext context = new ValidationContext(category, null, null);
            List<ValidationResult> results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(category, context, results, true);
            if (isValid)
            {
                // check for unique name
                if (db.Categories.Any(c => c.CategoryName == category.CategoryName))
                {
                    // generate validation error
                    isValid = false;
                    results.Add(new ValidationResult("Category already exists", new string[] { "CategoryName" }));
                }
                else
                {
                    logger.Info("Validation passed");
                }
            }
            if (!isValid)
            {
                foreach (var result in results)
                {
                    logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                }
                return null;
            }

            return category;
        }

        public static Product InputProduct(NorthwindConsole_32_KMBContext db)
        {

            Product product = new Product();
            Console.WriteLine("Enter the Product name");
            product.ProductName = Console.ReadLine();

            ValidationContext context = new ValidationContext(product, null, null);
            List<ValidationResult> results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, context, results, true);
            if (isValid)
            {
                // check for unique name
                if (db.Products.Any(c => c.ProductName == product.ProductName))
                {
                    // generate validation error
                    isValid = false;
                    results.Add(new ValidationResult("Product already exists", new string[] { "ProductName" }));
                }
                else
                {
                    logger.Info("Validation passed");
                }
            }
            if (!isValid)
            {
                foreach (var result in results)
                {
                    logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                }
                return null;
            }

            return product;
        }
    }

}