# stone-webapi-breakeven
> Descrição do projeto:
> Implementação de uma carteira que contém ativos para compra e/ou venda;

> Entidades e suas particularidades:
> - AccountBanking: Necessário informar um "document" para a criação.
> - Wallet: É a carteira que cada AccountBanking terá! De forma dinâmica, ao criar uma AccountBanking, será criado uma Wallet em segundo momento que será vinculada a AccountBanking que foi criada. A mesma é utilizada para depósito e/ou retirada de valores;
> - Products: Os produtos (ativos) que serão criados e estarão disponíveis para a compra e/ou venda;
> - WalletProduct: Como Wallet e Product são N:N, essa é a entidade que fará a "ponte";
> - Extract: Todos as operações de compra e venda, assim como, depósito e retirada de valores serão registradas nesta entidade;

> Ferramentas:
> - Postman;
> - .NET 6
> - Banco de dados: Mysql

> Inicialização:
> - CLone o repositório git;
> - Congifure o banco de dados Mysql no "Localhost", a porta padrão utilizada é "3306";
> - Será necessário login e senha para acesso do banco de dados, os mesmos encontra-se no arquivo: appsettings.json
