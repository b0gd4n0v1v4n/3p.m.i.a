using System.Reflection;
using Models.Entities;

namespace AIMP_v3._0.Extensions
{
    public static class ObjectExtansions
    {
        public static bool CompareProperties(this object obj,object objTwo)
        {
            if (obj == null || objTwo == null)
                return false;

            foreach (PropertyInfo iProperty in obj.GetType().GetProperties())
            {
                var propertyTwo = objTwo.GetType().GetProperty(iProperty.Name);
                if (propertyTwo == null)
                    return false;
                var valueObj = iProperty.GetValue(obj, null);
                var valueObjTwo = propertyTwo.GetValue(objTwo, null);
                if (valueObj != null)
                {
                    if (valueObj.GetType().GetInterface("IEntity") != null)
                    {
                        if (((IEntity) valueObjTwo)?.Id != ((IEntity) valueObj)?.Id)
                            return false;
                    }
                    else if (valueObj?.ToString() != valueObjTwo?.ToString())
                        return false;
                }
            }

            return true;
        }
    }
}
