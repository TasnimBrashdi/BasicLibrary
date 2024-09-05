﻿using System;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;

namespace BasicLibrary
{
    internal class Program
    {
        static List<(string BName, string BAuthor, int ID, int q)> Books = new List<(string BName, string BAuthor, int ID, int q)>();
        static string filePath = "C:\\Users\\Codeline User\\Documents\\filelib\\lib.txt";

        static string nameReturn;
        static string nameBorrow;
        // test check out
        static void Main(string[] args)
        {// downloaded form ahmed device 
            bool ExitFlag = false;
            LoadBooksFromFile();
            do
            {
                Console.WriteLine("Welcome to Lirary");
                Console.WriteLine("\n Enter A for Admin ,B for User:");


                string choice = Console.ReadLine()?.ToUpper();

                switch (choice)
                {
                    case "A":
                        adminMenu();
                        break;

                    case "B":
                        userMenu();
                        break;

                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;



                }

                Console.WriteLine("press any key to continue");
                string cont = Console.ReadLine();

                Console.Clear();

            } while (ExitFlag != true);
        }
        static void adminMenu()
        {
            bool ExitFlag = false;

            do
            {

                Console.WriteLine("\n Enter the char of operation you need :");
                Console.WriteLine("\n A- Add New Book");
                Console.WriteLine("\n B- Display All Books");
                Console.WriteLine("\n C- Search Book");
                Console.WriteLine("\n D- Edit Book");
                Console.WriteLine("\n E- Remove Book");
                Console.WriteLine("\n F- log out");

                string choice = Console.ReadLine()?.ToUpper();

                switch (choice)
                {
                    case "A":
                        AddnNewBook();
                        break;

                    case "B":
                        ViewAllBooks();
                        break;

                    case "C":
                        SearchForBook();
                        break;
                    case "D":
                        EditBook();
                        break;
                    case "E":
                        RemoveBook();
                        break;

                    case "F":
                        Console.WriteLine("You have succeessfully logged out");
                        ExitFlag = true;
                        break;

                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;



                }

                Console.WriteLine("press any key to continue");
                string cont = Console.ReadLine();

                Console.Clear();
            } while (ExitFlag != true);
        }
        static void userMenu()
        {


            bool ExitFlag = false;
           

            do
            {
                Console.WriteLine("\n Enter the char of operation you need :");
                Console.WriteLine("\n A- Search Book");
                Console.WriteLine("\n B- Borrow Books");
                Console.WriteLine("\n C- return Book");
                Console.WriteLine("\n D- Log Out");

                string choice = Console.ReadLine()?.ToUpper(); 

                switch (choice)
                {
                    case "A":
                        SearchForBook();
                        break;

                    case "B":
                        Console.WriteLine("\n List of book available");
                        ViewAllBooks();
                        BorrowBook();
                        break;

                    case "C":
                        ReturnBook();
                        break;
                    case "D":
                        Console.WriteLine("You have succeessfully logged out");
                        ExitFlag = true;
                        break;
                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;




                }

                Console.WriteLine("press any key to continue");
                string cont = Console.ReadLine();

                Console.Clear();
            } while (ExitFlag != true);
        }

        static void AddnNewBook()
        {

            Console.WriteLine("Enter number of Books you want to add:");
            int Nbooks;
            while (!int.TryParse(Console.ReadLine(), out Nbooks) || Nbooks <= 0)
            {
                Console.Write("Invalid input.");
            }
            for (int i = 0; i < Nbooks; i++)
            {

                Console.WriteLine($"Enter Book {i + 1}| Name: ");
                string name = Console.ReadLine();

                Console.WriteLine($"Enter Book {i + 1}| Author: ");
                string author = Console.ReadLine();


                Console.WriteLine($"Enter Book {i + 1}| quntity:");
                int q;
                while (!int.TryParse(Console.ReadLine(), out q) || q <= 0)
                {
                    Console.Write("Quntity must be number and greater than zero .");
                }
           
                Books.Add((name, author, i+1, q));
                Console.WriteLine("Book Added Succefull\n");
            }
            SaveBooksToFile();
            ViewAllBooks();
        }


        static void ViewAllBooks()
        {
            StringBuilder sb = new StringBuilder();

            int BookNumber = 0;

            for (int i = 0; i < Books.Count; i++)
            {
                BookNumber = i + 1;
                sb.Append("Book ").Append(BookNumber).Append("| name : ").Append(Books[i].BName);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append("| Author : ").Append(Books[i].BAuthor);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append("| ID : ").Append(Books[i].ID);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append("| Quntity : ").Append(Books[i].q);
                sb.AppendLine().AppendLine();
                Console.WriteLine(sb.ToString());
                sb.Clear();

            }
        }

