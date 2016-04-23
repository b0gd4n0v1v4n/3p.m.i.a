namespace Models.SecurityRigths.Operations
{
    public class ViewOperation : IRight
    {
        public string Id
        {
            get
            {
                return "View";
            }
        }

        public string Name
        {
            get
            {
                return "Просмотр";
            }
        }
    }
}
