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
                Console.WriteLine("\n D- Save and Exit");

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
                        SaveBooksToFile();
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
                Console.WriteLine("\n D- Exit");

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
                        SaveBooksToFile();
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
            Console.WriteLine("Enter Book Name");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Book Author");
            string author = Console.ReadLine();

            Console.WriteLine("Enter Book ID");
            int ID;
            while (!int.TryParse(Console.ReadLine(), out ID)|| ID<0)
            {
                Console.WriteLine("Invalid ID.");
            }


            Console.WriteLine("Enter Book quntity");
            int q;
            while (!int.TryParse(Console.ReadLine(), out q) || q <= 0)
            {
                Console.Write("Quntity must be number and greater than zero .");
            }
            Books.Add((name, author, ID, q));
            Console.WriteLine("Book Added Succefully");

        }


        static void ViewAllBooks()
        {
            StringBuilder sb = new StringBuilder();

            int BookNumber = 0;

            for (int i = 0; i < Books.Count; i++)
            {
                BookNumber = i + 1;
                sb.Append("Book ").Append(BookNumber).Append(" name : ").Append(Books[i].BName);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append(" Author : ").Append(Books[i].BAuthor);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append(" ID : ").Append(Books[i].ID);
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append(" Quntity : ").Append(Books[i].q);
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
                            if (parts.Length == 3)
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

        static void BorrowBook() {

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
    } 
}

