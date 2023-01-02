using System.Collections.Generic;

namespace Zamat.Sample.Cli.SeedData;

class Seed
{
    public IEnumerable<SeedUser> Users { get; set; } = new List<SeedUser>();
}
