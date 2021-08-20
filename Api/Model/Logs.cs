using System;

#nullable disable

namespace Api.Model
{
    public partial class Logs
    {
        public Guid LogId { get; set; }

        // [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        public Guid UserId { get; set; }
        public WebUser User { get; set; }
        public string IpClient { get; set; }
        public string Description { get; set; }
        public string Info { get; set; }
    }
}
