namespace StorKoorespondencii
{
    using StorKoorespondencii.Data;
    using StorKoorespondencii.DataProcessing;
    using System;


    public class StartUp
    {
        public static void Main()
        {

            var context = new StoreDbContext();

            string command = "n";
            

            while (command.ToLower() == "n") 
            {
                
                Console.WriteLine("Меню");
                Console.WriteLine("1. Показване на всички кореспондеции");
                Console.WriteLine("2. Показване на кореспондеции за конкретен потребител");
                Console.WriteLine("3. Показване на кореспондеции за конкретени потребител по част от името на потребител");
                Console.WriteLine("4. Обновяване на правата на потребител по ИМЕ");
                Console.WriteLine("5. Обновяване на правата на на ВСИЧКИ потребители");

                int choice = 0;
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {

                    Console.WriteLine("Грешна команда!");
                }
                
                string result = "";
                if (choice == 1)
                {
                    result = Serializer.AllCorrespondeceTable(context);
                }

                if (choice == 2)
                {
                    Console.Write("Име на потребител: ");
                    string user = Console.ReadLine();

                    result = Serializer.AllCorrespondeceTable(context, user, true);
                }

                if (choice == 3)
                {
                    Console.Write("Име на потребител: ");
                    string user = Console.ReadLine();

                    result = Serializer.AllCorrespondeceTable(context, user, false);
                }

                if (choice == 4)
                {
                    Console.Write("Име на потребител: ");
                    string user = Console.ReadLine();

                    result = Serializer.UpdatePermUserName(context, user);
                }

                if (choice==5)
                {
                    Serializer.UpdatePermAllUsers(context);
                }

                Console.WriteLine(result=="" ? "no result" : result);
                Console.WriteLine("Exit Y/N");

                command = Console.ReadLine();

            } 
        }
    }
}
