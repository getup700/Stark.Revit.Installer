# StarkInstaller
����һ����Revit�����װʱ���ٸ��ƶ�汾addin�ļ��Ĺ��ߡ���װʱ����Ŀ�ļ����Ƶ��û��ļ��к�������ж��ʱ�����������revit��addin�ļ�Ŀ¼ɾ��addin�ļ���
# ǰ������

StarkInstaller֧�ֵ�Revit�ļ��ṹ
# ��Ŀ�ļ��нṹ
��Ҫ���ļ��ṹ�Ա�֤StarkInstaller��ȷִ�С�����ͨ���޸�Դ���������

```plaintext
yourproject/
������ src/
��   ������ your files
������ installer/
    ������ Test.Revit.addin
    ������ Install.exe
    ������ Uninstall.exe
    ������ version.txt
    ������ log-install.txt

```
# ʹ�÷���
1. �޸�Test.Revit.addin�����ļ������ơ��޸�Name��Assembly��ClientId��FullClassName�����ݡ�
2. ����version.txtָ��addin�ļ����ư汾��version.txt�ļ�����ʾ������
```
2020;2021;2022;2023
```
3. ����Ŀ��װʱָ������Installer.exe�ļ�������Ŀж��ʱָ������Uninstaller.exe�ļ���
# ��װ��־
�ڰ�װ��ɾ��addin�ļ�ʱ��������`Log-install.txt`��־�ļ�����װʧ��ʱ��ͨ����־��λ
```
2024-07-15 00.38.14 ��װ����ִ�п�ʼ...
2024-07-15 00.38.16 ����Test.Revit.addin��2020�ļ��У����滻����·��Ϊ		<Assembly>D:\Github\Stark.Revit.Installer\src\share\2020\Test.Revit.Addin.dll</Assembly>
2024-07-15 00.38.28 ����Test.Revit.addin��2021�ļ��У����滻����·��Ϊ		<Assembly>D:\Github\Stark.Revit.Installer\src\share\2021\Test.Revit.Addin.dll</Assemb
2024-07-15 00.38.31 ��װ����ִ�н���...
2024-07-15 00.39.01 ж�س���ִ�п�ʼ...
2024-07-15 00.39.01 ɾ��**.Revit.addin�ļ�
2024-07-15 00.39.01 ɾ��**.Revit.addin�ļ�
2024-07-15 00.39.01 ж�س���ִ�н���...
```
