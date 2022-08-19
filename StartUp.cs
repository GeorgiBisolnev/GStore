namespace StorKoorespondencii
{
    using StorKoorespondencii.Data;
    using StorKoorespondencii.DataProcessing;
    using System;


    public class StartUp
    {
        public static void Main()
        {
            SlackMessageLog log = new SlackMessageLog();
            log.CreateMessage("Hello there...");
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
                Console.WriteLine("6. Добавяне на права на потребител");
                Console.WriteLine("7. Изтриване на всички права на потребител");


                int choice = 0;
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {

                    Console.WriteLine("Грешна команда!");
                    log.CreateMessage(e.Message.ToString());
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

                    result = Serializer.AllCorrespondeceTable(context, user, false);
                }

                if (choice == 3)
                {
                    Console.Write("Име на потребител: ");
                    string user = Console.ReadLine();

                    result = Serializer.AllCorrespondeceTable(context, user, true);
                }

                if (choice == 4)
                {
                    Console.Write("Име на потребител: ");
                    string user = Console.ReadLine();

                    result = Serializer.UpdatePermUserName(context, user);
                }

                if (choice==5)
                {
                    result = Serializer.UpdatePermAllUsers(context);                    
                }

                if (choice==6)
                {
                    Console.Write("Име на потребител: ");
                    string username = Console.ReadLine();

                    var userId = context.Login.Where(u => u.SUName == username).Select(u => u.SUid).FirstOrDefault();


                    if (userId == 0)
                    {
                        result = "Не може да се добавят права на несъществуващ потребител";                        
                    }
                    else
                    {
                        
                        Console.Write("Дт код: ");
                        string DtCode = Console.ReadLine();
                        Console.Write("Кт код: ");
                        string KtCode = Console.ReadLine();
                        Console.Write("Тип док: ");
                        string DokCode = Console.ReadLine();

                        result = Serializer.AddCorrespondenceToUser(context, userId, KtCode, DtCode, DokCode);
                    }

                }

                if (choice==7)
                {
                    Console.Write("Име на потребител: ");
                    string username = Console.ReadLine();

                    var userId = context.Login.Where(u => u.SUName == username).Select(u => u.SUid).FirstOrDefault();


                    if (userId == 0)
                    {
                        result = "Не може да се добавят права на несъществуващ потребител";
                    }
                    else
                    {
                        StoreDbContext contextDelete = new StoreDbContext();
                        result = Serializer.DeletePermUserName(contextDelete, username);
                    }
                        
                }

                Console.WriteLine(result=="" ? "no result" : result);

                log.CreateMessage(result);

                Console.WriteLine("Exit Y/N");

                command = Console.ReadLine();

            } 
        }
    }
}
