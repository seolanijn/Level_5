/*
 * File Name: PostProxy.cs
 * Author: Seolan Jin
 * Date: March 10, 2023
 * Description: This class implements Content, overrides Getters, and defines some useful methods to simulate the program.
 */
namespace Assi2
{
    class PostProxy : Content
    {
        // Properties
        public Post _post;
        public string Title;
        public string Body;

        // Constructor
        public PostProxy() {
            // Set Title and Body as "Loading..."
            Title = "Loading...";
            Body = "Loading...";
        }

        public void Download()
        {
            // Set a Proxy's post content to a FancyPost
            _post = new FancyPost("Downloaded", "Downloaded Body");
        }
        public void SetTitle(string title)
        {
            _post.Title = title;
        }
        public void SetBody(string body)
        {
            _post.Body = body;
        }

        public override string GetPrintableTitle()
        {
            // If post content is not defined yet, returns Proxy's Title
            if (_post == null)
            {
                return Title;
            }
            // Otherwise, returns post's Title
            return _post.GetPrintableTitle();
        }

        public override string GetPrintableBody()
        {
            // If post content is not defined yet, returns Proxy's Body
            if (_post == null)
            {
                return Body;
            }
            // Otherwise, returns post's Body
            return _post.GetPrintableBody();
        }

        public PostProxy Clone()
        {
            // Creates a new PostProxy
            PostProxy postProxy = new PostProxy();
            // Sets the Proxy's post content with default title and body
            postProxy._post = new Post("Default Title", "Default Body");
            return postProxy;
        }
    }
}
