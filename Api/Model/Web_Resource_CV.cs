using System.Collections.Generic;

namespace Api.Model
{
    public class Web_Resource_CV:Web_Resource
    {
        public string cv_link { get; set; }
        public List<Web_Resource_CV_Experience> workExperience { get; set; }

        public Web_Resource_CV()
        {
            workExperience = new List<Web_Resource_CV_Experience>();
        }

        public Web_Resource_CV(string pResourceLink,string cvLink):base(pResourceLink)
        {
            cv_link = cvLink;
            workExperience = new List<Web_Resource_CV_Experience>();
        }

    }
}