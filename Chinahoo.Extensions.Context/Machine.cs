using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Chinahoo.Extensions.Context
{
    /// <summary>
    /// 服务器端
    /// </summary>
    public class Machine
    {
        /// <summary>
        /// 获取mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress(string separator = "-")
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            if (nics == null || nics.Length < 1)
            {
                return "";
            }
            foreach (NetworkInterface adapter in nics.Where(c =>
             c.NetworkInterfaceType != NetworkInterfaceType.Loopback && c.OperationalStatus == OperationalStatus.Up))
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();

                var unicastAddresses = properties.UnicastAddresses;
                if (unicastAddresses.Any(temp => temp.Address.AddressFamily == AddressFamily.InterNetwork))
                {
                    var address = adapter.GetPhysicalAddress();
                    if (string.IsNullOrEmpty(separator))
                    {
                        return address.ToString();
                    }
                    else
                    {
                        return string.Join(separator, address.GetAddressBytes().Select(x => ((int)x).ToString("X")));
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 获取机器名称
        /// </summary>
        /// <returns></returns>
        public static string MachineName()
        {
            return Environment.MachineName;
        }
    }
}
