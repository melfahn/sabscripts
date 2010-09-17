using System;

namespace SABSync.Entities
{
    public class NzbInfo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public NzbSite Site { get; set; }
        public Uri Link { get; set; }
        public string Description { get; set; }

        public bool IsPassworded()
        {
            return Title.EndsWith("(Passworded)", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}