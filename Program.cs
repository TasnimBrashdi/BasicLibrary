﻿using System.Text;

namespace BasicLibrary
{
    internal class Program
    {
        static List<(string BName, string BAuthor, int ID)> Books = new List<(string BName, string BAuthor, int ID)>();    

        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Welcome to Lirary");
                Console.WriteLine("\n Enter the char of operation you need :");
                Console.WriteLine("\n A- Add New Book");
                Console.WriteLine("\n B- Display All Books");
                Console.WriteLine("\n C- Search for Book by Name");
                Console.WriteLine("\n D- Save and Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "A":
                        AddnNewBook();
                        break;

                    case "B":
                        ViewAllBooks();
                        break;

                    case "C":
                        // SearchForBook();
                        break;

                    case "D":
                        //SaveToFile();
                        break;

                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;



                }

                Console.WriteLine("press any key to continue");
                string cont = Console.ReadLine();

                Console.Clear();

            } while (true);
        }
        static void AddnNewBook() 
        { 
                 Console.WriteLine("Enter Book Name");
                 string name = Console.ReadLine();   

                 Console.WriteLine("Enter Book Author");
                 string author= Console.ReadLine();  

                 Console.WriteLine("Enter Book ID");
                 int ID = int.Parse(Console.ReadLine());

                  Books.Add(  ( name, author, ID )  );
                  Console.WriteLine("Book Added Succefully");

        }

        static void ViewAllBooks() 
        {
            int BookNumber;

          for(int i = 0; i<Books.Count;i++)
            {
                BookNumber = i + 1;
                Console.WriteLine( "Book "+ BookNumber + " : name "+Books[i].BName);
                Console.WriteLine("Book " + BookNumber + " : Author " + Books[i].BAuthor);
                Console.WriteLine("Book " + BookNumber + " : ID " + Books[i].ID+"\n\n");               

            }
        
        }
        //SearchForBook() { }
        //SaveToFile() { }
        //ReadFromFile() { }

    }
}
