using Xunit;
using task2;

public class RectangleTests
{
    [Fact]
    public void Area_CorrectCalculation()
    {
        var rect = new Rectangle(3, 4);
        Assert.Equal(12, rect.Area);
    }

    [Fact]
    public void Perimeter_CorrectCalculation()
    {
        var rect = new Rectangle(3, 4);
        Assert.Equal(14, rect.Perimeter);
    }
}