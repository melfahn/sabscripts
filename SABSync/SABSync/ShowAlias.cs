namespace SABSync
{
    public class ShowAlias
    {
        public ShowAlias()
        {
            
        }

        public ShowAlias(string badName, string alias)
        {
            Alias = alias;
            BadName = badName;
        }

        public string Alias { get; set; }
        public string BadName { get; set; }
        public override string ToString()
        {
            return string.Format("Alias={0}, BadName={1}", Alias, BadName);
        }
    }
}