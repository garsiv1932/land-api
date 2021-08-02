using System.Collections.Generic;

namespace Api.Model
{
    public class Web_Resource_Blog:Web_Resource
    {
        public List<Web_Resource_Blog_Article> articles { get; set; }

        public Web_Resource_Blog(string resourceLink):base(resourceLink)
        {
            articles = new List<Web_Resource_Blog_Article>();
        }

        public Web_Resource_Blog()
        {
            
        }


    }
}