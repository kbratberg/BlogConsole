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
                        var query = db.Blogs.OrderBy(b => b.BlogId);

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
                        if (name == "")
                        {
                            logger.Info("Name cannot be null");
                        }
                        else
                        {
                            var blog = new Blog { Name = name };

                            var db = new BloggingContext();
                            db.AddBlog(blog);
                            logger.Info("Blog added - {name}", name);
                        }
                    }
                    else if (choice == "3")
                    {
                        // create blog post and save to db
                        var db = new BloggingContext();
                        var query = db.Blogs.OrderBy(b => b.BlogId);
                        Boolean postAdded = false;
                        Console.WriteLine("Pick a Blog to add a post");

                        foreach (var item in query)
                        {
                            Console.WriteLine($"{item.BlogId}) {item.Name}");

                        }
                        Int32 intPostChoice;
                        String postChoice = Console.ReadLine();
                        Boolean success = Int32.TryParse(postChoice, out intPostChoice);
                        if (success)
                        {
                            logger.Info($"You entered {intPostChoice}");
                        }
                        else if (postChoice == "")
                        {
                            logger.Info("Choice cannot be null");
                        }
                        
                        foreach (var item in query)
                        {
                            if (intPostChoice == item.BlogId)
                            {
                               postAdded = true;
                            }

                        }
                        if(postAdded == true){
                             Console.WriteLine("Enter Post Title:");
                               String postTitle = Console.ReadLine();

                                if (postTitle == "")
                                {
                                    logger.Info("Title cannot be Null");
                                }
                                Console.WriteLine("Enter Post Content");
                               String postContent = Console.ReadLine();

                                if (postContent == "")
                                {
                                    logger.Info("Content cannot be null");
                                }
                                var post = new Post { Title = postTitle, Content = postContent, BlogId = intPostChoice };
                                db.AddPost(post);
                                logger.Info($"Post entered Title: {postTitle}, Content: {postContent}");

  
                        }
                    }

                    else if (choice == "4")
                    {
                        var db = new BloggingContext();
                        var query = db.Blogs.OrderBy(b => b.BlogId);
                        Int64 totalPosts = 0;

                        Console.WriteLine("Enter which Blogs you would like to Display");
                        Console.WriteLine("0) Display all blogs");
                        foreach (var blog in query)
                        {
                            Console.WriteLine($"{blog.BlogId}) Display all posts in {blog.Name}");
                        }
                        Int64 blogToDisplay;
                        String displayChoice = Console.ReadLine();
                        Boolean success = Int64.TryParse(displayChoice, out blogToDisplay);

                        if (success)
                        {
                            if (blogToDisplay == 0)
                            {
                                foreach (var blog in query)
                                {
                                    var postQuery = db.Posts.Where(p => p.BlogId == blog.BlogId).OrderBy(p => p.Title);
                                    if (postQuery.Count() != 0){
                                    Console.WriteLine($"Category: {blog.Name}");
                                    foreach (var post in postQuery)
                                    {
                                        Console.WriteLine($"Title:\n{post.Title}\nContent:\n{post.Content}");
                                    }
                                    totalPosts += postQuery.Count();
                                    }
                                }
                                logger.Info($"There were {totalPosts} Posts to display");
                            }else{
                                Boolean isBlog = false;
                            foreach (var blog in query)
                            {
                                if (blogToDisplay == blog.BlogId)
                                {
                                    Console.WriteLine($"{blog.Name}");
                                    var postToDisplayQuery = db.Posts.Where(p => p.BlogId == blogToDisplay);
                                    logger.Info($"There are {postToDisplayQuery.Count()} posts to display");
                                    foreach (var post in postToDisplayQuery)
                                    {
                                        Console.WriteLine($"Title:\n{post.Title}\nContent:\n{post.Content}");
                                    }
                                    isBlog = true;
                                }
                                
                            }
                            if (isBlog == false){
                                logger.Info("Enter a valid blog entry");
                            }
                            }
                        }
                        else
                        {
                            logger.Info("Please enter a valid number");
                        }

                    }

                    else if (choice == "q")
                    {
                        logger.Info("Program Ended");
                    }
                    else
                    {
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