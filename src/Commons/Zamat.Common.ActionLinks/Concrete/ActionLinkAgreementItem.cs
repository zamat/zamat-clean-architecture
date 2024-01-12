using AUMS.Common.ActionLinks.Consts;

namespace AUMS.Common.ActionLinks.Concrete;

/// <summary>
/// Parameters of action launch for Contracts
/// </summary>
public class ActionLinkAgreementItem : ActionLinkItem
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="itemId">Parametr for action (e.g. id, guid, number)</param>
    public ActionLinkAgreementItem(string itemId)
    {
        ItemGuid = ActionLinkItemGuid.ActionLinkItemAgreementGuid;
        ItemId = itemId;
    }
}
