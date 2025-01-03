namespace AOC24;

public class InputScraperTests
{
    [Fact]
    public async Task GivenValidDayCanGetInput()
    {
        // Arrange
        var expected = await File.ReadAllTextAsync("./Resources/input.txt");
        expected = expected.Replace("\r", "");
        
        var scraper = new InputScraper();
        
        // Act
        var actual = await scraper.GetInput(14);
        
        // Assert
        Assert.Equal(expected,actual);
    }
}