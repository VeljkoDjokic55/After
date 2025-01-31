using System.Collections.Generic;

namespace AFTER.Shared.Common
{
    public class EmailData
    {
        public string Subject { get; set; }

        public string Content { get; set; }

        public string To { get; set; }

        public bool IsContentHtml { get; set; }

        public List<string> CcList { get; set; }

        public EmailData()
        {
            CcList = new List<string>();
        }

        public EmailData(string to, string subject, string content, bool isContentHtml = true)
        {
            To = to;
            Subject = subject;
            Content = content;
            IsContentHtml = isContentHtml;
            CcList = new List<string>();
        }
    }
}
