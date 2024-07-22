using Install;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uninstall
{
    public class Program
    {
        static void Main(string[] args)
        {
            Log("卸载程序执行开始...");
            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            //要删除的文件名
            var files = currentDirectory.GetFiles("*.addin")?.Select(x => x.Name)?.ToList();
            if (files.Count == 0 || files == null)
            {
                Log($"没有找到有效的addin文件");
                return;
            }

            string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var addinDirectorInfo = new DirectoryInfo(Path.Combine(programData, @"Autodesk\Revit\Addins"));

            var directories = addinDirectorInfo.GetDirectories();

            foreach (var directoryInfo in directories)
            {
                //进入文件夹
                if (directoryInfo.Name.Length == 4 && directoryInfo.Name.StartsWith("20"))
                {
                    var childrenFile = directoryInfo.GetFiles();
                    //在当前版本文件夹中删除目标文件
                    var file = childrenFile.Where(x => x.Extension == ".addin" && files.Contains(x.Name)).FirstOrDefault();
                    if (file != null)
                    {
                        file.Delete();
                        Log($"删除{directoryInfo.Name}{file.Name}文件");
                    }
                }
            }
            Log("卸载程序执行结束...");
        }

        static void Log(string message)
        {
            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            var logFile = Path.Combine(currentDirectory.FullName, "log-install.txt");
            File.AppendAllText(logFile, $"{DateTime.Now:yyyy-MM-dd HH.mm.ss} {message}\n");
        }
    }
}
