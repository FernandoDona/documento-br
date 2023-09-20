using Documento.BR.Rules.Formatters;

namespace Documento.BR.Rules.Tests;

public class CPFFormatterTests

{
    [Theory]
    [InlineData("29717195021", "297.171.950-21")]
    public void Format_ValidCPF_ReturnsFormattedCPF(string input, string formattedInput)
    {
        Assert.Equal(formattedInput, CPFFormatter.Format(input));
    }

    [Theory]
    [InlineData("297.171.950-21", "29717195021")]
    public void Unformat_ValidCPF_ReturnsOnlyDigits(string input, string unformattedInput)
    {
        Assert.Equal(unformattedInput, CPFFormatter.Unformat(input));
    }

    [Theory]
    [InlineData("34513416323")]
    public void Format_InvalidCPF_ThowException(string input)
    {
        Assert.Throws<ArgumentException>(() => CPFFormatter.Format(input));
    }
}