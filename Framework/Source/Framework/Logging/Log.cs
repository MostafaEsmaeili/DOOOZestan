using System;

namespace Framework.Logging
{
    public class Log
    {
        private string machineName = Environment.MachineName;
        public int Id { get; set; }
        public DateTime EventDateTime { get; set; }
        public string EventLevel { get; set; }
        public string UserName { get; set; }
        public string MachineName 
        {
            get { return machineName; }
            set { machineName = value; }
        } 
        public string ErrorSource { get; set; }
        public string ErrorClass { get; set; }
        public string ErrorMethod { get; set; }
        public string InnerErrorMessage { get; set; }
    }
}
