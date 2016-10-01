namespace Aimp.UserRights
{
    public class RightView : IRight
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
