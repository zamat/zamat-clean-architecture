using System;

namespace Zamat.Common.Multitenancy;

public class TenantNotFoundException(string? message) : Exception(message)
{
}
