using System;
using Api.Model;
using Api.SRVs;
using AutoMapper.Configuration;


namespace Api.Context
{
    public class DbInitializer
    {
        private readonly ServiceWeb _serviceWeb;
        private readonly ApiContext _context;

        public DbInitializer(ServiceWeb serviceWeb,ApiContext context)
        {
            _serviceWeb = serviceWeb;
            _context = context;
        }
        public  void Initialize() {
            bool init = _context.Database.EnsureCreated();

            if (init)
            {
                Web llorachdevs = new Web("llorachdevs", "https://www.llorachdevs.com");

                llorachdevs.Secret = _serviceWeb.TokenCreator(llorachdevs.Site_Link);

                WebUserRole admin_role = new WebUserRole("ADMIN");
                WebUserRole user_role = new WebUserRole("USER");

                DateTime pablo_admin_birthday = new DateTime(1984,11,03);

                
                WebUser pablo_admin = new WebUser("PABLO", "ENRIQUE", "LLORACH", "PAZ", "llorach.pablo@llorachdevs.com", "llorach.pablo@llorachdevs.com", pablo_admin_birthday, "13aBr2009", "+59891211845", llorachdevs, admin_role);
                WebUser enrique_user = new WebUser("ENRIQUE", "PAZ", "", "", "enrique.paz@llorachdevs.com", "enrique.paz@llorachdevs.com", pablo_admin_birthday, "13aBr2009", "+59891211846", llorachdevs, user_role);
                
                WebArticle formik = new();
                formik.ArticleLink = "https://garsiv1932.github.io/formik/";
                formik.Image = "https://www.llorachdevs.com/static/media/formik.1abe3c8d.png";
                formik.Tittle = "Creando Formularios con Formik";
                formik.User = pablo_admin;
                formik.UserEmail = pablo_admin.Email;

                WebArticle nagios = new();
                nagios.ArticleLink = "https://garsiv1932.github.io/nagios-mikrotik-ssh/";
                nagios.Image = "https://www.llorachdevs.com/static/media/Dibujo.f498333c.jpg";
                nagios.Tittle = "Alertas de tus Mikrotiks en Nagios";
                nagios.User = pablo_admin;
                nagios.UserEmail = pablo_admin.Email;

                WebArticle ipmi = new();
                ipmi.ArticleLink = "https://garsiv1932.github.io/nagios-ipmi/";
                ipmi.Image = "https://www.llorachdevs.com/static/media/IPMI.6975eb1c.jpg";
                ipmi.User = pablo_admin;
                ipmi.UserEmail = pablo_admin.Email;
                ipmi.PublishDate = DateTime.Now.AddYears(-1);
                ipmi.Tittle = "Alertas del Hardware de tu server mediante IPMI";
                
                WebArticleComment ipmi_comment_uno = new WebArticleComment();
                ipmi_comment_uno.User = enrique_user;
                ipmi_comment_uno.UserEmail = enrique_user.Email;
                ipmi_comment_uno.Comment = "Muy bueno";
                ipmi_comment_uno.Published = DateTime.Now.AddDays(-3);
                ipmi_comment_uno.IpAddress = "192.168.0.1";
                
                WebArticleComment ipmi_comment_dos = new WebArticleComment();
                ipmi_comment_dos.User = enrique_user;
                ipmi.UserEmail = enrique_user.Email;
                ipmi_comment_dos.Comment = "Una porqueria";
                ipmi_comment_dos.Published = DateTime.Now.AddDays(-5);
                ipmi_comment_dos.IpAddress = "192.168.0.10";

                ipmi.Comments.Add(ipmi_comment_uno);
                ipmi.Comments.Add(ipmi_comment_dos);
                
                pablo_admin.Articles.Add(formik);
                pablo_admin.Articles.Add(nagios);
                pablo_admin.Articles.Add(ipmi);
                
                llorachdevs.users.Add(pablo_admin);
                _context.Db_Webs.Add(llorachdevs);
                
                _context.SaveChanges();
            }
        }



        public DbInitializer()
        {
        }
    }
}