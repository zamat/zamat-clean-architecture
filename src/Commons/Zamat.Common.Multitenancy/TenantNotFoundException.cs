using System;

namespace Zamat.Common.Multitenancy;

public class TenantNotFoundException : Exception
{
    public TenantNotFoundException(string? message) : base(message)
    {
    }
}
