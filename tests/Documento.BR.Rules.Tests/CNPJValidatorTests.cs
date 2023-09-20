using Documento.BR.Rules.Validators;

namespace Documento.BR.Rules.Tests;

public class CNPJValidatorTests
{

    [Theory]
    [InlineData("43.276.153/0001-03")]
    [InlineData("22.222.222/2222-22")]
    [InlineData("86.x56.345/0002-14")]
    [InlineData("297.171.950-21")]
    [InlineData("96934594562194")]
    public void Validate_InvalidCNPJ_ReturnsFalse(string input)
    {
        Assert.False(CNPJValidator.Validate(input));
    }

    [Theory]
    [InlineData("89.491.969/0001-16")]
    [InlineData("89491969000116")]
    public void Validate_ValidCNPJ_ReturnsTrue(string input)
    {
        Assert.True(CNPJValidator.Validate(input));
    }
}