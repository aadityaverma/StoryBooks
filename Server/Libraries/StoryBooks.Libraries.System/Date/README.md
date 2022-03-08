## Date time mocking

```
UtcDateTime.Instance.Now();									//3/8/2022         
UtcDateTime.SetOverride(d => new DateTime(2000, 1, 1));
UtcDateTime.Instance.Now();									//1/1/2000
UtcDateTime.ResetToNormal();
UtcDateTime.Instance.Now();									//3/8/2022
```