        static void SearchForBook()
        {

            Console.WriteLine("Enter the book name you want");
            string name = Console.ReadLine();
            bool flag = false;
            try
            {
                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].BName == name)
                    {
                        Console.WriteLine("Book Author is : " + Books[i].BAuthor);
                        flag = true;
                        break;
                    }
                }
           
            if (flag != true)
            { Console.WriteLine("book not found"); }
            }

            catch (Exception e)
            {
                Console.WriteLine("Erorr" + e.Message);

            }
        }

        static void LoadBooksFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 4)
                            {
                                Books.Add((parts[0], parts[1], int.Parse(parts[2]), int.Parse(parts[3])));
                            }
                        }
                    }
                    Console.WriteLine("Books loaded from file successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }

        static void SaveBooksToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath,true))
                {
                    foreach (var book in Books)
                    {
                        writer.WriteLine($"{book.BName}|{book.BAuthor}|{book.ID}|{book.q}");
                    }
                }
                Console.WriteLine("Books saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        static void BorrowBook()    {

            Console.WriteLine("Enter Book Name you want to borrow");
            nameBorrow = Console.ReadLine();
            bool flag = false;

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].BName == nameBorrow)
                {
                    Console.WriteLine("Book is available for borrowing ");
                    int newq = Books[i].q - 1;
                    Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, newq);
                    flag = true;
                    break;
                }
            }

            if (flag != true)
            { Console.WriteLine("book not found"); }
       

        }
        static void ReturnBook()
        {
            Console.WriteLine("Enter Book Name you want to return");
            nameReturn = Console.ReadLine();
            bool flag = false;

            for (int i = 0; i < Books.Count; i++)
            {
                if (nameBorrow == nameReturn)
                {
                    Console.WriteLine("Book has been retrieved ");
                    int newq = Books[i].q + 1;
                    Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, newq);
                    flag = true;
                    break;
                }
            }
            if (flag != true)
            { Console.WriteLine(" This book not availabe or that has not been  borrowed , mybe you Enter wrong name"); }
            

        }
        static void EditBook()
        {
            ViewAllBooks();
            Console.Write("Enter the ID Book  to edit: ");
            int IdB =int.Parse( Console.ReadLine());
            bool flag = false;

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == IdB)
                {
                    Console.Write(" 1.Book's Name\n 2.Author's Name\n 3.quntity\n ");
                    int ChoiceEdit;
                    while (!int.TryParse(Console.ReadLine(), out ChoiceEdit) || ChoiceEdit <= 0)
                    {
                        Console.Write("Invalid input.");
                    }
                    switch (ChoiceEdit)
                    {
                        case 1:
                            Console.Write("Enter the new bOOK'S NAME: ");
                            string editname = Console.ReadLine();
                            Books[i] = (editname, Books[i].BAuthor, IdB, Books[i].q);
                            Console.Write(" bOOK'S NAME UPDATED\n ");

                            break;
                        case 2:
                            Console.Write("Enter the new AUTHOR'S NAME: ");
                            string editAuth = Console.ReadLine();
                            Books[i] = (Books[i].BName,editAuth, IdB, Books[i].q);
                            Console.Write(" AUTHOR'S NAME UPDATED\n ");
                            break;
                        case 3:
                            Console.Write("Enter the new bOOK'S Quntity: ");
                           int newq =int.Parse( Console.ReadLine());
                            Books[i] = (Books[i].BName, Books[i].BAuthor, IdB,newq);
                            Console.Write(" bOOK'S NAME UPDATED\n ");
                            break;
                        default:
                            Console.WriteLine("Invalid input");
                            break;
                    }
                    SaveBooksToFile();

                    flag = true;
                    break;
                }
            }

            if (flag != true)
            { Console.WriteLine("book not found\n"); }
          
            ViewAllBooks();

        }
        static void RemoveBook()
        {
            ViewAllBooks();
            Console.Write("Enter the ID Book  to remove: ");
            int IdB = int.Parse(Console.ReadLine());
            bool flag = false;

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == IdB)
                {
                    Books.RemoveAt(i);  
                    Console.Write("Book removed succeessfully\n");
                   
                    SaveBooksToFile();

                    flag = true;
                    break;
                }
            }

            if (flag != true)
            { Console.WriteLine("book not found\n"); }
        }
    }
    }
    


