using System;
using Api.Model;


namespace Api.Context
{
    public class DbInitializer
    {
        public static void Initialize(ApiContext context) {
            bool init = context.Database.EnsureCreated();

            if (init)
            {
                Blog llorachdevs = new Blog(DateTime.Now,"llorachdevs", "https://www.llorachdevs.com");

                
                Blog_User_Role admin_role = new Blog_User_Role(DateTime.Now,"ADMIN");

                DateTime pablo_admin_birthday = new DateTime(1984,11,03);

                
                Blog_User pablo_admin = new Blog_User(DateTime.Now, "PABLO", "ENRIQUE", "LLORACH", "PAZ", "llorach.pablo@llorachdevs.com", "llorach.pablo@llorachdevs.com", pablo_admin_birthday, "13aBr2009", "+59891211845", llorachdevs, admin_role);

                Blog_Article formik = new();
                formik.Blog_Article_Id = DateTime.Now;
                formik.Link = "https://garsiv1932.github.io/formik/";
                formik.Image = "https://www.llorachdevs.com/static/media/formik.1abe3c8d.png";
                formik.Tittle = "Creando Formularios con Formik";

                Blog_Article nagios = new();
                nagios.Blog_Article_Id = DateTime.Now;
                nagios.Link = "https://garsiv1932.github.io/nagios-mikrotik-ssh/";
                nagios.Image = "https://www.llorachdevs.com/static/media/Dibujo.f498333c.jpg";
                nagios.Tittle = "Alertas de tus Mikrotiks en Nagios";

                Blog_Article ipmi = new();
                ipmi.Blog_Article_Id = DateTime.Now;
                ipmi.Link = "https://garsiv1932.github.io/nagios-ipmi/";
                ipmi.Image = "https://www.llorachdevs.com/static/media/IPMI.6975eb1c.jpg";
                ipmi.Tittle = "Alertas del Hardware de tu server mediante IPMI";
                
                Blog_Article_Comment ipmi_comment_uno = new Blog_Article_Comment();
                ipmi_comment_uno.Blog_Article_Comment_Id = DateTime.Now;
                ipmi_comment_uno.Comment = "Muy bueno";
                ipmi_comment_uno.Published = DateTime.Now.AddDays(-3);
                ipmi_comment_uno.Ip_Address = "192.168.0.1";
                
                Blog_Article_Comment ipmi_comment_dos = new Blog_Article_Comment();
                ipmi_comment_dos.Blog_Article_Comment_Id = DateTime.Now;
                ipmi_comment_dos.Comment = "Una porqueria";
                ipmi_comment_dos.Published = DateTime.Now.AddDays(-5);
                ipmi_comment_dos.Ip_Address = "192.168.0.10";
                
                ipmi.Comments.Add(ipmi_comment_uno);
                ipmi.Comments.Add(ipmi_comment_dos);

                pablo_admin.Articles.Add(formik);
                pablo_admin.Articles.Add(nagios);
                pablo_admin.Articles.Add(ipmi);
                
                llorachdevs.Blog_Users.Add(pablo_admin);
                context.Db_Blogs.Add(llorachdevs);
                
                context.SaveChanges();
            }
        }



        public DbInitializer()
        {
        }
    }
}