using System;
using System.Runtime.InteropServices;
using System.Security;

namespace HappiNESs
{
    /// <summary>
    /// Helpers for the <see cref="SecureString"/> class
    /// </summary>
    public static class SecureStringHelpers
    {
        /// <summary>
        /// Unsecure a <see cref="SecureString"/> to plain text
        /// </summary>
        /// <param name="secureString"></param>
        /// <returns></returns>
        public static string Unsecure(this SecureString secureString)
        {
            // Make sure we have a secure string
            if (secureString == null)
                return string.Empty;

            // Get a pointer for an unsecure string in memory
            var unmanagedString = IntPtr.Zero;

            try
            {
                // Unsecures the Password
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                // Clean up any memory allocation
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        /// <summary>
        /// Converts a string into a SecureString
        /// </summary>
        /// <param name="String">The string to be converted</param>
        /// <returns></returns>
        public static SecureString GetFromString(string String)
        {
            var newSecure = new SecureString();
            foreach (var c in String)
            {
                newSecure.AppendChar(c);
            }
            return newSecure;
        }
    }
}
