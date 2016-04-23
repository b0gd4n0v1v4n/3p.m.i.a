namespace Models.SecurityRigths.Operations
{
    public class AddOperation : IRight
    {
        public string Id
        {
            get
            {
                return "Add";
            }
        }

        public string Name
        {
            get
            {
                return "Добавление";
            }
        }
    }
}
