using Newtonsoft.Json;
using StorKoorespondencii.Data;
using StorKoorespondencii.DataProcessing.Dto.Export;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorKoorespondencii.DataProcessing
{
    public class Serializer
    {
        public static string ExportAllCorrespondenceJson(StoreDbContext context)
        {
            var output = context
                    .Login
                    .Select(l => new
                    {
                        Username = l.SUName,
                        permisions = l.Permitions.Select(p => new
                        {
                            KtCode = p.SCCtCode,
                            DtCode = p.SCDtCode,
                            DotCode =p.SDOTCode
                        })
                        .ToArray()
                    })
                    .ToArray();



            string json = JsonConvert.SerializeObject(output, Formatting.Indented);

            return json.Trim();
        }

        public static string AllCorrespondeceTable(StoreDbContext context)
        {

            string json = ExportAllCorrespondenceJson(context);

            
            var sb = new StringBuilder();

            ExportAllCorrespondenceDto[] users = context
                   .Login
                   .Select(l => new ExportAllCorrespondenceDto()
                   {
                       UserName = l.SUName,
                       Correspondences = l.Permitions.Select(p => new UserCorrespondencesArrayDto()
                       {
                           KtCode = p.SCCtCode,
                           DotCode = p.SDOTCode,
                           DtCode = p.SCDtCode
                       })
                       .ToArray()

                   })
                   .ToArray();


            if (users.Length == 0)
            {
                return "";
            }

            Console.WriteLine(new string('-', 43));
            foreach (var user in users)
            {
                if (user.Correspondences.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{String.Format("{0,-43}", user.UserName)}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(new string('-', 43));

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{String.Format("{0,-43}", user.UserName + " - No correspondences")}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(new string('-', 43));
                }

                if (user.Correspondences.Length>0)
                {
                    foreach (var correspondency in user.Correspondences)
                    {
                        Console.WriteLine($"| {String.Format("{0,-12}", correspondency.DtCode)}| {String.Format("{0,-12}", correspondency.KtCode)}| {String.Format("{0,-12}", correspondency.DotCode)}|");
                    }
                }
                Console.WriteLine(new string('-', 43));

            }               
            return sb.ToString().Trim();
        }

        public static string AllCorrespondeceTable(StoreDbContext context, string username, bool notUniqueUser = true)
        {

            string json = ExportAllCorrespondenceJson(context);


            var sb = new StringBuilder();
            ExportAllCorrespondenceDto[] users;
            if (notUniqueUser)
            {
                users = context
                   .Login
                   .Where(l => l.SUName.Contains(username))
                   .Select(l => new ExportAllCorrespondenceDto()
                   {
                       UserName = l.SUName,
                       Correspondences = l.Permitions.Select(p => new UserCorrespondencesArrayDto()
                       {
                           KtCode = p.SCCtCode,
                           DotCode = p.SDOTCode,
                           DtCode = p.SCDtCode
                       })
                       .ToArray()

                   })
                   .ToArray();
            }
            else
            {
                users = context
                   .Login
                   .Where(l => l.SUName ==username)
                   .Select(l => new ExportAllCorrespondenceDto()
                   {
                       UserName = l.SUName,
                       Correspondences = l.Permitions.Select(p => new UserCorrespondencesArrayDto()
                       {
                           KtCode = p.SCCtCode,
                           DotCode = p.SDOTCode,
                           DtCode = p.SCDtCode
                       })
                       .ToArray()

                   })
                   .ToArray();
            }

            if (users.Length==0)
            {
                return "";
            }



            Console.WriteLine(new string('-', 43));
            foreach (var user in users)
            {
                if (user.Correspondences.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{String.Format("{0,-43}", user.UserName)}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(new string('-', 43));
                    
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{String.Format("{0,-43}", user.UserName + " - No correspondences")}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(new string('-', 43));
                }

                if (user.Correspondences.Length > 0)
                {
                    foreach (var correspondency in user.Correspondences)
                    {
                        Console.WriteLine($"| {String.Format("{0,-12}", correspondency.DtCode)}| {String.Format("{0,-12}", correspondency.KtCode)}| {String.Format("{0,-12}", correspondency.DotCode)}|");
                    }
                }
                Console.WriteLine(new string('-', 43));

            }
            return sb.ToString().Trim();
        }

        public static string AddCorrespondenceToUser(StoreDbContext context, string userName) 
        {
            return "";
        }
    }
}
