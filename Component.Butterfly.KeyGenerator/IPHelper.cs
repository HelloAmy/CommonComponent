using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace CommonComponent.Butterfly.KeyGenerator
{
    public class IPHelper
    {
        /// <summary>
        /// 获取本机IPV4地址
        /// </summary>
        /// <returns>地址</returns>
        public static List<string> GetLocalIPV4List()
        {
            try
            {
                string hostName = Dns.GetHostName();//本机名   
                System.Net.IPAddress[] addressList = Dns.GetHostAddresses(hostName);
                if (addressList != null)
                {
                    List<string> ipv4 = (from p in addressList
                                         where p.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                                         select p.ToString()).ToList<string>();


                    return ipv4;
                }
            }
            catch
            {
            }

            return new List<string>();
        }

        /// <summary>
        /// 获取本机IPV6地址
        /// </summary>
        /// <returns>IPV6地址</returns>
        public static List<string> GetLocalIpV6List()
        {
            try
            {
                string hostName = Dns.GetHostName();
                System.Net.IPAddress[] addressList = Dns.GetHostAddresses(hostName);
                if (addressList != null)
                {

                    List<string> ipv6List = (from p in addressList
                                             where p.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6
                                             select p.ToString()).ToList<string>();
                    return ipv6List;
                }
            }
            catch
            {
            }
            return new List<string>();
        }
    }
}
