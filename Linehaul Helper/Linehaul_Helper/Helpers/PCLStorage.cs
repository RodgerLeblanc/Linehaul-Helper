using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linehaul_Helper.Helpers
{
    class PCLStorage
    {
        public async static Task PCLStorageSave(string folderName, string fileName, string text)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync(folderName,
                CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync(fileName,
                CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(text);
        }

        public async static Task<string> PCLStorageLoad(string folderName, string fileName)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync(folderName,
                CreationCollisionOption.OpenIfExists);
            IFile file = await folder.GetFileAsync(fileName);
            byte[] buffer;
            using (System.IO.Stream stream = await file.OpenAsync(FileAccess.Read))
            {
                buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, (int)stream.Length);
            }
            return System.Text.Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }
    }
}
