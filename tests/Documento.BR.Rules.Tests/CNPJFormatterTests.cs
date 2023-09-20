using Documento.BR.Rules.Formatters;

namespace Documento.BR.Rules.Tests;

public class CNPJFormatterTests

{
    [Theory]
    [InlineData("32598657000199", "32.598.657/0001-99")]
    public void Format_ValidCNPJ_ReturnsFormattedCNPJ(string input, string formattedInput)
    {
        Assert.Equal(formattedInput, CNPJFormatter.Format(input));
    }

    [Theory]
    [InlineData("89.491.969/0001-16", "89491969000116")]
    public void Unformat_ValidCNPJ_ReturnsOnlyDigits(string input, string unformattedInput)
    {
        Assert.Equal(unformattedInput, CNPJFormatter.Unformat(input));
    }

    [Theory]
    [InlineData("86456345000214")]
    public void Format_InvalidCNPJ_ThowException(string input)
    {
        Assert.Throws<ArgumentException>(() => CNPJFormatter.Format(input));
    }
}