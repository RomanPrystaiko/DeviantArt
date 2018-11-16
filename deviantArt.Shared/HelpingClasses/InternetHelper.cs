using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace deviantArt.Shared.HelpingClasses
{
    public static class InternetHelper
    {
        /// <summary>
        /// Returns true if internet access is available.
        /// </summary>
        /// <returns></returns>
        public static bool CheckInternetConnection()
        {
            bool isInternetConnected = NetworkInterface.GetIsNetworkAvailable();
            return isInternetConnected;
        }
    }
}
