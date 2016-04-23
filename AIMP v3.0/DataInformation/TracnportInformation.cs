using AIMP_v3._0.DataAccess;
using Models.TrancportInfo;
using System;

namespace AIMP_v3._0.DataInformation
{
    public class TracnportInformation
    {
        private static TrancportInfo _info;
         
        private TracnportInformation()
        {

        }

        public static TrancportInfo Info
        {
            get
            {
                if (_info == null)
                {
                    using (AimpService aimp = new AimpService())
                    {
                        _info = aimp.GetTrancportInfo();

                        if (_info.Error)
                            throw new Exception(_info.Message);
                    }
                }
                return _info;
            }
        }
    }
}
