using System;

namespace AUMS.Common.ActionLinks;

/// <summary>
/// Interface with parameters to trigger the action
/// </summary>
public interface IActionLinkItem
{
    /// <summary>
    ///  Unambiguous Guid for actions (e.g., in the aums menu)
    /// </summary>
    Guid ItemGuid { get; set; }

    /// <summary>
    ///Parameter for the action (e.g. id, guid, number)
    /// </summary>
    string ItemId { get; set; }

    /// <summary>
    /// Creation (serialization) of encrypted ActionLink 
    /// </summary>
    /// <returns></returns>
    string ToActionLink();
}
