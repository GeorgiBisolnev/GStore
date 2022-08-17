namespace StorKoorespondencii
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
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
                int choice = int.Parse(Console.ReadLine());
                string result = "";
                if (choice == 1)
                {
                    result = Serializer.AllCorrespondeceTable(context);
                }

                if (choice == 2)
                {
                    Console.WriteLine("Име на потребител: ");
                    string user = Console.ReadLine();

                    result = Serializer.AllCorrespondeceTable(context, user, true);
                }

                if (choice == 3)
                {
                    Console.WriteLine("Име на потребител: ");
                    string user = Console.ReadLine();

                    result = Serializer.AllCorrespondeceTable(context, user, false);
                }

                Console.WriteLine(result=="" ? "no result" : result);
                Console.WriteLine("Exit Y/N");

                command = Console.ReadLine();

            } 
        }
    }
}
