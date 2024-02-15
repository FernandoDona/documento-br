## Bibliotecas com documentos brasileiros como CPF, CNPJ com suas respectivas validações e formatadores.

</div>

#### São 3 bibliotecas contidas neste repositório:
##### [Documento.BR.Extensions.FluentValidation](#documentobrextensionsfluentvalidation)
Uma biblioteca que extende o FluentValidation para adicionar validação de documentos brasileiros.
##### Documento.BR.Rules
É o core deste repositório. Contem as classes de validação e formatação dos documentos.
##### Documento.BR.Models
Uma tentativa de criar value types dos documentos.

## Documento.BR.Extensions.FluentValidation

<a href="https://www.nuget.org/packages/Documento.BR.Extensions.FluentValidation"><img alt="Nuget" src="https://img.shields.io/nuget/v/Documento.BR.Extensions.FluentValidation"></a>

`dotnet add package Documento.BR.Extensions.FluentValidation --version 1.0.0`




### Como funciona
Vamos tomar como exemplo uma validação de `CNPJ`, porém o mesmo padrão se aplica aos outros documentos.
```csharp
RuleFor(person => person.CNPJString).ValidateCNPJ();
```
Será feita a validação em cima do campo CNPJString do objeto person onde em caso de erro será retornado uma mensagem e um código de erro.
O código do erro será `InvalidCNPJ` e a mensagem será de acordo com o nome da propriedade e valor testados. Caso a UI esteja configurado com a cultura `pt-BR` a mensagem será retornada em português, caso contrário será retornada em inglês.
Exemplo:
```csharp
public static IRuleBuilderOptions<T, string?> ValidateCNPJ<T>(this IRuleBuilderInitial<T, string?> ruleBuilder)
{
    return ruleBuilder.Must(input => CNPJValidator.Validate(input))
        .WithMessage(IsBrazillianCulture() ? "{PropertyName} {PropertyValue} não é válido." : "{PropertyName} {PropertyValue} is not valid.")
        .WithErrorCode("InvalidCNPJ"); ;
}
```
