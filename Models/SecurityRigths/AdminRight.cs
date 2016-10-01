namespace Models.SecurityRigths
{
    public class RightAdmin : IRight
    {
        public string Id
        {
            get
            {
                return "Admin";
            }
        }

        public string Name
        {
            get
            {
                return "Администратор";
            }
        }
    }
}
