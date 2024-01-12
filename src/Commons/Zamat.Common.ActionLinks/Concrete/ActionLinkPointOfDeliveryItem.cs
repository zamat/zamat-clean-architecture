using AUMS.Common.ActionLinks.Consts;

namespace AUMS.Common.ActionLinks.Concrete;

/// <summary>
/// Parameters of action launch for Collection Points
/// </summary>
public class ActionLinkPointOfDeliveryItem : ActionLinkItem
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="itemId">Parameter for action (e.g. id, guid, number)</param>
    public ActionLinkPointOfDeliveryItem(string itemId)
    {
        ItemGuid = ActionLinkItemGuid.ActionLinkItemPointOfDeliveryGuid;
        ItemId = itemId;
    }
}
