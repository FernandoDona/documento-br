using Documento.BR.Rules.Validators;

namespace Documento.BR.Rules.Tests;

public class CPFValidatorTests
{

    [Theory]
    [InlineData("162.s00.000-56")]
    [InlineData("888.888.888-88")]
    [InlineData("345.134.163-23")]
    [InlineData("32.598.657/0001-99")]
    [InlineData("345.134.163-2332")]
    [InlineData("2971719021")]
    public void Validate_InvalidCPF_ReturnsFalse(string input)
    {
        Assert.False(CPFValidator.Validate(input));
    }

    [Theory]
    [InlineData("020.776.020-90")]
    [InlineData("29717195021")]
    public void Validate_ValidCPF_ReturnsTrue(string input)
    {
        Assert.True(CPFValidator.Validate(input));
    }
}