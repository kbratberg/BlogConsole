using System;
using NLog.Web;
using System.IO;
using System.Linq;


namespace BlogsConsole
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
                String choice;

                do
                {
                    Console.WriteLine("Select the number of a option:");
                    Console.WriteLine("1) Display all Blogs");
                    Console.WriteLine("2) Add Blog");
                    Console.WriteLine("3) Create Blog Post");
                    Console.WriteLine("4) Display all Blog Posts");
                    Console.WriteLine("Enter q to exit");

                    choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        // Display all Blogs from the database

                        var db = new BloggingContext();
                        var query = db.Blogs.OrderBy(b => b.Name);

                        logger.Info($"There were {query.Count()} blogs returned");

                        Console.WriteLine("All blogs in the database:");
                        foreach (var item in query)
                        {
                            Console.WriteLine(item.Name);
                        }

                    }

                   else if (choice == "2")
                    {
                        // Create and save a new Blog
                        Console.Write("Enter a name for a new Blog: ");
                        var name = Console.ReadLine();
                        if(name == ""){
                            logger.Info("Name cannot be null");
                        }else{
                        var blog = new Blog { Name = name };
                        
                        var db = new BloggingContext();
                        db.AddBlog(blog);
                        logger.Info("Blog added - {name}", name);
                        }
                    }
                   else if(choice == "3"){
                        // create blog post and save to db
                        var db = new BloggingContext();
                        var query = db.Blogs.OrderBy(b => b.Name);

                        Console.WriteLine("Pick a Blog to add a post");
                        
                        foreach (var item in query)
                        {
                            Console.WriteLine($"{item.BlogId}) {item.Name}");
                            
                        }
                        Int64 intPostChoice;
                        String postChoice = Console.ReadLine();
                        Boolean success = Int64.TryParse(postChoice, out intPostChoice);
                        if(success){ 
                            logger.Info($"You entered {intPostChoice}");
                            }
                            else if (postChoice == ""){
                                logger.Info("Choice cannot be null");
                            }

                        foreach (var item in query){
                            if (intPostChoice == item.BlogId){
                                Console.WriteLine("Enter Post Title:");
                                String postTitle = Console.ReadLine();

                                if (postTitle == ""){
                                    logger.Info("Title cannot be Null");
                                }
                                Console.WriteLine("Enter Post Content");
                                String postContent = Console.ReadLine();

                                if (postContent == ""){
                                    logger.Info("Content cannot be null");
                                }

                                var post = new Post { Title = postTitle, Content = postContent, BlogId = item.BlogId };
                                db.AddPost(post);

                            }else{
                                logger.Info("Invalid Blog Id");
                            }
                        }

                    }

                   else if (choice == "4"){

                    }

                    else if(choice == "q"){
                        logger.Info("Program Ended");
                    }
                    else{
                        logger.Info("Invalid Choice");
                    }

                } while (choice == "1" || choice == "2" || choice == "3" || choice == "4");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }
    }
}