using System;
using System.IO;
using System.Linq;


namespace Install
{
    public class Program
    {
        static void Main(string[] args)
        {
            Log("安装程序执行开始...");
            var installerDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            var projectDirectory = installerDirectory.Parent;
            //获取模板addin文件
            var templateFile = installerDirectory.GetFiles("*.addin")?.FirstOrDefault();
            if (templateFile == null)
            {
                Log("未在当前文件夹中找到addin文件");
                return;
            }

            //获取本机addin的复制路径
            var programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            DirectoryInfo addinsDirectoryInfo = new DirectoryInfo(Path.Combine(programData, @"Autodesk\Revit\Addins"));
            if (!addinsDirectoryInfo.Exists)
            {
                Log($"{addinsDirectoryInfo.FullName}路径不存在");
                return;
            }
            var revitAddinDirectores = addinsDirectoryInfo
                .GetDirectories()?.ToList()
                .Where(x => x.Name.Length == 4 && x.Name.StartsWith("20"));
            var revitAddinVersion = revitAddinDirectores.Select(x => x.Name);

            ////获取插件支持的Revit版本
            //var versionDirectory = currentDirectory
            //    .Parent
            //    .GetDirectories().First(x => x.Name == "doc")
            //    .GetDirectories().FirstOrDefault(x => x.Name == "version")
            //    .GetDirectories().Where(x => x.Name.Length == 4 && x.Name.StartsWith("20"));
            //var dllCanInstallVersion = versionDirectory
            //    ?.Where(x => x.GetFiles() != null)
            //    ?.Select(x => x.Name).ToList();

            //获取插件要安装的版本
            var versionTxt = installerDirectory.GetFiles("version.txt")?.FirstOrDefault();
            if (versionTxt == null)
            {
                Log("没有找到version.txt配置文件");
                return;
            }
            var fileTxt = File.ReadAllText(versionTxt.FullName);
            var textIndicateVersion = fileTxt?.Split(';')
                .Where(x => x.Length == 4 && x.StartsWith("20")).ToList();


            //综合判断后，最终要安装的路径
            var installVersionDirectory = revitAddinDirectores
                .Where(x => textIndicateVersion.Contains(x.Name)).ToList();
            if (installVersionDirectory.Count == 0)
            {
                Log("没有在version.txt文件中找到指示的安装版本");
                return;
            }

            //app的程序集
            //var dllDirectory = currentDirectory.GetDirectories()
            //    .FirstOrDefault(x => x.Name == "share");
            //if (dllDirectory == null)
            //{
            //    dllDirectory.Create();
            //}

            //遍历各个版本文件夹
            foreach (var directoryInfo in installVersionDirectory)
            {
                using (StreamReader streamReader = new StreamReader(templateFile.FullName))
                {
                    var name = templateFile.Name;
                    string path = Path.Combine(addinsDirectoryInfo.FullName, directoryInfo.Name, templateFile.Name);
                    if (File.Exists(path))
                    {
                        File.Delete(path);

                    }
                    var flag = false;
                    using (StreamWriter streamWriter = new StreamWriter(path))
                    {
                        while (streamReader.Peek() != -1)
                        {
                            string text = streamReader.ReadLine();
                            if (text != null && text.Contains("Assembly"))
                            {
                                text = text.Replace("{path}", projectDirectory.FullName);
                                text = text.Replace("{version}", directoryInfo.Name);
                                Log($"复制{templateFile.Name}到{directoryInfo.Name}文件夹，并替换启动路径为{text}");
                                flag = true;
                            }
                            streamWriter.WriteLine(text);

                        }
                    }
                    streamReader.Close();
                    if (flag == false)
                    {
                        Log($"没有在{templateFile.Name}文件中找到{{0}}符号，替换复制失败");
                        break;
                    }
                }
            }

            Log("安装程序执行结束...");

        }

        static void Log(string message)
        {
            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            var logFile = Path.Combine(currentDirectory.FullName, "log-install.txt");
            File.AppendAllText(logFile, $"{DateTime.Now:yyyy-MM-dd HH.mm.ss} {message}\n");
        }
    }
}
