using System;
using System.Collections.Generic;
using System.Text;

namespace AUMS.Common.ActionLinks.Security;

/// <summary>
/// Class responsible for ActionLink encryption/decryption
/// </summary>
internal class Cryptography
{
    /// <summary>
    /// Data encryption
    /// </summary>
    /// <param name="actionLinkItem">Serialized actionLinkItem</param>
    /// <returns></returns>
    internal static string Encrypt(string actionLinkItem)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(actionLinkItem);
        return Convert.ToBase64String(plainTextBytes);
    }

    /// <summary>
    /// Data decryption
    /// </summary>
    /// <param name="actionLink">Encrypted actionLink</param>
    /// <returns></returns>
    internal static string Decrypt(string actionLink)
    {
        var base64EncodedBytes = Convert.FromBase64String(actionLink);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }
}
