using FileStorage.BLL.Model;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Compression;

namespace FileStorage.Web.Infrastructure
{
    public class IOService
    {
        public void SaveFile(FileDTO value, FolderDTO folder)
        {
            value.Id = Guid.NewGuid();
            var relativeUserFolder = Path.Combine("Files", value.UserId.ToString());
            if (folder != null)
                relativeUserFolder = folder.FullPath;
            var fullUserFolder = Path.Combine(Directory.GetCurrentDirectory(), relativeUserFolder);
            var fullPath = Path.Combine(fullUserFolder, value.DiskFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                value.File.CopyTo(stream);
            }

            if (value.FolderId == Guid.Empty)
                value.FolderId = null;
        }

        public void DeleteFile(FileDTO file)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), file.Path);
            File.Delete(path);
        }

        public void CreateFolder(FolderDTO folder, FolderDTO parent)
        {
            var userFolder = Path.Combine("Files", folder.UserId.ToString());
            if (parent != null)
                userFolder = parent.FullPath;
            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Path.Combine(userFolder, folder.FolderName)));
        }

        public void MoveFolder(FolderDTO value, FolderDTO parent)
        {
            try
            {
                if (parent != null)
                {
                    Directory.Move(Path.Combine(Directory.GetCurrentDirectory(), value.FullPath), Path.Combine(Directory.GetCurrentDirectory(), @$"{parent.FullPath}\{value.FolderName}"));
                }
                else
                {
                    string path = @$"Files\{value.UserId}\{value.FolderName}";
                    Directory.Move(Path.Combine(Directory.GetCurrentDirectory(), value.FullPath), Path.Combine(Directory.GetCurrentDirectory(), path));
                }
            }
            catch (IOException) { }
        }

        public void RenameFolder(FolderDTO value, FolderDTO origin)
        {
            Directory.Move(Path.Combine(Directory.GetCurrentDirectory(), origin.FullPath), Path.Combine(Directory.GetCurrentDirectory(), origin.ParentPath + "\\" + value.FolderName));
            
        }

        public void MoveFile(FileDTO value, FolderDTO parent)
        {
            if (parent != null)
            {
                File.Move(value.Path, parent.FullPath + "\\" + value.DiskFileName);
            }
            else
            {
                string path = @$"Files\{value.UserId}\{value.DiskFileName}";
                File.Move(value.Path, Path.Combine(Directory.GetCurrentDirectory(), path));
            }
        }

        public void RenameFile(FileDTO value, FileDTO origin)
        {
            var path = origin.Path.Split("\\");
            path = path.Take(path.Length - 1).ToArray();
            string fullPath = string.Join('\\', path);
            File.Move(Path.Combine(Directory.GetCurrentDirectory(), origin.Path), Path.Combine(Directory.GetCurrentDirectory(), fullPath + "\\" + value.DiskFileName));
        }

        public Stream DownloadFolder(FolderDTO item)
        {
            var tempId = Guid.NewGuid();
            ZipFile.CreateFromDirectory(item.FullPath, item.ParentPath + $"\\{tempId}.zip");
            return new FileStream(path: item.ParentPath + $"\\{tempId}.zip", mode: FileMode.Open);
        }
    }
}
