using System;

namespace AUMS.Common.Multitenancy;

public class TenantNotFoundException : Exception
{
    public TenantNotFoundException(string? message) : base(message)
    {
    }
}
