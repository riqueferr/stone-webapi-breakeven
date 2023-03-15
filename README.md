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
> - Docker

> Inicialização:
> - CLone o repositório git;
> - Congifure o banco de dados Mysql no server "localhost", a porta padrão utilizada é "3306". Além do database, deve se chamar "breakeven";
> - Modifique o nome do arquivo appsettings_sample.json para appsettings.json;
> - Coloque o login e senha do banco utilizada em seu localhost;
> - Os demais pacotes utilizados já estão inclusos no projeto;
