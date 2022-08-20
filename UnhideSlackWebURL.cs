namespace StorKoorespondencii
{
    using System.Text.RegularExpressions;
    public class UnhideSlackWebURL
    {
        public static string Unhide(string originalURL)
        {
            string regexPatern = "@@@";

            return Regex.Replace(originalURL, regexPatern, "");
        }
    }
}
