using CSharpClicker.Dtos;

namespace CSharpClicker.ViewModels;

public class AdminViewModel
{
    public IEnumerable<BoostDto>? Boosts { get; init; }
}
