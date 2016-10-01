namespace Aimp.UserRights
{
    public class RightDelete : IRight
    {
        public string Id
        {
            get
            {
                return "Delete";
            }
        }

        public string Name
        {
            get
            {
                return "Удаление";
            }
        }
    }
}
