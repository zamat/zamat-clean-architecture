using System.Text;

namespace Zamat.Common.FilterQuery;

public class QueryParams : List<QueryParam>
{
    public string ConvertToQueryString()
    {
        var stringBuilder = new StringBuilder("?");
        foreach (QueryParam queryParam in this)
        {
            stringBuilder.AppendJoin("&", $"{queryParam.Field}={queryParam.Value}");
        }
        return stringBuilder.ToString();
    }
}
