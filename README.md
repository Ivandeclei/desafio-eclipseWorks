# Gerenciamento
## Resumo de solução
Para solucionar o desafio  escolhi utilizar uma arquitetura hexagonal porque possibilita um desacoplamento de tecnologias externas, possibilitando um isolamento do nucleo do sistema de qualquer outra aplicação externa, facilitando amudança de tecnologia ou adições de novos requisitos. Facilita a testabilidade da logica do negocio alem de facilitar a manutenção, organização explicita do nucleo da aplicação e sistemas externos,  facilitando tambem o uso dos principis SOLID.


## Fase 1: Execução da aplicação
**Ir no diretorio onde foi baixado a aplicação e rodar o comando para subir a aplicação nos containers**
    - docker-compose up --build
    ou
    - usar a propria ferramenta do visual studio para executar a aplicação usando docker-compose
    
 ##   
Apos os containers estiverem rodando abrir o visual studio  e rodar no terminal PACK MANAGER CONSOLE -  OBS: Certifique-se de criar o banco de dados se o mesmo nao for criado*

-antes de executar o comando é necessario alterar a conecction string para localhost
-
-```"ConnectionString": "Server=localhost; Database=gerenciamento;User Id=sa;Password=SuperPassword@123;TrustServerCertificate=True;"```

- Rode o comando abaixo para executar as migrations
- update-database

**Para executar a aplicação a connection string deve estar com o nome do servidor para mssql-server**
-
-```"ConnectionString": "Server=mssql-server; Database=gerenciamento;User Id=sa;Password=SuperPassword@123;TrustServerCertificate=True;"```

OBS: para acessar o banco de dados via Sql server Management Studio é necessario utilizar o nome do servidor como localhost

## Fase 2: Perguntas Para o PO

1. **Por que uma tarefa não pode mudar de prioridade?**
   - Quais são as razões por trás dessa restrição?
   - Existe alguma situação específica onde essa regra pode ser flexibilizada?
   - Quais são as implicações operacionais de permitir a mudança de prioridade?

2. **Por que um projeto tem limitação de quantidade de tarefas?**
   - Qual é a lógica por trás dessa limitação?
   - Existem critérios específicos para definir essa quantidade máxima?
   - Como essa limitação impacta a gestão e organização dos projetos?

## Fase 3: Melhorias

1. **Implementar autenticação da API**
   - Adicionar mecanismos de autenticação para garantir a segurança e a integridade dos dados.
   - Escolher o método de autenticação mais adequado (por exemplo, OAuth, JWT).
   - Documentar o processo de autenticação para desenvolvedores e usuários.

2. **Remover o vínculo da tabela de atualizações em relação a Tasks**
   - Avaliar a necessidade de manter um log detalhado de atualizações diretamente vinculado às tarefas.
   - Criar uma estratégia de arquivamento ou resumo de logs para evitar buscas extensas e melhorar a performance.
   - Implementar uma solução para buscar apenas as informações necessárias sem carregar um log excessivamente grande como por exemplo os comentarios.
