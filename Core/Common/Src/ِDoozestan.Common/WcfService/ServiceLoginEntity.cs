using System;

namespace Doozestan.Common.WcfService
{
    public class ServiceLoginEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string IP { get; set; }

        public string TockenKey { get; set; }

        public DateTime LastSet { get; set; }
    }
}
