This repo is intended to reproduce OData WebApi issue #[2106](https://github.com/OData/WebApi/issues/2106) or alternatively provide a working solution or workaround for it

### Fails with EF Core repro
```
http://localhost:6789/odata/Orders?$expand=OrderItems($expand=Product)
```

### Works with EF6 repro
```
http://localhost:13059/odata/Orders?$expand=OrderItems($expand=Product)
```
Works even for expansion depth of 4
```
http://localhost:13059/odata/Customers?$expand=Orders($expand=OrderItems($expand=Product($expand=Category)))
```

### Fixed in EF Core 6.0
```
http://localhost:5104/odata/Orders?$expand=OrderItems($expand=Product)
```
Repro project: **ODataWebApiIssue2106Repro.Net6**

Note: Run migration scripts from **ODataWebApiIssue2106Repro** to create the database (if necessary)