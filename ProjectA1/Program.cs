using System.Xml;

namespace ProjectA1
{
    internal class Program
    {
        struct Article
        {
            public string Name;
            public decimal Price;
        }

        const int _maxNrArticles = 10;
        const int _maxArticleNameLength = 20;
        const decimal _vat = 0.25M;

        static Article[] articles = new Article[_maxNrArticles];
        static uint nrArticles;

        static void Main(string[] args)
        {
            ReadArticles();
            PrintReciept();
        }

        private static void ReadArticles()
        {
            Console.WriteLine("Hello and welcome to Project A1");
            CoolLine();
            Console.WriteLine("Let's print a receipt!");
            
            //Here we make sure we get a positive int lower than 10:
            bool didItWork;
            do
            {
                Console.WriteLine("How many articles do you want to buy today? (max 10)");
                didItWork = uint.TryParse(Console.ReadLine(), out nrArticles);
                if (!didItWork)
                {
                    Console.WriteLine("----Wrong input, try again.----");
                }
                else if (nrArticles > 10)
                {
                    Console.WriteLine("You cannot but more than 10 articles");
                    didItWork = false;
                }
                else if (nrArticles == 0)
                {
                    Console.WriteLine("You cannot but 0 articles!");
                    didItWork = false;
                }
            }   
            while (!didItWork);
            Console.Clear();
            CoolLine();

            Console.WriteLine("Please enter the name of every article and its price.");
            Console.WriteLine("Use the format: name - price, like this:");
            Console.WriteLine("Kiwi - 5,85  (The decimal separator will depend on the\nregional settings of your operating system.)");
            CoolLine();

            // loop through every article asking for input, and checking format:
            for (int i = 0; i < nrArticles; i++)
            {
                string enteredString;
                string[] inputStrings = default;
                bool correctNumberFormat = default;
                bool correctNameFormat = default;
                decimal price = default;
                string name = default;
                do
                {
                    correctNameFormat = true;
                    Console.WriteLine($"Article n#{i+1}:");
                    enteredString = Console.ReadLine();

                    //Does the input contain - ?
                    if (enteredString.Contains(" - "))
                    {
                        inputStrings = enteredString.Split(" - ");
                    }
                    else
                    {
                        Console.WriteLine("----Wrong input format----\nUse the format: name - price, like this: ");
                        Console.WriteLine("Kiwi - 5,85");
                        continue;
                    }

                    //Is the number format correct?
                    correctNumberFormat = decimal.TryParse(inputStrings[1], out price);
                    if (!correctNumberFormat)
                    {
                        Console.WriteLine("----wrong number format----\nCheck that your price has the decimal separator\ncorresponding to your regional settings and is a valid number.\n(In Sweden we use \",\")");
                    }
                    else if (price < 0)
                    {
                        correctNumberFormat = false;
                        Console.WriteLine("The price cannot be a negative number!");
                    }

                    //Did the user input a name for the article, and is this name not too long?
                    name = inputStrings[0];
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Don't forget to enter a name for your article.");
                        correctNameFormat = false;
                    }
                    else if (name.Length > _maxArticleNameLength)
                    {
                        Console.WriteLine("Please use a name for your article with no more than 20 characters.");
                        correctNameFormat = false;
                    }

                }
                while (!enteredString.Contains(" - ") || !correctNumberFormat || !correctNameFormat);

                //Now that all is right, we create an instance of Article to save the data.
                articles[i].Name = name;
                articles[i].Price = price;
            }

            
        }
        private static void PrintReciept()
        {
            Console.Clear();
            Console.WriteLine("Press any key to print your reciept: ");
            Console.ReadKey(true);
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            CoolLine();
            Console.WriteLine($"{"Receipt Purchased Articles", -25}");
            Console.WriteLine($"Purchase date: {DateTime.Now}");
            Console.WriteLine($"Numer of items purchased: {nrArticles}\n");

            //Calculating the total purchase and printing out the reciept:
            decimal total = 0;
            Console.WriteLine($"{"#", -10}{"Name", -20}{"Price", -20}");
            for (int i = 0;i < nrArticles; i++)
            {
                total += articles[i].Price;
                Console.WriteLine($"{i + 1,-10}{articles[i].Name,-20}{articles[i].Price, -20:C}");
            }
            Console.WriteLine($"\n{"Total Purchase:", -30}{total, -20:C}");
            Console.WriteLine($"{"Includes VAT (25%):", -30}{total - total/ (_vat+1), -20:C}");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void CoolLine()
        {
            for (int i = 0; i < 50; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine("\n");
        }
    }
}