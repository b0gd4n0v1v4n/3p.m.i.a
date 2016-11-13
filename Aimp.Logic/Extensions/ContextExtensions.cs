using Aimp.DataAccess.Interfaces;
using Entities;
using System.Collections;

namespace Aimp.Logic.Extensions
{
    public static class ContextExtensions
    {
#warning refactoring
        public static void UserFileUpdate(this IDataContext context, int? newFileId, UserFile newFile,UserFile oldFile)
        {
            if (StructuralComparisons.StructuralEqualityComparer.Equals(newFile?.File, oldFile?.File))
                return;

            if (newFileId.HasValue && oldFile?.Id == newFileId.Value)
                return;

            if (oldFile != null)
                context.UserFiles.Delete(oldFile);

            if (newFile != null)
                context.UserFiles.AddOrUpdate(newFile);
        }
    }
}
