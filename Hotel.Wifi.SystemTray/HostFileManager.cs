using System.Collections.Generic;
using System.IO;

namespace Hotel.Wifi.SystemTray
{
    public class HostFileManager
    {
        //private static readonly string HostsFile = @"\usr\tmp\Hosts";
        private static readonly string HostsFile = @"\Windows\System32\Drivers\Etc\Hosts";
        private static readonly string HostsFileTemp = HostsFile + "-tmp";
        private static readonly string HostsBackup = HostsFile + ".BAK";

        public string HostsFileName { get { return HostsFile; } }

        private List<string> lines;

        public List<string> ReadFile()
        {
            lines = new List<string>(File.ReadAllLines(HostsFile));
            return lines;
        }

        public void ReplaceFile(List<string> newLines)
        {
            File.WriteAllLines(HostsFileTemp, newLines);
            CopySecurityInformation(HostsFile, HostsFileTemp);
            File.Replace(HostsFileTemp, HostsFile, HostsBackup);
        }

        public void SecureFile()
        {
            CopySecurityInformation(HostsFile, HostsFileTemp);
        }

        // From http://stackoverflow.com/questions/3118439/how-to-copy-ntfs-permissions
        //private static void CopySecurityInformation(String source, String dest)
        //{
        //    FileSecurity fileSecurity = File.GetAccessControl(source, AccessControlSections.All);
        //    fileSecurity.SetAccessRuleProtection(true, true);  // from http://www.codekeep.net/snippets/1dc00f8c-b338-4760-aecb-024fe5009ed6.aspx
        //    File.SetAccessControl(dest, fileSecurity);
        //    FileAttributes fileAttributes = File.GetAttributes(source);
        //    File.SetAttributes(dest, fileAttributes);
        //}

        // From http://msdn.microsoft.com/en-us/library/system.io.file.setaccesscontrol.aspx
        private static void CopySecurityInformation(String source, String dest)
        {
            FileSecurity sourceFileSecurity = File.GetAccessControl(source, AccessControlSections.All);
            FileSecurity destFileSecurity = new FileSecurity();
            string sourceDescriptor = sourceFileSecurity.GetSecurityDescriptorSddlForm(AccessControlSections.All);
            destFileSecurity.SetSecurityDescriptorSddlForm(sourceDescriptor);
            File.SetAccessControl(dest, sourceFileSecurity);

            FileAttributes fileAttributes = File.GetAttributes(source);
            File.SetAttributes(dest, fileAttributes);
        }

    }
}