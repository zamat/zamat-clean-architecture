﻿using System.Threading.Tasks;

namespace Zamat.Common.Multitenancy;

public interface ITenantStore
{
    Task<Tenant?> GetTenantAsync(string tenantIdentifier);
}
