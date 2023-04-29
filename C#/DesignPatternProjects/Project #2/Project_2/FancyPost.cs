/*
 * File Name: Post.cs
 * Author: Seolan Jin
 * Date: March 10, 2023
 * Description: This class implements Post, overrides Getters.
 */
namespace Assi2
{
    class FancyPost : Post
    {
        public FancyPost(string t, string b) : base(t, b) {

        }

        // Returns the post title in a fancy way
        public override string GetPrintableTitle()
        {
            string result = $"<<{Title}>>";
            return result;
        }

        // Returns the post body in a fancy way
        public override string GetPrintableBody()
        {
            // Declares strings to format the output properly
            string temp1 = new string('-', Body.Length);
            string temp2 = new string(' ', Body.Length);

            string result = "";
            result += $"| {temp2} |\n";
            result += $"| {Body} |\n";
            result += $"| {temp2} |\n";
            result += $"+-{temp1}-+";
            return result;
        }
    }
}
