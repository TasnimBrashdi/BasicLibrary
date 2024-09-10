using System;
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
        static List<(string BName, string BAuthor, int ID, int copies, int CopiesBorrow,float price,string categories)> Books = new List<(string BName, string BAuthor, int ID, int copies, int CopiesBorrow,float price, string categories)>();
        static List<(int AID,string Aname,string email, string pas)> Admin = new List<(int AID,string Aname,string email, string pas)>();
        static List<(int Idbook, int Iduser)> BorrowList = new List<(int Idbook, int Iduser)>();
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

        static int TotalBooks=0;
        static int nextIdUser = 1;
        static int nextIdAdmin = 1;
        // test check out
        static void Main(string[] args)
        {// downloaded form ahmed device 
            bool ExitFlag = false;
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
                        SearchForBook();
                        break;

                    case "B":
                        Console.WriteLine("\n List of book available");
                        ViewAllBooks();
                        BorrowBook(UID);
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
               int  BookId = i + 1;
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
                    Console.Write("copies greater than zero .");
                }
                Console.WriteLine($"Enter Book {BookId}| Categery: ");
                string cate = Console.ReadLine();

                Books.Add((name, author, BookId, q,0,price,cate));
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
                sb.Append("Book ").Append(BookNumber).Append("| Quntity : ").Append(Books[i].copies);
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
                            if (parts.Length == 7)
                            {
                                Books.Add((parts[0], parts[1], int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]), float.Parse(parts[5]), parts[6]));
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
                        writer.WriteLine($"{book.BName}|{book.BAuthor}|{book.ID}|{book.copies}");
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
                        Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, newq, Books[i].CopiesBorrow, Books[i].price, Books[i].categories);
                    
                        BorrowCount.Add((Books[i].BName, i + 1));

                     
                        BorrowList.Add((Books[i].ID, UID));
                        Filereport(filereport, usernam, (Books[i].BName, Books[i].BAuthor, Books[1].ID, newq),TotalBooks);

                      
                            string authorName = Books[i].BAuthor;

                            Console.WriteLine($"\nThis author {authorName} has other books:");
                            foreach (var book in Books)
                            {
                                if (book.BAuthor == authorName)
                                {
                                    Console.WriteLine($"Book's Title: {book.BName}, ID: {book.ID}");
                                }
                            }
                     
                        SaveMostBorrowing();
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
                        int newq = Books[i].copies + 1;
                        Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, newq, Books[i].CopiesBorrow, Books[i].price, Books[i].categories);
                        TotalBooks++;
                  
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
                    Console.Write(" A.Book's Name\n B.Author's Name\n C.copies\n D.Pirce \n E.Categories\n ");
                    string ChoiceEdit = Console.ReadLine()?.ToUpper();

                    switch (ChoiceEdit)
                    {
                        case "A":
                            Console.Write("Enter the new bOOK'S NAME: ");
                            string editname = Console.ReadLine();
                            Books[i] = (editname, Books[i].BAuthor, IdB, Books[i].copies, Books[i].CopiesBorrow, Books[i].price, Books[i].categories);
                            Console.Write(" bOOK'S NAME UPDATED\n ");

                            break;
                        case "B":
                            Console.Write("Enter the new AUTHOR'S NAME: ");
                            string editAuth = Console.ReadLine();
                            Books[i] = (Books[i].BName, editAuth, IdB, Books[i].copies, Books[i].CopiesBorrow, Books[i].price, Books[i].categories);
                            Console.Write(" AUTHOR'S NAME UPDATED\n ");
                            break;
                        case "C":
                            Console.Write("Add bOOK'S copies: ");
                            int newq = int.Parse(Console.ReadLine());
                            Books[i] = (Books[i].BName, Books[i].BAuthor, IdB, Books[i].copies + newq, Books[i].CopiesBorrow, Books[i].price, Books[i].categories);
                            Console.Write(" bOOK'S NAME UPDATED\n ");
                            break;
                            case "D":
                            Console.WriteLine("Enter new price of book");
                            string newcate = Console.ReadLine();
                            Books[i] = (Books[i].BName, Books[i].BAuthor, IdB, Books[i].copies, Books[i].CopiesBorrow, Books[i].price, newcate);
                            break;
                            case "E":
                            Console.WriteLine("Enter new category of book");
                            float newprice;
                      
                            while (!float.TryParse(Console.ReadLine(), out newprice) || newprice <= 0)
                            {
                                Console.Write("price must greater than zero .");
                            }
                            Books[i] = (Books[i].BName, Books[i].BAuthor, IdB, Books[i].copies, Books[i].CopiesBorrow, newprice, Books[i].categories);
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
                var book = Books.FirstOrDefault(b => b.ID == mostBorrowedBookId);
                if (book != default)
                {
                    bestAuthor = book.BAuthor;
                }
            }

            //Track the number of times each author is associated with the most borrowed book
            var authorBorrowCounts = new Dictionary<string, int>();
            foreach (var borrow in BorrowList)
            {
                var book = Books.FirstOrDefault(b => b.ID == borrow.Idbook);
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
                var mostBorrowedBook = Books.FirstOrDefault(b => b.ID == mostBorrowedBookId);
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
        static void SaveMostBorrowing()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileborrow))
                {
                    foreach (var book in BorrowList)
                    {
                        writer.WriteLine($"{book.Idbook}|{book.Iduser}");
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
    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
            }
        }



    }
}
    


