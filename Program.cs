﻿using System;
using System.Collections.Immutable;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace BasicLibrary
{
    internal class Program
    {
        static List<(string BName, string BAuthor, int ID, int q)> Books = new List<(string BName, string BAuthor, int ID, int q)>();
        static List<(string email, string pas, int ID)> Admin = new List<(string email, string pas, int ID)>();
        static List<(string nameb,int c)> BorrowCount = new List<(string nameb, int c)>();
        static List<(string username, string Uemail, string Upas, int UID)> User = new List<(string username, string Uemail, string Upas, int UID)>();
        static string filePath = "C:\\Users\\Codeline User\\Documents\\filelib\\lib.txt";
        static string filereport = "C:\\Users\\Codeline User\\Documents\\filelib\\report.txt";
        static string fileuser = "C:\\Users\\Codeline User\\Documents\\filelib\\user.txt";
        static string fileadmin = "C:\\Users\\Codeline User\\Documents\\filelib\\admin.txt";
        static string fileborrow = "C:\\Users\\Codeline User\\Documents\\filelib\\borrow.txt";
        static string usernam;
        static string nameReturn;
        static string nameBorrow;
        //static int BorrowCount = 0;
        static int TotalBooks=0;
        static int nextIdUser = 1;
        static int nextIdAdmin = 1;
        // test check out
        static void Main(string[] args)
        {// downloaded form ahmed device 
            bool ExitFlag = false;
            LoadBooksFromFile();
            TotalBook();
            try
            {
                do
                {
                    Console.WriteLine("\n    ---- Welcome to Lirary ----  ");
                    Console.WriteLine("\n Choose: \n A for Admin \n B for User \n C New Register ");


                    string choice = Console.ReadLine()?.ToUpper();

                    switch (choice)
                    {
                        case "A":

                            LogAdmin();
                            break;

                        case "B":
                            LogUser();

                            break;
                        case "C":

                            RegisterUser();

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
            catch (Exception ex)
            {
                Console.WriteLine("ERROR" + ex.Message);
            }
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

                Books.Add((name, author, i + 1, q));
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
                using (StreamWriter writer = new StreamWriter(filePath))
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

        static void BorrowBook()
        {

            Console.WriteLine("Enter Book Name you want to borrow");
            nameBorrow = Console.ReadLine();
            bool flag = false;
            try
            {
                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].BName == nameBorrow)
                    {   
                        Console.WriteLine("Book is available for borrowing ");
                        int newq = Books[i].q - 1;
                        Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, newq);
                    
                        BorrowCount.Add((Books[i].BName, i + 1));

                        TotalBooks--;
                        Filereport(filereport, usernam, (Books[i].BName, Books[i].BAuthor, Books[1].ID, newq),TotalBooks);

                        SaveMostBorrowing();
                            string authorName = Books[i].BAuthor;

                            Console.WriteLine($"\nThis author {authorName} has other books:");
                            foreach (var book in Books)
                            {
                                if (book.BAuthor == authorName)
                                {
                                    Console.WriteLine($"Book's Title: {book.BName}, ID: {book.ID}");
                                }
                            }
                        MostBookBorrowed();




                        flag = true;
                        break;
                    }
                }

                if (flag != true)
                { Console.WriteLine("book not found"); }
            }
            catch (Exception ex)
            {

                Console.WriteLine("ERROR" + ex.Message);

            }

        }
        static void ReturnBook()
        {
            Console.WriteLine("Enter Book Name you want to return");
            nameReturn = Console.ReadLine();
            bool flag = false;
            try
            {
                for (int i = 0; i < Books.Count; i++)
                {
                    if (nameBorrow == nameReturn)
                    {
                        Console.WriteLine("Book has been retrieved ");
                        int newq = Books[i].q + 1;
                        Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, newq);
                        TotalBooks++;
                        BorrowCount.Add((nameReturn, i-1));
                        returnbookfile(filereport, usernam, (nameReturn, Books[i].BAuthor, Books[i].ID, newq),TotalBooks);
                       
                        flag = true;
                        break;
                    }

                }
                if (flag != true)
                { Console.WriteLine(" This book not availabe or that has not been  borrowed , mybe you Enter wrong name"); }
            }
            catch (Exception ex)
            {

                Console.WriteLine("ERROR" + ex.Message);

            }
           
        }
        static void EditBook()
        {
            ViewAllBooks();
            Console.Write("Enter the ID Book  to edit: ");
            int IdB = int.Parse(Console.ReadLine());
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
                            Books[i] = (Books[i].BName, editAuth, IdB, Books[i].q);
                            Console.Write(" AUTHOR'S NAME UPDATED\n ");
                            break;
                        case 3:
                            Console.Write("Enter the new bOOK'S Quntity: ");
                            int newq = int.Parse(Console.ReadLine());
                            Books[i] = (Books[i].BName, Books[i].BAuthor, IdB, newq);
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

        static void Filereport(string filereport, string usernam, (string BName, string BAuthor, int ID, int q) Books,int totalbooks)
        {


            StringBuilder Brep = new StringBuilder();
            Brep.Append("\n_________Book's Borrow:________");
            Brep.AppendLine();
            Brep.Append("-----------------------------------------");
            Brep.AppendLine();
            Brep.Append("Book's Name: ").Append(Books.BName);
            Brep.AppendLine();
            Brep.Append("Authoer's Name: ").Append(Books.BAuthor);
            Brep.AppendLine();
            Brep.Append("Book's Id: ").Append(Books.ID);
            Brep.AppendLine();
            Brep.Append("User Name: ").Append(usernam);
            Brep.AppendLine();

            Brep.Append("Number of Books available: ").Append(Books.q);
            Brep.AppendLine();

            Brep.Append("Total Books: ").Append(totalbooks);
            Brep.AppendLine();
            using (StreamWriter report = new StreamWriter(filereport, true))
            {



                string Br = Books.ToString();

                report.WriteLine(Brep.ToString());
                report.WriteLine("Date and Time:");

                report.WriteLine(DateTime.Now);
            }


        }
        static void returnbookfile(string filereport, string usernam, (string BName, string BAuthor, int ID, int q) Books, int totalbooks)
        {
            StringBuilder Bretun = new StringBuilder();
            Bretun.Append("\n_______Book's Return:________");
            Bretun.AppendLine();
            Bretun.Append("-----------------------------------------");
            Bretun.AppendLine();
            Bretun.Append("Book's Name: ").Append(Books.BName);
            Bretun.AppendLine();
            Bretun.Append("\nAuthoer's Name: ").Append(Books.BAuthor);
            Bretun.AppendLine();
            Bretun.Append("\nBook's Id: ").Append(Books.ID);
            Bretun.AppendLine();
            Bretun.Append("\n User Name: ").Append(usernam);
            Bretun.AppendLine();

            Bretun.Append("Number of Books available: ").Append(Books.q);
            Bretun.AppendLine();
            Bretun.Append("Total Books: ").Append(totalbooks);
            Bretun.AppendLine();

            using (StreamWriter report = new StreamWriter(filereport, true))
            {



                string Bret = Books.ToString();

                report.WriteLine(Bretun.ToString());
                report.WriteLine("Date and Time:");

                report.WriteLine(DateTime.Now);
            }

        }

        static bool IsValidEmail(string email)
        {

            return !string.IsNullOrWhiteSpace(email) && email.Contains("@") && email.EndsWith(".com");
        }
        static void RegisterUser()
        {
            Console.WriteLine("Enter User Name");
            string uname = Console.ReadLine();
            Console.WriteLine("Enter your Email");
            string email = Console.ReadLine();
            if (!IsValidEmail(email))
            {
                Console.WriteLine("Invalid email, Please enter a valid email address.");
                return;
            }

            Console.WriteLine("Enter your Password");
            string pas = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(uname) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pas))
            {
                Console.WriteLine("User name,Email and Password cannot be empty.");
                return;
            }
            User.Add((uname, email, pas, nextIdUser++));
            Console.WriteLine("User Added Succefull\n");
            SaveUserToFile();
        }
        static void SaveAdminToFile()

        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileadmin))
                {
                    foreach (var a in Admin)
                    {
                        writer.WriteLine($"{a.email}|{a.pas}|{a.ID}");
                    }
                }
                Console.WriteLine("Admin saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
        static void SaveUserToFile()

        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileuser))
                {
                    foreach (var u in User)
                    {
                        writer.WriteLine($"{u.username}|{u.Uemail}|{u.Upas}|{u.UID}");
                    }
                }
                Console.WriteLine("User saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
        static void LoadUser()
        {

            try
            {
                if (File.Exists(fileuser))
                {
                    using (StreamReader reader = new StreamReader(fileuser))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 4)
                            {
                                User.Add((parts[0], parts[1], parts[2], int.Parse(parts[3])));
                            }
                        }
                    }
                    Console.WriteLine("User loaded from file successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }
        static void LoadAdmin()
        {

            try
            {
                if (File.Exists(fileadmin))
                {
                    using (StreamReader reader = new StreamReader(fileadmin))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 3)
                            {
                                Admin.Add((parts[0], parts[1], int.Parse(parts[2])));
                            }
                        }
                    }
                    Console.WriteLine("Admin loaded from file successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }
        static void LogUser()
        {
            LoadUser();
            Console.WriteLine("Enter User Name");
            usernam = Console.ReadLine();
            Console.WriteLine("Enter your Password");
            string pas = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(usernam) || string.IsNullOrWhiteSpace(pas))
            {
                Console.WriteLine("Email and Password cannot be empty.");
                return;
            }
            bool flag = false;
            try
            {
                for (int i = 0; i < User.Count; i++)
                {
                    if (User[i].username == usernam && User[i].Upas == pas)
                    {
                        Console.WriteLine($"           WELCOME: {usernam} ");
                        flag = true;
                        userMenu();
                        break;
                    }
                }

                if (flag != true)
                { Console.WriteLine("User not found Or input invaild"); }
            }

            catch (Exception e)
            {
                Console.WriteLine("Erorr" + e.Message);

            }



        }
        static void LogAdmin()
        {
            LoadAdmin();
            Console.WriteLine("Enter your Email");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your Password");
            string pas = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pas))
            {
                Console.WriteLine("Email and Password cannot be empty.");
                return;
            }
            bool flag = false;
            try
            {
                for (int i = 0; i < Admin.Count; i++)
                {
                    if (Admin[i].email == email && Admin[i].pas == pas)
                    {
                        Console.WriteLine("_______________WELCOME_______________");
                        flag = true;
                        adminMenu();
                        break;
                    }
                }

                if (flag != true)
                { Console.WriteLine("Admin not found Or input invaild"); }
            }

            catch (Exception e)
            {
                Console.WriteLine("Erorr" + e.Message);

            }




        }
        static void TotalBook()
        {
            TotalBooks = 0;
            for (int i = 0; i < Books.Count; i++)
            {
                TotalBooks += Books[i].q;
            }
        }
        static void MostBookBorrowed()
        {
            List<int> bookIdcount = new List<int>();
            List<int> borrowCount = new List<int>();
       

            foreach (var b in BorrowCount)
            {
                int bookId = b.c;
                int index = bookIdcount.IndexOf(bookId);
                if (index == -1)
                {

                    bookIdcount.Add(bookId);
                    borrowCount.Add(1);
                }
                else
                    {

                        borrowCount[index]++;
                }
            }
        
            int most= -1;
            int max = 0;
            for (int i = 0; i < borrowCount.Count; i++)
            {
                if (borrowCount[i] > max)
                {
                    most = bookIdcount[i];
                    max = borrowCount[i];
                }
            }
            string BookName=null;
            foreach (var book in Books)
            {
                if (book.ID == most)
                    BookName = book.BName;
    
            }
            Console.WriteLine($"Most Borrowed Book ID :{most}");




        }
        static void SaveMostBorrowing()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileborrow))
                {
                    foreach (var book in BorrowCount)
                    {
                        writer.WriteLine($"{book.nameb}|{book.c}");
                    }
                }
                Console.WriteLine("Books borrowed saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }
        static void loadBorrow()
        {
            try
            {
                if (File.Exists(fileborrow))
                {
                    using (StreamReader reader = new StreamReader(fileborrow))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 2)
                            {
                                BorrowCount.Add((parts[0],int.Parse(parts[1])));
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



    }
}
    


