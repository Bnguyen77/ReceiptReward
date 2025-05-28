
# ReceiptReward

**ReceiptReward** is a stand-alone .NET 8 Web API service that calculates reward points based on submitted digital receipts. Its functionality is dynamically configurable using a YAML file (`config.yml`) to support flexible, pluggable reward calculation rules.

## Overview

- Built with ASP.NET Core (.NET 8)
- Rule-based reward calculation, configured via YAML
- Interactive Swagger UI for manual testing
- Dockerized for easy deployment
- Test mode for validating calculation rules

## How to Run

1. Clone the repo
```bash
git clone https://github.com/Bnguyen77/ReceiptReward.git
```
2. Check environment in config.yml:
```yaml
environment: production
```
3. Build and run Docker in project root dir:
```bash
docker build -t receipt-reward .
docker run --rm -p 8080:8080 receipt-reward
```
4. Run with Swagger on:
```
http://localhost:8080/swagger/index.html
```

## Configuration-Driven Rule Engine
 
Located in `config.yml`:

```yaml
version: V01

environment: production

calculations:
  V01:
    - RetailerAlphanumeric
    - TotalRoundDollar
    - TotalMultipleQuarter
    - ItemPairs
    - ItemDesc
    - OddPurchaseDate
    - AfternoonPurchaseTime

test:
  version: v01
  path: Tests/TestCases
```

## Adding or Removing Rules

To add a new calculation rule, follow these steps:

1. **Add Enum Value**  
In `CalculatorStep.Enum.cs`:
```csharp
Add3Point
```

2. **Update the Interface**  
In `IRewardCalculator.cs`:
```csharp
int Add3Point();
```

3. **Implement the Rule**  
In `RewardCalculator.cs`:
```csharp
public int Add3Point()
{
    return 3;
}
```

4. **Wire it Up in the Orchestrator**  
In `RewardOrchestrator.cs`:
```csharp
CalculationStep.Add3Point => _calculator.Add3Point(),
```

✅ **Now Enable It in `config.yml`**  
```yaml
calculations:
  V01:
    - Add3Point
```

You can now enable or disable this rule without touching any code, simply by modifying `config.yml`.
