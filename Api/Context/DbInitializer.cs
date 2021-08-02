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
                Web llorachdevs = new Web("llorachdevs", "https://www.llorachdevs.com");

                
                Web_User_Role admin_role = new Web_User_Role(DateTime.Now,"ADMIN");
                Web_User_Role user_role = new Web_User_Role(DateTime.Now,"USER");

                DateTime pablo_admin_birthday = new DateTime(1984,11,03);

                
                Web_User pablo_admin = new Web_User("PABLO", "ENRIQUE", "LLORACH", "PAZ", "llorach.pablo@llorachdevs.com", "llorach.pablo@llorachdevs.com", pablo_admin_birthday, "13aBr2009", "+59891211845", llorachdevs, admin_role);
                Web_User enrique_user = new Web_User("ENRIQUE", "PAZ", "", "", "enrique.paz@llorachdevs.com", "enrique.paz@llorachdevs.com", pablo_admin_birthday, "13aBr2009", "+59891211846", llorachdevs, user_role);
                
                Web_Resource_Blog_Article formik = new();
                formik.Article_Link = "https://garsiv1932.github.io/formik/";
                formik.Image = "https://www.llorachdevs.com/static/media/formik.1abe3c8d.png";
                formik.Tittle = "Creando Formularios con Formik";
                formik.User = pablo_admin;
                formik.Blog_User_Email = pablo_admin.Email;

                Web_Resource_Blog_Article nagios = new();
                nagios.Article_Link = "https://garsiv1932.github.io/nagios-mikrotik-ssh/";
                nagios.Image = "https://www.llorachdevs.com/static/media/Dibujo.f498333c.jpg";
                nagios.Tittle = "Alertas de tus Mikrotiks en Nagios";
                nagios.User = pablo_admin;
                nagios.Blog_User_Email = pablo_admin.Email;

                Web_Resource_Blog_Article ipmi = new();
                ipmi.Article_Link = "https://garsiv1932.github.io/nagios-ipmi/";
                ipmi.Image = "https://www.llorachdevs.com/static/media/IPMI.6975eb1c.jpg";
                ipmi.User = pablo_admin;
                ipmi.Blog_User_Email = pablo_admin.Email;
                ipmi.Publish_Date = DateTime.Now.AddYears(-1);
                ipmi.Tittle = "Alertas del Hardware de tu server mediante IPMI";
                
                Web_Resource_Blog_Article_Comment ipmi_comment_uno = new Web_Resource_Blog_Article_Comment();
                ipmi_comment_uno.User = enrique_user;
                ipmi_comment_uno.User_Email = enrique_user.Email;
                ipmi_comment_uno.Comment = "Muy bueno";
                ipmi_comment_uno.Published = DateTime.Now.AddDays(-3);
                ipmi_comment_uno.Ip_Address = "192.168.0.1";
                
                Web_Resource_Blog_Article_Comment ipmi_comment_dos = new Web_Resource_Blog_Article_Comment();
                ipmi_comment_dos.User = enrique_user;
                ipmi.Blog_User_Email = enrique_user.Email;
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