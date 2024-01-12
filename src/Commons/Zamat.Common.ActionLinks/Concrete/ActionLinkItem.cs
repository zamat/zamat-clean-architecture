using System;
using AUMS.Common.ActionLinks.Security;
using Newtonsoft.Json;

namespace AUMS.Common.ActionLinks.Concrete;

/// <summary>
/// Class with parameters to trigger the action
/// </summary>
public class ActionLinkItem : IActionLinkItem
{
    /// <summary>
    /// Unambiguous Guid for action 
    /// </summary>
    public Guid ItemGuid { get; set; }

    /// <summary>
    /// Parameter for action (ex. id, guid, number)
    /// </summary>
    public string ItemId { get; set; }

    /// <summary>
    /// Creation (serialization) of encrypted ActionLink
    /// </summary>
    /// <returns></returns>
    public string ToActionLink()
    {
        var actionLinkSerialized = JsonConvert.SerializeObject(this);
        return Cryptography.Encrypt(actionLinkSerialized);
    }

    /// <summary>
    /// Creating an ActionLinkItem
    /// </summary>
    /// <param name="actionLink">Encrypted actionLink</param>
    /// <returns></returns>
    public static ActionLinkItem ToActionLinkItem(string actionLink)
    {
        string actionLinkDecrypted;
        ActionLinkItem actionLinkItem;

        try
        {
            actionLinkDecrypted = Cryptography.Decrypt(actionLink);
            if (actionLinkDecrypted == null)
            {
                throw new Exception($"Bad actionLink format :{Environment.NewLine}{actionLink}");
            }

            actionLinkItem = JsonConvert.DeserializeObject<ActionLinkItem>(actionLinkDecrypted);
            if (actionLinkItem == null)
            {
                throw new Exception($"Bad actionLink format :{Environment.NewLine}{actionLink}");
            }
        }
        catch (Exception e)
        {
            throw new Exception($"Bad actionLink format :{Environment.NewLine}{actionLink}", e);
        }

        return actionLinkItem;
    }
}
