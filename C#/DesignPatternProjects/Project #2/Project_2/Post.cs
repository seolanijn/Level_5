/*
 * File Name: Post.cs
 * Author: Seolan Jin
 * Date: March 10, 2023
 * Description: This class implements Content, overrides Getters.
 */
namespace Assi2
{
    class Post : Content
    {
        public string Title;
        public string Body;

        public Post(string t, string b) {
            this.Title = t;
            this.Body = b;
        }

        public override string GetPrintableTitle()
        {
            return Title;
        }
        public override string GetPrintableBody()
        {
            return Body;
        }
    }
}
