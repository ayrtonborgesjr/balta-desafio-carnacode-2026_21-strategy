# Testes Unitários - CalculoFrete

Este projeto contém testes unitários completos para todas as classes do projeto CalculoFrete.Console.

## Estrutura dos Testes

### 1. **CorreiosStrategyTests.cs** (9 testes)
Testa a estratégia dos Correios:
- ✅ Nome da estratégia
- ✅ Disponibilidade (sempre disponível)
- ✅ Cálculo de frete básico
- ✅ Taxa express
- ✅ Desconto para mesmo estado
- ✅ Combinação de desconto e taxa express
- ✅ Prazo de entrega express (3 dias)
- ✅ Prazo de entrega padrão (7 dias)

### 2. **FedExStrategyTests.cs** (11 testes)
Testa a estratégia da FedEx:
- ✅ Nome da estratégia
- ✅ Disponibilidade para peso ≤ 50kg
- ✅ Indisponibilidade para peso > 50kg
- ✅ Cálculo de frete básico
- ✅ Fator multiplicador express (1.8x)
- ✅ Taxa adicional para região Norte
- ✅ Taxa adicional para região Nordeste
- ✅ Combinação de taxas express e regional
- ✅ Prazo de entrega express (2 dias)
- ✅ Prazo de entrega padrão (5 dias)

### 3. **DHLStrategyTests.cs** (9 testes)
Testa a estratégia da DHL:
- ✅ Nome da estratégia
- ✅ Disponibilidade para peso ≤ 50kg
- ✅ Indisponibilidade para peso > 50kg
- ✅ Cálculo de frete para peso leve (≤ 10kg)
- ✅ Taxa extra para peso > 10kg
- ✅ Taxa express (+R$ 35,00)
- ✅ Combinação de todas as taxas
- ✅ Prazo de entrega express (1 dia)
- ✅ Prazo de entrega padrão (4 dias)

### 4. **LocalCarrierStrategyTests.cs** (9 testes)
Testa a estratégia da Transportadora Local:
- ✅ Nome da estratégia
- ✅ Disponibilidade para São Paulo-SP
- ✅ Indisponibilidade para outros estados
- ✅ Cálculo de frete para São Paulo
- ✅ Retorno 0 para outros estados
- ✅ Cálculo para diferentes pesos
- ✅ Prazo de entrega (sempre 1 dia)
- ✅ Prazo independente de express

### 5. **ShippingCalculatorTests.cs** (11 testes)
Testa o contexto ShippingCalculator:
- ✅ Definição de estratégia
- ✅ Erro quando nenhuma estratégia é definida
- ✅ Erro quando estratégia não está disponível
- ✅ Cálculo correto para Correios
- ✅ Cálculo correto para FedEx
- ✅ Cálculo correto para DHL
- ✅ Cálculo correto para Transportadora Local
- ✅ Troca de estratégia em runtime
- ✅ Validação do limite de peso da FedEx

### 6. **ShippingInfoTests.cs** (5 testes)
Testa o modelo ShippingInfo:
- ✅ Criação de instância com propriedades required
- ✅ Propriedades podem ser alteradas
- ✅ IsExpress default é false
- ✅ Weight aceita valores decimais
- ✅ Weight aceita zero

## Resumo

- **Total de testes**: 49
- **Testes bem-sucedidos**: 49 ✅
- **Testes falhados**: 0
- **Cobertura**: Todas as classes do projeto Console

## Como Executar

```powershell
# Compilar o projeto de testes
dotnet build

# Executar todos os testes
dotnet test

# Executar com verbosidade detalhada
dotnet test --verbosity normal
```

## Tecnologias Utilizadas

- **xUnit**: Framework de testes unitários
- **.NET 9.0**: Framework
- **StringWriter**: Para capturar saída do console nos testes do ShippingCalculator

## Padrões Aplicados

- **Arrange-Act-Assert (AAA)**: Estrutura clara de testes
- **Testes independentes**: Cada teste é isolado e não depende de outros
- **Nomenclatura descritiva**: Nomes de testes indicam claramente o que está sendo testado
- **IDisposable**: Implementado no ShippingCalculatorTests para cleanup adequado

