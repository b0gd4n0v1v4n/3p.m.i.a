using AimpDataAccess.Context;
using Models.Entities;

namespace AimpLogic.Helpres
{
    public class UserFileCheck
    {
        public static void AddOrUpdate(IAimpContext context,IEntity entity,UserFile newFile,UserFile dbOldFile)
        {
            if (newFile != null)
            {
                if (entity.Id == 0)
                {
                    if (newFile != null)
                    {
                        context.UserFiles.AddOrUpdate(newFile);
                        context.SaveChanges();
                    }
                }
                else
                {
                    if (newFile == null && dbOldFile?.Id != null)
                    {
                        context.UserFiles.Delete(dbOldFile.Id);
                    }
                    else
                    {
                        if (newFile != null)
                        {
                            newFile.Id = dbOldFile?.Id ?? 0;
                            context.UserFiles.AddOrUpdate(newFile);
                            context.SaveChanges();
                        }
                    }
                }
            }
            else
            {
                if (dbOldFile?.Id != null)
                    context.UserFiles.Delete(dbOldFile.Id);
            }

        }
    }
}
