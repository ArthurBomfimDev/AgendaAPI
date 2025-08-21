<div align="center">
<img src="https://placehold.co/1200x300/512BD4/FFFFFF?text=AgendaAPI&font=raleway" alt="Banner do Projeto AgendaAPI">
</div>

<h1 align="center">AgendaAPI: Gerenciador de Atividades com Clean Architecture</h1>

<p align="center">
<img src="https://img.shields.io/badge/.NET-8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 8">
<img src="https://img.shields.io/badge/C%23-12-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C# 12">
<img src="https://img.shields.io/badge/Entity%20Framework-4479A1?style=for-the-badge&logo=nuget&logoColor=white" alt="Entity Framework">
<img src="https://img.shields.io/badge/SQL%20Server-2019%2B-red?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" alt="SQL Server 2019+">
<img src="https://img.shields.io/badge/Arquitetura-Clean%20%26%20CQRS-blue?style=for-the-badge" alt="Arquitetura Limpa e CQRS">
<img src="https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black" alt="Swagger">
<img src="https://img.shields.io/badge/MediatR-2E86C1?style=for-the-badge" alt="MediatR">
</p>

# 🎯 Sobre o Projeto

Este projeto é a minha solução para o desafio de desenvolvimento de API do Bootcamp GFT Start #7 .NET, oferecido pela Digital Innovation One (DIO).

O objetivo inicial era criar um simples CRUD para um gerenciador de tarefas. No entanto, abracei o desafio como uma oportunidade para aprofundar meus conhecimentos em arquiteturas de software robustas e escaláveis. Por isso, o projeto foi inteiramente reestruturado para implementar os princípios da Clean Architecture e o padrão CQRS com MediatR, visando a máxima separação de responsabilidades, testabilidade e manutenibilidade.

<p align="center">
<a href="https://www.dio.me/" target="_blank">
<img src="https://hermes.digitalinnovation.one/assets/diome/logo-full.svg" alt="Logotipo da DIO" width="250">
</a>
</p>

---

# 🧠 Domínio Inteligente: A Entidade Activity
A entidade Tarefa do desafio original foi completamente reimaginada como Activity, transformando-a em um modelo de domínio rico e inteligente.

Monitoramento de Tempo Detalhado: A entidade agora possui um sistema de log de tempo que rastreia cada mudança de status. Isso permite calcular com precisão o tempo total gasto, o tempo em andamento, o tempo em pausa e até mesmo o tempo de atraso de uma atividade.

Ciclo de Vida Claro: O fluxo de status (Pendente, Em Andamento, Pausado, Concluído, Cancelado) é gerenciado por ações específicas, garantindo a integridade e a consistência do estado da atividade.

<table>
<thead>
<tr>
<th style="text-align: left;">Propriedade</th>
<th style="text-align: left;">Tipo</th>
<th style="text-align: left;">Descrição</th>
</tr>
</thead>
<tbody>
<tr>
<td><strong>Title</strong></td>
<td><code>string</code></td>
<td>Título principal da atividade.</td>
</tr>
<tr>
<td><strong>Description</strong></td>
<td><code>string?</code></td>
<td>Descrição opcional e detalhada da atividade.</td>
</tr>
<tr>
<td><strong>Status</strong></td>
<td><code>EnumActivityStatus</code></td>
<td>Status atual (Pendente, Andamento, Pausado, etc.).</td>
</tr>
<tr>
<td><strong>Priority</strong></td>
<td><code>EnumActivityPriority</code></td>
<td>Nível de prioridade da atividade (Baixa, Média, Alta).</td>
</tr>
<tr>
<td><strong>DueDate</strong></td>
<td><code>DateTimeOffset?</code></td>
<td>Prazo final para a conclusão da atividade (campo nullable, para atividades sem prazo definido).</td>
</tr>
<tr>
<td><strong>FinalWorkedTime</strong></td>
<td><code>TimeSpan?</code></td>
<td><strong>Tempo total efetivamente trabalhado</strong>, somando todos os períodos em que a atividade esteve "Em Andamento".</td>
</tr>
<tr>
<td><strong>DelayDuration</strong></td>
<td><code>TimeSpan?</code></td>
<td><strong>Tempo de atraso</strong>, calculado caso a data de conclusão ultrapasse o <code>DueDate</code>.</td>
</tr>
<tr>
<td><strong>TimeToStart</strong></td>
<td><code>TimeSpan?</code></td>
<td>Tempo decorrido entre a criação e o primeiro início da atividade.</td>
</tr>
<tr>
<td><strong>TimeLogs</strong></td>
<td><code>IReadOnlyCollection</code></td>
<td>Coleção de registros (<code>ActivityTimeLog</code>) que marcam o início e o fim de cada período de trabalho.</td>
</tr>
</tbody>
</table>

# 🏛️ Arquitetura: Clean Architecture + CQRS & MediatR
O coração do projeto é sua arquitetura. A escolha pela Clean Architecture garante um baixo acoplamento entre as camadas, enquanto o CQRS (Command and Query Responsibility Segregation) separa as operações de escrita (Commands) das de leitura (Queries).

Por que essa escolha? Para otimizar cada fluxo de forma independente. As escritas são focadas em consistência e regras de negócio, enquanto as leituras são otimizadas para performance máxima.

MediatR: Atua como um "maestro" na camada de aplicação, desacoplando os componentes e simplificando a comunicação, o que torna o código mais limpo e fácil de manter.

