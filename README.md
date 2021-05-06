# Livraria

*Controle de Restaurante (Favo de Mel)

## Início

* *Cadastro de Usuários*
* *Cadastro de Comandas*
* *Fechamento de Comandas*
* *Cadastro de Pedidos por Comandas*
* *Finalização dos Pedidos*
* *Cancelamento dos Pedidos*


* *Perfis com acesso para "Garçom" poderá abrir e fechar comandas*
* *Perfis com acesso para "Cozinha" poderá apenas visualizar comandas*

* *Perfis com acesso para "Garçom" poderá cadastrar e cancelar pedidos*
* *Perfis com acesso para "Cozinha" poderá apenas finalizar pedidos*


### Pré-requisito

* Visual Studio 2019 ou superior
* EF Core 5 ou superior
* SQL Server 2016 ou superior

### Desenvolvimento

* ASP.NET MVC
* EF Core 5
* Angular 8
* SQL Server
* XUnit

### Features
O projeto pode ser usado como modelo para iniciar o desenvolvimento de um projeto C# usando o EF Core 5 ou superior e banco de dados SQL Server 2016 ou superior, com testes unitários.

### Configuracão
Para executar o projeto, é necessário utilizar o Visual Studio 2017 ou superior. Uma vez importado o projeto, é necessário configurar o "appsettings.json" com a configuração do banco de dados.

* "Data Source": nome do servidor onde foi instalado o banco de dados.
* "initial catalog": nome do banco de dados.
* "user id": usuário de acesso do servidor.
* "password": senha de acesso do servidor.

No back-end ao executar o projeto no Visual Studio irá criar o banco de dados e as tabelas iniciais que já existem nas migrations existentes.
No front-end tem que mudar os arquivos "environment.ts" e "environment.prod.ts" a variável API, colocando a URL que gerou ai executar o projeto.


### Meta
Bruno Milani – brunodiasmilani@hotmail.com