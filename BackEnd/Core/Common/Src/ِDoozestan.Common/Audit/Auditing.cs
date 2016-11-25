namespace Doozestan.Common.Audit
{
    public  class Auditing
    {
        public int AuditId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string IPAddress { get; set; }
        public string UrlAccessed { get; set; }
        public System.DateTime TimeAccessed { get; set; }
        public string CurrentAction { get; set; }
        public string CurrentController { get; set; }
        public string CurrentArea { get; set; }
    }
}
