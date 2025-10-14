using System.Text.RegularExpressions;
using Common.Utils.Cryptography.Argon2;
namespace Common.Test;

public class Argon2Test
{
    private string _salt;

    [SetUp]
    public void SetUp()
    {
        _salt = Argon2.GenerateRandomSecretBytes();
    }

    [Test]
    public void ValidateHashTest()
    {
        var text = "test";
        var hash = Argon2.GenerateHash(text, _salt);
        Assert.That(hash, Is.Not.Null);
    }

    [Test]
    public void ValidateVerifyTest()
    {
        var text = "test";
        var hash = Argon2.GenerateHash(text, _salt);
        var isVerified = Argon2.Verify(text, hash, _salt);
        Assert.That(isVerified, Is.True);
    }
}