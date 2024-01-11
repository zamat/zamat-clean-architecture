namespace Zamat.Clean.Cli.SeedData;

internal class Seed
{
    public IEnumerable<SeedUser> Users { get; set; } = new List<SeedUser>();
}
