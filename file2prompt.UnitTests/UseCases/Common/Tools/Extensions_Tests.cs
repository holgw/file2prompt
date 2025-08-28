using file2prompt.Core.UseCases.Common.Tools;

namespace file2prompt.UnitTests.UseCases.Common.Tools;

[TestFixture]
public class ExtensionsTests
{
    [Test]
    public void InjectContent_WhenPromptIsNull_ShouldReturnContent()
    {
        // Arrange
        string? prompt = null;
        string content = "test content";

        // Act
        var result = prompt.InjectContent(content);

        // Assert
        Assert.That(result, Is.EqualTo(content));
    }

    [Test]
    public void InjectContent_WhenPromptIsEmpty_ShouldReturnContent()
    {
        // Arrange
        string prompt = "";
        string content = "test content";

        // Act
        var result = prompt.InjectContent(content);

        // Assert
        Assert.That(result, Is.EqualTo(content));
    }

    [Test]
    public void InjectContent_WhenPromptHasNoTag_ShouldAppendContent()
    {
        // Arrange
        string prompt = "Hello world";
        string content = "test content";

        // Act
        var result = prompt.InjectContent(content);

        // Assert
        Assert.That(result, Is.EqualTo($"Hello world{Environment.NewLine}test content"));
    }

    [Test]
    public void InjectContent_WhenPromptHasTag_ShouldReplaceTag()
    {
        // Arrange
        string prompt = "Hello [BODY]";
        string content = "test content";

        // Act
        var result = prompt.InjectContent(content);

        // Assert
        Assert.That(result, Is.EqualTo("Hello test content"));
    }

    [Test]
    public void InjectContent_WhenPromptHasTagWithDifferentCase_ShouldReplaceTag()
    {
        // Arrange
        string prompt = "Hello [bOdY]";
        string content = "test content";

        // Act
        var result = prompt.InjectContent(content);

        // Assert
        Assert.That(result, Is.EqualTo("Hello test content"));
    }

    [Test]
    public void InjectContent_WhenPromptHasMultipleTags_ShouldReplaceFirstTag()
    {
        // Arrange
        string prompt = "Hello [BODY] and [BODY]";
        string content = "test content";

        // Act
        var result = prompt.InjectContent(content);

        // Assert
        Assert.That(result, Is.EqualTo("Hello test content and test content"));
    }

    [Test]
    public void InjectContent_WhenPromptHasOnlyTag_ShouldReturnJustContent()
    {
        // Arrange
        string prompt = "[BODY]";
        string content = "test content";

        // Act
        var result = prompt.InjectContent(content);

        // Assert
        Assert.That(result, Is.EqualTo("test content"));
    }

    [Test]
    public void ExtractContent_WhenSourceIsNull_ShouldReturnEmptyString()
    {
        // Arrange
        string? source = null;
        string startTag = "start";
        string endTag = "end";

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void ExtractContent_WhenSourceIsEmpty_ShouldReturnEmptyString()
    {
        // Arrange
        string source = "";
        string startTag = "start";
        string endTag = "end";

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void ExtractContent_WhenStartTagIsNull_ShouldStartFromBeginning()
    {
        // Arrange
        string source = "Hello world end";
        string? startTag = null;
        string endTag = "end";

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo("Hello world "));
    }

    [Test]
    public void ExtractContent_WhenStartTagIsEmpty_ShouldStartFromBeginning()
    {
        // Arrange
        string source = "Hello world end";
        string startTag = "";
        string endTag = "end";

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo("Hello world "));
    }

    [Test]
    public void ExtractContent_WhenStartTagNotFound_ShouldStartFromBeginning()
    {
        // Arrange
        string source = "Hello world end";
        string startTag = "notfound";
        string endTag = "end";

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo("Hello world "));
    }

    [Test]
    public void ExtractContent_WhenEndTagIsNull_ShouldExtractToEnd()
    {
        // Arrange
        string source = "start Hello world";
        string startTag = "start";
        string? endTag = null;

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo(" Hello world"));
    }

    [Test]
    public void ExtractContent_WhenEndTagIsEmpty_ShouldExtractToEnd()
    {
        // Arrange
        string source = "start Hello world";
        string startTag = "start";
        string endTag = "";

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo(" Hello world"));
    }

    [Test]
    public void ExtractContent_WhenEndTagNotFound_ShouldExtractToEnd()
    {
        // Arrange
        string source = "start Hello world";
        string startTag = "start";
        string endTag = "notfound";

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo(" Hello world"));
    }

    [Test]
    public void ExtractContent_WhenBothTagsAreNull_ShouldExtractToEnd()
    {
        // Arrange
        string source = "Hello world";
        string? startTag = null;
        string? endTag = null;

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo("Hello world"));
    }

    [Test]
    public void ExtractContent_WhenBothTagsAreEmpty_ShouldExtractToEnd()
    {
        // Arrange
        string source = "Hello world";
        string startTag = "";
        string endTag = "";

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo("Hello world"));
    }

    [Test]
    public void ExtractContent_WhenStartAndEndTagsFound_ShouldExtractBetween()
    {
        // Arrange
        string source = "start Hello world end";
        string startTag = "start";
        string endTag = "end";

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo(" Hello world "));
    }

    [Test]
    public void ExtractContent_WhenStartAndEndTagsWithCaseDifference_ShouldExtractBetween()
    {
        // Arrange
        string source = "START Hello world END";
        string startTag = "start";
        string endTag = "end";

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo(" Hello world "));
    }

    [Test]
    public void ExtractContent_WhenStartTagAtBeginning_ShouldExtractFromAfterStart()
    {
        // Arrange
        string source = "start Hello world end";
        string startTag = "start";
        string endTag = "end";

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo(" Hello world "));
    }

    [Test]
    public void ExtractContent_WhenStartTagAtEnd_ShouldExtractFromEnd()
    {
        // Arrange
        string source = "Hello world start";
        string startTag = "start";
        string endTag = "end";

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void ExtractContent_WhenStartTagAtEndAndNoEndTag_ShouldExtractToEnd()
    {
        // Arrange
        string source = "Hello world start";
        string startTag = "start";
        string? endTag = null;

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void ExtractContent_WhenStartAndEndTagsAreSame_ShouldExtractBetween()
    {
        // Arrange
        string source = "start Hello world start";
        string startTag = "start";
        string endTag = "start";

        // Act
        var result = source.ExtractContent(startTag, endTag);

        // Assert
        Assert.That(result, Is.EqualTo(" Hello world "));
    }
}