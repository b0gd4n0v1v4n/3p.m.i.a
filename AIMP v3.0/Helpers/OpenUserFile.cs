using System;
using System.IO;
using System.Windows;
using AIMP_v3._0.DataAccess;

namespace AIMP_v3._0.Helpers
{
    public static class OpenUserFile
    {
        public static void Open(string name, byte[] body)
        {
            File.WriteAllBytes(name, body);
            System.Diagnostics.Process.Start(name);
        }

        public static void GetAndOpen(int id)
        {
            try
            {
                using (AimpService service = new AimpService())
                {
                    var file = service.GetUserFile(id);
                    
                    Open(file.Name,file.File);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Не удалось открыть файл");
            }
        }

        public static byte[] GetFile(string path)
        {
            byte[] file;
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);
                }
            }
            return file;
        }
    }
}
