using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management; //
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using WebAppUser.Data;
using WebAppUser.Models;
using WebAppUser.ViewModels;

namespace WebAppUser.Helpers
{
    public static class ActivityLogHelper
    {

        //var ip = HttpContext.Request.Host;
        // var remoteIpAddress = Request.Host.Host;
        public static string GetIPClient()
        {
            IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
            return heserver.AddressList.ToList().Where(p => p.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault().ToString();
        }

        public static string GetNameDeviceClient()
        {
            return Dns.GetHostName();
        }

        public static string GetMacAdreessDeviceClient()
        {
            IPAddress[] IP = Dns.GetHostAddresses(Dns.GetHostName());
            return IP[1].ToString();   //Mac Address
           //return IP[1].ToString();  //IP Address
        }
    }
}
