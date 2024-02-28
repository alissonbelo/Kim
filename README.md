# Projeto Kim

Este é um projeto em C# que utiliza o framework .NET e o SQL Server para armazenamento de dados. Ele inclui uma configuração básica para a conexão com o banco de dados e a criação de uma tabela chamada "Kim".

## Configuração da Conexão com o Banco de Dados

Para configurar a conexão com o banco de dados SQL Server, siga estas etapas:

1. Abra o arquivo `appsettings.json`.
2. Localize a seção `<connectionStrings>` e insira as informações de conexão do seu banco de dados. Aqui está um exemplo:

```xml
  "ConnectionStrings": {
    "SqlServer" : "Server=localhost;Database=kim;Trusted_Connection=True;TrustServerCertificate=true;"
  }
'''

3. Criar uma tabela chamada Kim no banco de dados SQL Server.
