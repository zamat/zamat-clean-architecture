﻿using System;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Zamat.AspNetCore.Diagnostics.HealthChecks;

record HealthCheckEntryResponse(HealthStatus Status, TimeSpan Duration, string? Description)
{
    internal HealthCheckEntryResponse(HealthReportEntry entry) : this(entry.Status, entry.Duration, entry.Description)
    {
    }
}
