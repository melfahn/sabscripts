namespace SABSync.Entities
{
    public class ShowAlias
    {
        public string Alias { get; set; }
        public string BadName { get; set; }

        public override string ToString()
        {
            return string.Format("Alias={0}, BadName={1}", Alias, BadName);
        }
    }
}