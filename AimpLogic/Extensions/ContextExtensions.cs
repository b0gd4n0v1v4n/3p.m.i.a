using AimpDataAccess.Context;
using Models.Entities;
using System.Collections;

namespace AimpLogic.Extensions
{
    public static class ContextExtensions
    {
        public static void UserFileUpdate(this IAimpContext context, int? newFileId, UserFile newFile,UserFile oldFile)
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
