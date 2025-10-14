using System.Text.RegularExpressions;
namespace Common.Test;

public class Tests
{


    [Test]
    [TestCase("abcdefgh")]
    [TestCase("abcdefgh1")]
    [TestCase("abcd.efgh1")]
    [TestCase("abcd.efA")]
    [TestCase("asdf.s")]
    public void ValidateMinLength(string text)
    {
        var isMatch = Regex.IsMatch(text, pattern: ".{8}");
        Assert.That(isMatch, Is.True);
    }

    [Test]
    [TestCase("asdfasdfasdfasdfasdf")]
    [TestCase("..123456dasd78 ")]
    [TestCase(".123456dasd78 ")]
    [TestCase("abcdefgh1")]
    [TestCase("abcd.efgh1")]
    [TestCase("abcd.efA")]
    public void ValidateMaxLength(string text)
    {
        var isMatch = Regex.IsMatch(text, pattern: "^.{0,14}$");
        Assert.That(isMatch, Is.True);
    }

    [Test]
    [TestCase("abc")]
    [TestCase("a1")]
    [TestCase("12")]
    [TestCase(" ")]
    [TestCase("@")]
    public void ValidateHasLetter(string text)
    {
        var isMatch = Regex.IsMatch(text, pattern: "[a-z]");
        Assert.That(isMatch, Is.True);
    }

    [Test]
    [TestCase("abc")]
    [TestCase("a1")]
    [TestCase("12")]
    [TestCase(" ")]
    [TestCase("@")]
    public void ValidateHasNumber(string text)
    {
        var isMatch = Regex.IsMatch(text, pattern: "[0-9]");
        Assert.That(isMatch, Is.True);
    }

    [Test]
    [TestCase("abc")]
    [TestCase("Abc")]
    [TestCase("AbB")]
    [TestCase("A")]
    [TestCase("A1")]
    [TestCase("a1")]
    [TestCase("12")]
    [TestCase(" ")]
    [TestCase("@")]
    public void ValidateHasUpperLetter(string text)
    {
        var isMatch = Regex.IsMatch(text, pattern: "[A-Z]");
        Assert.That(isMatch, Is.True);
    }

    [Test]
    [TestCase("!")]
    [TestCase("@")]
    [TestCase("#")]
    [TestCase("/")]
    [TestCase("$")]
    [TestCase("&")]
    [TestCase("*")]
    [TestCase("~")]
    [TestCase(" ")]
    [TestCase("a")]
    [TestCase("1")]
    public void ValidateHasSymbol(string text)
    {
        var isMatch = Regex.IsMatch(text, pattern: "[!@#/$&*~]");
        Assert.That(isMatch, Is.True);
    }

    [Test]
    [TestCase("")]
    [TestCase("1")]
    [TestCase("1a ")]
    [TestCase(" 1a")]
    [TestCase(" 1 a")]
    public void ValidateHasWhiteSpaces(string text)
    {
        var isMatch = Regex.IsMatch(text, pattern: "^\\S*$");
        Assert.That(isMatch, Is.True);
    }
}