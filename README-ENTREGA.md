# Lab 3 - Carrinho Shopee

Projeto ASP.NET Core MVC (.NET 8) com SQL Server, Entity Framework Core, Database First/Scaffold e carrinho em Session.

## 1. Banco de dados
No SQL Server Management Studio execute:

```sql
CREATE DATABASE dbShopee;
GO
USE dbShopee;
GO

CREATE TABLE Produto
(
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Preco DECIMAL(10,2) NOT NULL,
    Estoque INT NOT NULL
);
GO

INSERT INTO Produto (Nome, Preco, Estoque)
VALUES
('Mouse Gamer', 80.00, 20),
('Teclado Mecânico', 150.00, 15),
('Headset Gamer', 120.00, 10),
('Mouse Pad', 35.00, 30),
('Webcam Full HD', 200.00, 8);
GO
```

## 2. Conexão
Abra `appsettings.json` e substitua:
- SEU_SERVIDOR
- SEU_USUARIO
- SUA_SENHA

Exemplo com SQL Server Express:
`Server=.\SQLEXPRESS;Database=dbShopee;User Id=sa;Password=SUA_SENHA;TrustServerCertificate=True;`

Se usar Windows Authentication:
`Server=.\SQLEXPRESS;Database=dbShopee;Trusted_Connection=True;TrustServerCertificate=True;`

## 3. Restaurar e executar
No terminal na pasta do projeto:

```bash
dotnet restore
dotnet run
```

Ou abra `ShopeeMVC.sln`/o `.csproj` no Visual Studio e pressione Ctrl+F5.

## 4. Funcionalidades
- CRUD de Produto
- Listagem de produtos
- Adicionar ao carrinho
- Aumentar quantidade ao adicionar novamente
- Subtotal = Preço x Quantidade
- Total = soma dos subtotais
- Remover produto
- Limpar carrinho
- Session para manter o carrinho

## 5. Scaffold original
O projeto foi deixado funcional com as classes equivalentes ao resultado do Scaffold. Para gerar novamente a partir do banco:

```powershell
Scaffold-DbContext "Server=SEU_SERVIDOR;Database=dbShopee;User Id=SEU_USUARIO;Password=SUA_SENHA;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
```

O material da atividade usa .NET 8, SQL Server, EF Core e Database First.