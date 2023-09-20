using Documento.BR.Rules.Formatters;

namespace Documento.BR.Models.Tests;

public class CPFTests
{
    [Theory]
    [InlineData("020.776.020-90")]
    [InlineData("29717195021")]
    public void Create_FromValidString_ReturnsNewInstanceWithFormattedDocument(string input)
    {
        CPF cpf = CPF.Create(input);
        Assert.Equal(CPFFormatter.Format(input), cpf.Value);
    }

    [Theory]
    [InlineData("345.134.163-23")]
    public void TryCreate_FromInvalidString_ReturnsEmptyDocument(string input)
    {
        CPF cpf = CPF.TryCreate(input);
        Assert.Equal(CPF.Empty, cpf);
    }

    [Theory]
    [InlineData("345.134.163-23")]
    public void Create_FromInvalidString_ThrowException(string input)
    {
        Assert.Throws<ArgumentException>(() => CPF.Create(input));
    }
}