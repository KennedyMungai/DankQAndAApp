namespace BackendTests;

public class CalcTests
{
    [Fact]
    public void Add_When2Integers_ShouldReturnCorrectInteger()
    {
        // Given
        int a =1;
        int b=1;
        // When
        var result = Calc.Add(a, b);
        // Then
        Assert.Equal(2, result);
    }
}