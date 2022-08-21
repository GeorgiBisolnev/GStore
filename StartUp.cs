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


            string command = "n";
            

            while (command.ToLower() == "n") 
            {
                var context = new StoreDbContext();

                Console.WriteLine("Меню");
                Console.WriteLine("1. Показване на всички кореспондеции");
                Console.WriteLine("2. Показване на кореспондеции за конкретен потребител");
                Console.WriteLine("3. Показване на кореспондеции за конкретени потребител по част от името на потребител");
                Console.WriteLine("4. Обновяване на правата на потребител по ИМЕ");
                Console.WriteLine("5. Обновяване на правата на на ВСИЧКИ потребители");
                Console.WriteLine("6. Добавяне на права на потребител");
                Console.WriteLine("7. Изтриване на всички права на потребител");
                Console.WriteLine("8. Добавяне на права за нов склад и документ на потребител само до разходни звена до които вече има достъп на конкретен Потребител");
                Console.WriteLine("9. Добавяне на права за нов склад и документ само до разходни звена до които вече има достъп за ВСИЧКИ потребители в департамент ОТДЕЛЕНИЕ");



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
                        result = Serializer.DeletePermUserName(context, username);                        
                    }
                        
                }

                if (choice==8)
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
                        Console.Write("Кт код: ");
                        string KtCode = Console.ReadLine();
                        Console.Write("Тип док: ");
                        string DokCode = Console.ReadLine();

                        if (!context.SCharger.Any(c => c.SCCode == KtCode))
                        {
                            result = $"Не съществува звено с код {KtCode}";
                        }
                        else if (!context.SDOType.Any(d => d.SDOTCode == DokCode))
                        {
                            result = $"Не съществува документ с код {DokCode}";
                        }
                        else
                        {
                            result = Serializer.AddKtDotToUserName(context, username, KtCode, DokCode);
                        }                       
                    }
                        
                }

                if (choice ==9)
                {
                    Console.Write("Кт код: ");
                    string KtCode = Console.ReadLine();
                    Console.Write("Тип док: ");
                    string DokCode = Console.ReadLine();

                    if (!context.SCharger.Any(c => c.SCCode == KtCode))
                    {
                        result = $"Не съществува звено с код {KtCode}";
                    } 
                    else if (!context.SDOType.Any(d => d.SDOTCode == DokCode))
                    {
                        result = $"Не съществува документ с код {DokCode}";
                    }
                    else
                    {
                        Serializer.AddKtDotToAllUsers(context, KtCode, DokCode);

                        result = $"Добавени Кт код {KtCode} и {DokCode} към всички потребители в департамент ОТДЕЛЕНИЕ";
                    }
                    
                }
                Console.WriteLine(result=="" ? "no result" : result);

                log.CreateMessage(result);

                Console.WriteLine("Exit Y/N");
                context.Dispose();
                command = Console.ReadLine();
            } 
        }
    }
}
