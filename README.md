## Teste Atak - Gerador de dados Backend
Este projeto é parte de um teste que gera dados fictícios de clientes em uma planilha Excel e envia por e-mail.

### Tecnologias Utilizadas
- **ASP.NET 8**
- **Entity Framework Core**
- **SQLite** para o banco de dados
- **JWT** para autenticação

### Requisitos
Para rodar este projeto, é necessário ter o **.NET SDK 8.0** instalado.

### Instalação e Execução
1. Clone o repositório
2. Acesse o diretório
3. Restaure as dependências: ```dotnet restore```
4. Configurações adicionais:
  O arquivo appsettings.json já está configurado, porém, você pode precisar ajustar a URL que conecta o frontend ao backend. Para isso, abra o arquivo Program.cs e adicione a URL do frontend ao configurar as políticas de CORS.
5. Rodar a aplicação: ```dotnet run```

### Observações
* O banco de dados SQLite já está incluído no repositório, então não há necessidade de rodar migrações.
* Lembre de configure o CORS no arquivo Program.cs para permitir a comunicação entre as aplicações.
