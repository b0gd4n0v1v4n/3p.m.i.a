namespace Aimp.Model.SecurityRigths
{
    public class AdminRight : IRight
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
