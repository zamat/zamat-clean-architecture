using AUMS.Common.ActionLinks.Consts;

namespace AUMS.Common.ActionLinks.Concrete;

/// <summary>
/// Action launch parameters for the Deregulation Tree
/// </summary>
public class ActionLinkDeregulationTreeItem : ActionLinkItem
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="itemId">Parameter for action (e.g. id, guid, number)</param>
    public ActionLinkDeregulationTreeItem(string itemId)
    {
        ItemGuid = ActionLinkItemGuid.ActionLinkItemDeregulationTreeGuid;
        ItemId = itemId;
    }
}
