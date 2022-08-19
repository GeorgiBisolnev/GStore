using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StorKoorespondencii.Data;
using StorKoorespondencii.Data.Models;
using StorKoorespondencii.DataProcessing.Dto.Export;
using System.Data;
using System.Text;

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
                return "Няма потребители за показване";
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
                    continue;
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

            //string json = ExportAllCorrespondenceJson(context);


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
                    continue;
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

        public static string AddCorrespondenceToUser(StoreDbContext context, int userId, string Kt, string Dt, string Dot) 
        {
            
            var maxID = context.USCCPerm.Max(c => c.ID) + 1;
            var permisionToAdd = new UserPermition()
            {
                SUid = userId,
                SCCtCode = Kt,
                SDOTCode = Dot,
                SCDtCode = Dt,
                ve_SDoc = 1,
                v_SDoc = 1,
                vc_Price =1,
                ve_ISDocDt = 1,
                ve_ISDocCt =1,
                can_Block = 1,
                can_Unblock = 1,
                ID = maxID
            };

            context.Add(permisionToAdd);

            context.SaveChanges();

            string userName = context.Login.Where(u=>u.SUid==userId).Select(u=>u.SUName).First();
            return $"Въведени права за потребител {userName}, Дт код [{Dt}] Кт код [{Kt}] Тип документ [{Dot}]";
        }

        public static string UpdatePermUserName(StoreDbContext context, string userName)
        {
            int userId = -1;

            try
            {
                userId = context.Login.Where(u => u.SUName == userName).Select(u => u.SUid).First();
            }
            catch (Exception)
            {

                return "Няма такъв потребител";
            }

            string sql =
                $"EXEC [dbo].[up_SetUPermID_User] @SUid";

            string result=string.Empty;

            using (SqlConnection conn = new SqlConnection(Configuration.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@SUid", SqlDbType.VarChar);
                cmd.Parameters["@SUid"].Value = userId;
                try
                {
                    conn.Open();
                    result = (string)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }
            }
            return $"Права на потребител {userName} са обновени";
        }

        public static string UpdatePermAllUsers (StoreDbContext context)
        {
            int[] userIds = context
                .Login
                .Select(u => u.SUid)
                .ToArray();

            string sql =
                $"EXEC [dbo].[up_SetUPermID_User] @SUid";

            if (userIds.Length > 0)
            {
                string result = string.Empty;

                using (SqlConnection conn = new SqlConnection(Configuration.ConnectionString))
                {
                    try
                    {
                        conn.Open();

                        foreach (var id in userIds)
                        {
                            SqlCommand cmd = new SqlCommand(sql, conn);
                            cmd.Parameters.Add("@SUid", SqlDbType.VarChar);
                            cmd.Parameters["@SUid"].Value = id;
                        
                            result = (string)cmd.ExecuteScalar();
                        }
                                
                    }
                    catch (Exception ex)
                    {
                        return "Грешка при обновяване на права на потребител\n" + ex.Message;
                    }
                }           
                    
            }
            return "Всички права на потребителите са обновени!";
            
        }

        public static string DeletePermUserName(StoreDbContext context, string username)
        {
            var userId = context.Login.Where(u => u.SUName == username).Select(u => u.SUid).FirstOrDefault();

            if (userId==0)
            {
                return "Не съществува потребител с подаденото име";
            }

            var PermIds = context.USCCPerm.Where(p => p.SUid == userId).Select(s=>s.ID).ToArray();


            List<UserPermition> permisionsToRemove = new List<UserPermition>();

            foreach (var per in PermIds)
            {
                UserPermition up = new UserPermition();

                up.ID = per;

                permisionsToRemove.Add(up);
            }

            context.RemoveRange(permisionsToRemove);
            context.SaveChanges();

            return $"Правата на потребител {username} са изтрити!";
        }
    }
}
