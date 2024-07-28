# StarkInstaller
这是一个在Revit插件安装时快速复制多版本addin文件的工具。安装时，项目文件复制到用户文件夹后启动。卸载时，将进入检索revit的addin文件目录删除addin文件。
# 前置条件

StarkInstaller支持的Revit文件结构
# 项目文件夹结构
必要的文件结构以保证StarkInstaller正确执行。可以通过修改源代码调整。

```plaintext
yourproject/
├── src/
│   └── your files
└── installer/
    ├── Test.Revit.addin
    ├── Install.exe
    ├── Uninstall.exe
    ├── version.txt
    └── log-install.txt

```
# 使用方法
1. 修改Test.Revit.addin配置文件的名称、修改Name、Assembly、ClientId、FullClassName等内容。
2. 根据version.txt指定addin文件复制版本。version.txt文件内容示例如下
```
2020;2021;2022;2023
```
3. 在项目安装时指定启动Installer.exe文件，在项目卸载时指定启动Uninstaller.exe文件。
# 安装日志
在安装、删除addin文件时，将产生`Log-install.txt`日志文件。安装失败时可通过日志定位
```
2024-07-15 00.38.14 安装程序执行开始...
2024-07-15 00.38.16 复制Test.Revit.addin到2020文件夹，并替换启动路径为		<Assembly>D:\Github\Stark.Revit.Installer\src\share\2020\Test.Revit.Addin.dll</Assembly>
2024-07-15 00.38.28 复制Test.Revit.addin到2021文件夹，并替换启动路径为		<Assembly>D:\Github\Stark.Revit.Installer\src\share\2021\Test.Revit.Addin.dll</Assemb
2024-07-15 00.38.31 安装程序执行结束...
2024-07-15 00.39.01 卸载程序执行开始...
2024-07-15 00.39.01 删除**.Revit.addin文件
2024-07-15 00.39.01 删除**.Revit.addin文件
2024-07-15 00.39.01 卸载程序执行结束...
```
