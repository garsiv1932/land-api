using System;
using System.Collections.Generic;

namespace Api.Model
{
    public abstract class Web_Resource
    {
        public string resource_link { get; set; }
        public List<string> images { get; set; }

        public Web_Resource()
        {
            images = new List<string>();
        }

        protected Web_Resource(string resourceLink)
        {
            resource_link = resourceLink;
            images = new List<string>();
        }

    }
}