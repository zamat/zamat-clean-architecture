using Xunit;

namespace AUMS.Common.FilterQuery.UnitTests;

public class FilterQueryTests
{
    [Fact]
    public void CreateValidQueryString()
    {
        var filter = new FilterQuery<FilterItem>(SortEnum.FirstParamDesc);
        filter.Filter(x => x.FirstParam == "exampleValue");

        var queryParams = filter.QueryParams;
        var queryString = queryParams.ConvertToQueryString();

        Assert.Equal("?FirstParam=exampleValue", queryString);
    }
}
