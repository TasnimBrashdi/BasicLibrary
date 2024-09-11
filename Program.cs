using System;
using System.Collections.Immutable;
using System.IO;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace BasicLibrary
{
    internal class Program
    {
        static List<(int BID, string BName, string BAuthor, int copies, int CopiesBorrow,float price,string categories,int BorrowPeriod)> Books = new List<(int BID, string BName, string BAuthor,int copies, int CopiesBorrow,float price, string categories,int BorrowPeriod)>();
        static List<(int AID,string Aname,string email, string pas)> Admin = new List<(int AID,string Aname,string email, string pas)>();
        static List<(int Idbook, int Iduser,DateTime BorrowDate,DateTime ReturnDate, DateTime? ActualReturnDate,int? Rating,bool ISReturned)> BorrowList = new List<(int Idbook, int Iduser, DateTime BorrowDate, DateTime ReturnDate, DateTime? ActualReturnDate, int? Rating, bool ISReturned)>();

        static List<(string username, string Uemail, string Upas, int UID)> User = new List<(string username, string Uemail, string Upas, int UID)>();
        static List<(int CID,string CName, int NOFBooks)> Categories = new List<(int CID,string CName, int NOFBooks)>();
        static string BooksFile = "C:\\Users\\Codeline User\\Documents\\filelib\\lib.txt";
        static string filereport = "C:\\Users\\Codeline User\\Documents\\filelib\\report.txt";
        static string UsersFile = "C:\\Users\\Codeline User\\Documents\\filelib\\user.txt";
        static string AdminsFile = "C:\\Users\\Codeline User\\Documents\\filelib\\admin.txt";
        static string BorrowingFile = "C:\\Users\\Codeline User\\Documents\\filelib\\borrow.txt";
        static string CategoriesFile = "C:\\Users\\Codeline User\\Documents\\filelib\\Categories.txt";
        static string usernam;
        static string nameReturn;
        static string nameBorrow;

        static int TotalBooks=0;
        static int nextIdUser = 1;
        static int nextIdAdmin = 1;
        // test check out
        static void Main(string[] args)
        {// downloaded form ahmed device 
            bool ExitFlag = false;
            LoadAdmin();
            LoadUser();
            LoadBooksFromFile();
            loadBorrow();
            TotalBook();
            try
            {
                do
                {
                    Console.WriteLine("\n   - - - - Welcome to Library - - - -  ");
                    Console.WriteLine("\n Choose: \n A for Admin \n B for User \n C New Register \n D Log out ");


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
                            case "D":
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
                        ViewAllBooks();
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
        static void userMenu(int UID)
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
                        ViewAllBooksuser();
                        SearchForBook();
                        break;

                    case "B":
                        Console.WriteLine("\n List of book available");
                        ViewAllBooksuser();
                        BorrowBook(UID);
                        break;

                    case "C":
                        ReturnBook(UID);
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
                int  BookId = i + 1;
                int Idcate = i + 1;
                Console.WriteLine($"Enter Book {BookId}| Name: ");
                string name = Console.ReadLine();

                Console.WriteLine($"Enter Book {BookId}| Author: ");
                string author = Console.ReadLine();


                Console.WriteLine($"Enter Book {BookId}| copies:");
                int q;
                while (!int.TryParse(Console.ReadLine(), out q) || q <= 0)
                {
                    Console.Write("copies must be number and greater than zero .");
                }
                Console.WriteLine($"Enter Book {BookId}| price:");
                float price;
                while (!float.TryParse(Console.ReadLine(), out price) || price <= 0)
                {
                    Console.Write("price greater than zero .");
                }
                Console.WriteLine($"Enter Book {BookId}| Categery: ");
                string cate = Console.ReadLine();
                Console.WriteLine($"Enter Book {BookId}| BorrowPeriod:");
                int Borrowper;
                while (!int.TryParse(Console.ReadLine(), out Borrowper) || Borrowper <= 0)
                {
                    Console.Write("Borrow Period at least 1.");
                }
                int CopiesBorrow = 0;
                Books.Add((BookId, name, author,  q, CopiesBorrow, price, cate, Borrowper));
                //Categories.Add((Idcate, cate, Categories[i].NOFBooks));
                Console.WriteLine("Book Added Succefull\n");
            }
            SaveBooksToFile();
            ViewAllBooks();
        }


        static void ViewAllBooksuser()
        {
            StringBuilder sb = new StringBuilder();

            int BookNumber = 0;

            for (int i = 0; i < Books.Count; i++)
            {
                BookNumber = i + 1;
                sb.Append("Book ").Append(BookNumber).Append("| ID : ").Append(Books[i].BID);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append("| name : ").Append(Books[i].BName);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append("| Author : ").Append(Books[i].BAuthor);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append("| Price : ").Append(Books[i].price);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append("| Categary : ").Append(Books[i].categories);
                sb.AppendLine();
                sb.AppendLine().AppendLine();
                Console.WriteLine(sb.ToString());
                sb.Clear();

            }
        }
        static void ViewAllBooks()
        {
            StringBuilder sb = new StringBuilder();

            int BookNumber = 0;

            for (int i = 0; i < Books.Count; i++)
            {
                BookNumber = i + 1;
                sb.Append("Book ").Append(BookNumber).Append("| ID : ").Append(Books[i].BID);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append("| name : ").Append(Books[i].BName);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append("| Author : ").Append(Books[i].BAuthor);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append("| Copies : ").Append(Books[i].copies);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append("| Copies Borrow : ").Append(Books[i].CopiesBorrow);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append("| Price : ").Append(Books[i].price);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append("| Categary : ").Append(Books[i].categories);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append("| Borrow Period : ").Append(Books[i].BorrowPeriod);
   
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
                        Console.WriteLine("Book Author: " + Books[i].BAuthor+ " price: " + Books[i].price+ " categories: " + Books[i].categories);
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
                if (File.Exists(BooksFile))
                {
                    using (StreamReader reader = new StreamReader(BooksFile))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 8)
                            {
                                Books.Add((int.Parse(parts[0]), parts[1], parts[2],  int.Parse(parts[3]), int.Parse(parts[4]), float.Parse(parts[5]), parts[6], int.Parse(parts[7])));
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
                using (StreamWriter writer = new StreamWriter(BooksFile))
                {
                    foreach (var book in Books)
                    {
                        writer.WriteLine($"{book.BID}|{book.BName}|{book.BAuthor}|{book.copies}|{book.CopiesBorrow}|{book.price}|{book.categories}|{book.BorrowPeriod}");
                    }
                }
                Console.WriteLine("Books saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        static void BorrowBook(int UID)
        {

            Console.WriteLine("Enter Book Name you want to borrow");
            nameBorrow = Console.ReadLine();
            if(nameBorrow == null)
            {
                Console.WriteLine("Please Enter the name ");
            }
           
            bool flag = false;
            try
            {
                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].BName == nameBorrow)
                    {   
                        Console.WriteLine("Book is available for borrowing ");
                        int newq = Books[i].copies - 1;
                        TotalBooks--;
                        Books[i] = (Books[i].BID, Books[i].BName, Books[i].BAuthor, newq, Books[i].CopiesBorrow+1, Books[i].price, Books[i].categories, Books[i].BorrowPeriod);



          
                        DateTime BorrowDate=DateTime.Now;
                        DateTime returnDate = BorrowDate.AddDays(Books[i].BorrowPeriod);
                        DateTime? nullableDate = null;
                        BorrowList.Add((Books[i].BID, UID,DateTime.Now, BorrowDate, nullableDate, null,false));//
                        Filereport(filereport, usernam, (Books[i].BName, Books[i].BAuthor, Books[1].BID, newq),TotalBooks);

                      
                            string authorName = Books[i].BAuthor;

                            Console.WriteLine($"\nThis author {authorName} has other books:");
                            foreach (var book in Books)
                            {
                                if (book.BAuthor == authorName)
                                {
                                    Console.WriteLine($"Book's Title: {book.BName}, ID: {book.BID}");
                                }
                            }
                     
                        SaveBorrowing();
                        MostBorrowedBookAndBestAuthor();


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
        static void ReturnBook(int UID)
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
                        Console.WriteLine("Please Rating the book from 0 to 5 ");
                        int rat=int.Parse(Console.ReadLine());

                        int newq = Books[i].copies + 1;
                        Books[i] = (Books[i].BID, Books[i].BName, Books[i].BAuthor, newq, Books[i].CopiesBorrow-1, Books[i].price, Books[i].categories, Books[i].BorrowPeriod);
                        TotalBooks++;
                        BorrowList.Add((Books[i].BID, UID, BorrowList[i].BorrowDate,DateTime.Now, BorrowList[i].ActualReturnDate,rat, true));
                        returnbookfile(filereport, usernam, (nameReturn, Books[i].BAuthor, Books[i].BID, newq),TotalBooks);
                        SaveBorrowing();
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
                if (Books[i].BID == IdB)
                {
                    Console.Write(" A.Book's Name\n B.Author's Name\n C.copies\n D.Categories \n E.price \n F.Borrow Period \n");
                    string ChoiceEdit = Console.ReadLine()?.ToUpper();

                    switch (ChoiceEdit)
                    {
                        case "A":
                            Console.Write("Enter the new bOOK'S NAME: ");
                            string editname = Console.ReadLine();
                            Books[i] = (IdB, editname, Books[i].BAuthor,Books[i].copies, Books[i].CopiesBorrow, Books[i].price, Books[i].categories, Books[i].BorrowPeriod);
                            Console.Write(" bOOK'S NAME UPDATED\n ");

                            break;
                        case "B":
                            Console.Write("Enter the new AUTHOR'S NAME: ");
                            string editAuth = Console.ReadLine();
                            Books[i] = (IdB,Books[i].BName, editAuth,Books[i].copies, Books[i].CopiesBorrow, Books[i].price, Books[i].categories, Books[i].BorrowPeriod);
                            Console.Write(" AUTHOR'S NAME UPDATED\n ");
                            break;
                        case "C":
                            Console.Write("Add bOOK'S copies: ");
                            int newq = int.Parse(Console.ReadLine());
                            Books[i] = (IdB, Books[i].BName, Books[i].BAuthor,Books[i].copies + newq, Books[i].CopiesBorrow, Books[i].price, Books[i].categories, Books[i].BorrowPeriod);
                            Console.Write(" bOOK'S NAME UPDATED\n ");
                            break;
                            case "D":
                            Console.WriteLine("Enter new category of book");
                            string newcate = Console.ReadLine();
                            Books[i] = (IdB, Books[i].BName, Books[i].BAuthor,  Books[i].copies, Books[i].CopiesBorrow, Books[i].price, newcate, Books[i].BorrowPeriod);
                            Console.Write(" bOOK'S category UPDATED\n ");
                            break;
                            case "E":
                            Console.WriteLine("Enter new price of book");
                            float newprice;
                      
                            while (!float.TryParse(Console.ReadLine(), out newprice) || newprice <= 0)
                            {
                                Console.Write("price must greater than zero .");
                            }
                            Books[i] = (IdB, Books[i].BName, Books[i].BAuthor, Books[i].copies, Books[i].CopiesBorrow, newprice, Books[i].categories, Books[i].BorrowPeriod);
                            Console.Write(" bOOK'S price UPDATED\n ");
                            break;
                            case "F":
                            Console.Write("Edit bOOK'S Borrow Period: ");
                            int newp = int.Parse(Console.ReadLine());
                            Books[i] = (IdB, Books[i].BName, Books[i].BAuthor, Books[i].copies , Books[i].CopiesBorrow, Books[i].price, Books[i].categories, newp);
                            Console.Write(" bOOK'S Borrow Period UPDATED\n ");
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
            bool canRemove = true;
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].BID == IdB)
                {  
                    for(int j = 0; j < BorrowList.Count; j++) {
                        if (IdB == BorrowList[j].Idbook)
                        {
                            canRemove = false;
                            Console.Write("Bookcannot be removed because it is currently borrowed\n");
                            break;
                        }
                    }

                    if (canRemove)
                    {
                        Console.WriteLine("Are you sure? y/n.");
                        string input=Console.ReadLine();
                        switch (input)
                        {
                            case "y":
                                Books.RemoveAt(i);
                                Console.WriteLine("Book removed successfully.");
                                SaveBooksToFile();
                                break;
                            case "n":
                                Console.WriteLine("Book NOT removed.");
                                break;
                            default:
                                Console.WriteLine("Invaild input.");
                                break;
                        }
                       
                    }
                 

                    flag = true;
                    break;
                }
            }

            if (flag != true)
            { Console.WriteLine("book not found\n"); }
        }

        static void Filereport(string filereport, string usernam, (string BName, string BAuthor, int ID, int copies) Books,int totalbooks)
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

            Brep.Append("Number of Books available: ").Append(Books.copies);
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
        static bool IsValidPass(string pas)
        {
            string lengthPattern = @"^.{8,}$"; // At least 8 characters long
            string capitalLetterPattern = @"[A-Z]"; // At least one capital letter
            string symbolPattern = @"[\W_]"; // At least one symbol 

            bool isValidLength = Regex.IsMatch(pas, lengthPattern);
            bool hasCapitalLetter = Regex.IsMatch(pas, capitalLetterPattern);
            bool hasSymbol = Regex.IsMatch(pas, symbolPattern);

            return isValidLength && hasCapitalLetter && hasSymbol;
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
            if (!IsValidPass(pas)) {
                Console.WriteLine("Invalid Password,at last 8 character ,one sympol ,and one capital letter");
                return;
            }
            if (string.IsNullOrWhiteSpace(uname) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pas))
            {
                Console.WriteLine("User name,Email and Password cannot be empty.");
                return;
            }
            User.Add((uname, email, pas, nextIdUser++));
            Console.WriteLine("User Added Succefull\n");
            SaveUserToFile();
        }
      
        static void SaveUserToFile()

        {
            try
            {
                using (StreamWriter writer = new StreamWriter(UsersFile))
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
                if (File.Exists(UsersFile))
                {
                    using (StreamReader reader = new StreamReader(UsersFile))
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
                if (File.Exists(AdminsFile))
                {
                    using (StreamReader reader = new StreamReader(AdminsFile))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length ==4)
                            {
                                Admin.Add((int.Parse(parts[0]),parts[1], parts[2], parts[3]));
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
                        userMenu(User[i].UID);
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
                TotalBooks += Books[i].copies;
            }
        }
        static void MostBorrowedBookAndBestAuthor()
        {
            // Count the number of times each book is borrowed
            var borrowCounts = new Dictionary<int, int>();
            foreach (var borrow in BorrowList)
            {  
                if (borrowCounts.ContainsKey(borrow.Idbook))
                    borrowCounts[borrow.Idbook]++;
                else
                    borrowCounts[borrow.Idbook] = 1;
            }

            // Find the most borrowed book
            int mostBorrowedBookId = -1;
            int maxBorrowCount = 0;
            foreach (var kvp in borrowCounts)
            {
                if (kvp.Value > maxBorrowCount)
                {
                    mostBorrowedBookId = kvp.Key;
                    maxBorrowCount = kvp.Value;
                }
            }

            //Find the author of the most borrowed book
            string bestAuthor = null;
            if (mostBorrowedBookId != -1)
            {
                var book = Books.FirstOrDefault(b => b.BID == mostBorrowedBookId);
                if (book != default)
                {
                    bestAuthor = book.BAuthor;
                }
            }

            //Track the number of times each author is associated with the most borrowed book
            var authorBorrowCounts = new Dictionary<string, int>();
            foreach (var borrow in BorrowList)
            {
                var book = Books.FirstOrDefault(b => b.BID == borrow.Idbook);
                if (book != default)
                {
                    if (authorBorrowCounts.ContainsKey(book.BAuthor))
                        authorBorrowCounts[book.BAuthor]++;
                    else
                        authorBorrowCounts[book.BAuthor] = 1;
                }
            }

            // Find the best author based on the borrow counts
            string bestAuthorOverall = null;
            int maxAuthorBorrowCount = 0;
            foreach (var kvp in authorBorrowCounts)
            {
                if (kvp.Value > maxAuthorBorrowCount)
                {
                    bestAuthorOverall = kvp.Key;
                    maxAuthorBorrowCount = kvp.Value;
                }
            }

            //  results
            if (mostBorrowedBookId != -1)
            {
                var mostBorrowedBook = Books.FirstOrDefault(b => b.BID == mostBorrowedBookId);
                if (mostBorrowedBook != default)
                {
                    Console.WriteLine($"\nMost Borrowed Book ID: {mostBorrowedBookId}, Name: {mostBorrowedBook.BName}");
                }
            }
            else
            {
                Console.WriteLine("No books have been borrowed.");
            }

            if (bestAuthorOverall != null)
            {
                Console.WriteLine($"Best Author: {bestAuthorOverall}");
            }
            else
            {
                Console.WriteLine("No author data available.");
            }
        }
        static void SaveBorrowing()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(BorrowingFile))
                {
                    foreach (var book in BorrowList)
                    {
                        writer.WriteLine($"{book.Idbook}|{book.Iduser}|{book.BorrowDate}|{book.ReturnDate}|{book.ActualReturnDate}|{book.Rating}|{book.ISReturned}");
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
                if (File.Exists(BorrowingFile))
                {
                    using (StreamReader reader = new StreamReader(BorrowingFile))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split('|');
                            if (parts.Length == 7)
                            {
                                DateTime? actualReturnDate = parts[4].Equals("N/A", StringComparison.OrdinalIgnoreCase) ? (DateTime?)null : DateTime.Parse(parts[4]);
                            
                                int? rating = parts[5].Equals("N/A", StringComparison.OrdinalIgnoreCase) ? (int?)null : int.Parse(parts[5]);
                                BorrowList.Add((int.Parse(parts[0]),int.Parse(parts[1]), DateTime.Parse(parts[2]), DateTime.Parse(parts[3]), actualReturnDate, rating, bool.Parse(parts[6])));
                                
                            }
                        }
                    }
    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }



    }
}
    


