Hi.
I'm sorry I did not have time to write an add product.



---Add migrations---
dotnet ef migrations add InitialModel --context categorycontext -p ../OrderProduct/Models/Infrastructure.csproj -s ../OrderProduct/OrderProduct/OrderProduct.csproj -o Data/Migrations


--Update database---
dotnet ef database update --context categorycontext -p ../OrderProduct/Models/Infrastructure.csproj -s ../OrderProduct/OrderProduct/OrderProduct.csproj