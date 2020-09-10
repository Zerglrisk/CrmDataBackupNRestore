using System;
using System.Management;

namespace Core.Definition
{
    public class HardwareInfoGetter
    {
        public static string GetUUID()
        {
            try
            {
                var uuid = string.Empty;

                var mc = new ManagementClass("Win32_ComputerSystemProduct");
                var moc = mc.GetInstances();

                foreach (var o in moc)
                {
                    var mo = (ManagementObject)o;
                    uuid = mo.Properties["UUID"].Value.ToString();
                    break;
                }

                return uuid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