<div align="center">
<img src="https://blog.cleancoder.com/uncle-bob/images/2012-08-13-the-clean-architecture/CleanArchitecture.jpg" alt="Diagrama da Clean Architecture" width="600">
<p><em>Diagrama representando a estrutura da Clean Architecture por Robert C. Martin.</em></p>
</div>


# 🚀 Performance e Otimização
Repositórios de Leitura e Escrita: Seguindo o padrão CQRS, foram criados repositórios distintos.

WriteRepository: Focado em operações de escrita (Create, Update, Delete) e garantindo a integridade transacional com o Unit of Work.

ReadRepository: Especializado em consultas, projetando os resultados diretamente em DTOs (Data Transfer Objects). Isso evita o over-fetching de dados e melhora drasticamente a performance das leituras.

Queries Otimizadas: As consultas de leitura são construídas para serem o mais eficientes possível, buscando apenas os dados necessários para cada caso de uso.

# 📦 Padrões e Boas Práticas
Unit of Work: Garante que múltiplas operações de escrita sejam executadas em uma única transação, mantendo a consistência do banco de dados.

DTOs para Contratos de API: A comunicação com o mundo exterior é feita através de DTOs, protegendo o modelo de domínio de exposições desnecessárias e criando um contrato de API estável.

Bases Reutilizáveis: O uso de classes base para repositórios e outras estruturas acelera o desenvolvimento e reduz a duplicação de código (princípio DRY).

# 📖 Endpoints da API e Swagger
A API foi documentada utilizando o Swagger (OpenAPI) para facilitar a visualização e o teste dos endpoints. Você pode acessar a interface interativa para explorar todos os recursos disponíveis.

<div align="center">
<table>
<thead>
<tr>
<th style="text-align: left;">Verbo</th>
<th style="text-align: left;">Endpoint</th>
<th style="text-align: left;">Descrição</th>
</tr>
</thead>
<tbody>
<tr>
<td><strong>POST</strong></td>
<td><code>/api/activities</code></td>
<td>Cria uma nova atividade.</td>
</tr>
<tr>
<td><strong>GET</strong></td>
<td><code>/api/activities/all</code></td>
<td>Obtém todas as atividades.</td>
</tr>
<tr>
<td><strong>GET</strong></td>
<td><code>/api/activities/{id}</code></td>
<td>Obtém uma atividade específica pelo seu ID.</td>
</tr>
<tr>
<td><strong>GET</strong></td>
<td><code>/api/activities/title/{title}</code></td>
<td>Busca atividades por título.</td>
</tr>
<tr>
<td><strong>GET</strong></td>
<td><code>/api/activities/date/{date}</code></td>
<td>Busca atividades por uma data específica.</td>
</tr>
<tr>
<td><strong>GET</strong></td>
<td><code>/api/activities/status/{status}</code></td>
<td>Busca atividades por status.</td>
</tr>
<tr>
<td><strong>PUT</strong></td>
<td><code>/api/activities/{id}</code></td>
<td>Atualiza os dados principais de uma atividade existente.</td>
</tr>
<tr>
<td><strong>DELETE</strong></td>
<td><code>/api/activities/{id}</code></td>
<td>Deleta uma atividade.</td>
</tr>
<tr>
<td><strong>PATCH</strong></td>
<td><code>/api/activities/{id}/start</code></td>
<td>Inicia o trabalho em uma atividade.</td>
</tr>
<tr>
<td><strong>PATCH</strong></td>
<td><code>/api/activities/{id}/pause</code></td>
<td>Pausa o trabalho em uma atividade.</td>
</tr>
<tr>
<td><strong>PATCH</strong></td>
<td><code>/api/activities/{id}/complete</code></td>
<td>Marca uma atividade como concluída.</td>
</tr>
<tr>
<td><strong>PATCH</strong></td>
<td><code>/api/activities/{id}/cancel</code></td>
<td>Cancela uma atividade.</td>
</tr>
<tr>
<td><strong>PATCH</strong></td>
<td><code>/api/activities/{id}/priority</code></td>
<td>Altera a prioridade de uma atividade.</td>
</tr>
<tr>
<td><strong>PATCH</strong></td>
<td><code>/api/activities/{id}/due-date</code></td>
<td>Altera a data de vencimento de uma atividade.</td>
</tr>
</tbody>
</table>
</div>

<div align="center">
<img src="https://raw.githubusercontent.com/ArthurBomfimDev/AgendaAPI/main/swagger.png" alt="Interface do Swagger" width="700">
<p><em>Interface do Swagger para teste e documentação dos endpoints.</em></p>
</div>

# 💡 Próximos Passos e Melhorias Futuras
O projeto possui uma base sólida que permite a evolução para um sistema ainda mais completo e robusto. Algumas implementações futuras que agregariam grande valor são:

Projeto de Testes: Criação de um projeto dedicado para testes (xUnit, NUnit) para implementar testes unitários na camada de domínio e aplicação, garantindo a qualidade e a confiabilidade das regras de negócio.

Sistema de Usuários e Autenticação: Implementação de um sistema de identidade para permitir que múltiplos usuários gerenciem suas próprias listas de atividades de forma segura.

Atribuição de Tarefas: Com um sistema de usuários, seria possível evoluir o domínio para permitir a atribuição de atividades a usuários específicos, transformando a aplicação em uma ferramenta colaborativa.
