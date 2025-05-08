# HACKATHON 5NETT

 
## O Contexto
A Health&Med, uma startup inovadora no setor de saúde, está desenvolvendo um novo sistema que irá revolucionar a Telemedicina no país. Atualmente, a startup oferece a possibilidade de agendamento de consultas e realização de consultas online (Telemedicina) por meio de sistemas terceiros como Google Agenda e Google Meetings.

Recentemente, a empresa recebeu um aporte e decidiu investir no desenvolvimento de um sistema proprietário, visando proporcionar um serviço de maior qualidade, segurança dos dados dos acientes e redução de custos. O objetivo é criar um sistema robusto, escalável e seguro que permita o gerenciamento eficiente desses agendamentos e consultas.
 
Para viabilizar o desenvolvimento de um sistema que esteja em conformidade com as melhores práticas de qualidade e arquitetura de software, a Health&Med contratou os alunos do curso 5NETT para fazer a análise do projeto, arquitetura do software e desenvolvimento do MVP.
 

## Requisitos Funcionais
1. Autenticação do Usuário (Médico) ✔
 - O sistema deve permitir que o médico faça login usando o número de CRM e uma senha. ✔

2. Cadastro/Edição de Horários Disponíveis (Médico) ✔
 - O sistema deve permitir que o médico cadastre e edite os horários disponíveis para agendamento de consultas. ✔

3. Aceite ou Recusa de Consultas Médicas (Médico) ✔
 - O médico deve poder aceitar ou recusar consultas médicas agendadas. ✔

4. Autenticação do Usuário (Paciente) ✔
 - O sistema deve permitir que o paciente faça login usando um e-mail ou CPF e uma senha. ✔

5. Busca por Médicos (Paciente) ✔
 - O sistema deve permitir que o paciente visualize a lista de médicos disponíveis, utilizando filtros como especialidade.✔

6. Agendamento de Consultas (Paciente)✔
 - Após selecionar o médico, o paciente deve poder visualizar a agenda do médico e o valor da consulta, e efetuar o agendamento.✔
 - O usuário paciente poderá cancelar a consulta mediante justificativa.✔



## Requisitos Não Funcionais

1. Alta Disponibilidade
- O sistema deve estar disponível 24/7 devido à sua natureza crítica no setor de saúde.

2. Escalabilidade
- O sistema deve ser capaz de lidar com alta demanda, especialmente para profissionais muito procurados.
- O sistema deve suportar até 20.000 usuários simultâneos em horários de pico.

3. Segurança
- A proteção dos dados sensíveis dos pacientes deve seguir as melhores práticas de segurança da informação.


## Entregáveis Mínimos

Os grupos deverão entregar o seguinte:

1. Desenho da Solução MVP
- Diagrama da arquitetura que atenda aos requisitos funcionais e justificativas das escolhas técnicas.
- Descrição de como os requisitos não funcionais serão atendidos.

2. Demonstração da Infraestrutura
- Mostrando a aplicação funcionando na infraestrutura, com exemplos de uso real (como chamadas de API).

3. Demonstração da Esteira de CI/CD
- Explicação e demonstração do pipeline de deploy da aplicação.

4. Demonstração do MVP
- Aplicação executando na nuvem ou local, os itens de 1 a 6 dos requisitos funcionais, contemplando:

  - Autenticação do Usuário (Médico)
  
  - Cadastro/Edição de Horários Disponíveis (Médico)
 
  - Aceite ou Recusa de Consultas Médicas (Médico)
 
  - Autenticação do Usuário (Paciente)
 
  - Busca por Médicos (Paciente)
 
  - Agendamento de Consultas (Paciente)
 

## Integrantes

| Nome                      | RM     |
| ------------------------- | ------ |
| Edinam Marcondes          | 357908 |
| João Paulo Coaio          | 357906 |
| Victor Nascimento Peroba  | 357907 | 


## Pipeline do Projeto

A pipeline CI é executada a cada commit em qualquer branch que tenha o prefixo 'feature/*', já a pipeline CD é executada a cada merge na branch main.

[https://github.com/vicnp/hackathon-fiap/actions](https://github.com/vicnp/techchallange/actions)


## Arquitetura

<img width="564" alt="arquitetura" src="https://github.com/user-attachments/assets/dd96ce82-32a0-4d54-a6cb-d7465e6eba7b" />


## Subdomínios

<img width="564" alt="arquitetura" src="https://github.com/user-attachments/assets/5df55893-e232-4786-ba14-54fd3d938dc8" />





## Domain Storytelling

### Cadastro de Médico

- O médico preenche uma ficha funcional com seus dados, suas especialidades e CRM para avaliação do Time administrativo do hospital, que por sua vez faz uma avaliação e o devido arquivamento do documento.

![Cadastro de Médico](https://github.com/user-attachments/assets/cfb85320-fce0-4978-aba1-9d79625007d8)

  
### Cadastro de Paciente

- O paciente preenche a ficha cadastral na recepção, que é avaliada e devidamente arquivada pelos recepcionistas do hospital.
  
![Cadastro de Paciente](https://github.com/user-attachments/assets/031a3e7f-06a2-42d4-b996-cde23c57dfce)


### Agendamento de Consulta

- O Paciente, depois de sua ficha feita na recepção do hospital, usa o sistema para agendar uma próxima consulta.
- Assim que o agendamento é solicitado pelo paciente, a equipe da recepção recebe uma notificação, faz a avaliação da ficha cadastral do paciente, consulta os horários disponíveis, realizam o agendamento e notificam o paciente por email.

![Agendamento de Consulta](https://github.com/user-attachments/assets/4a314479-4df1-44f9-98ac-1b321fb2606e)


## Testes de unidade e integração

![Captura de Tela 2025-05-07 às 20 48 48](https://github.com/user-attachments/assets/e9d3b6a5-7334-4392-9e2b-bcd9c19be689)

![Captura de Tela 2025-05-07 às 20 51 26](https://github.com/user-attachments/assets/2c1f012d-ec75-44b4-afd3-40657702d0e9)

![Captura de Tela 2025-05-07 às 20 53 16](https://github.com/user-attachments/assets/ade68d4f-9300-41c6-9d93-82c38065f203)

## Instrumentação da API

![Captura de Tela 2025-05-07 às 21 39 23](https://github.com/user-attachments/assets/1528d7a1-8d47-4358-b446-c859783dfa14)


## Justificativas das Escolhas Técnicas
### API REST (ASP.NET Core)
- Framework robusto e maduro
- Excelente performance
- Suporte nativo a async/await
- Fácil integração com MySQL
- Suporte a containers Docker
- Documentação automática com Swagger

### Banco de Dados (MySQL)
- Open source
- Alta performance
- Suporte a transações
- Índices otimizados
- Backup e replicação
- Compatibilidade com .NET

## Estratégias de Implementação
### Autenticação
- JWT para autenticação stateless
- Refresh tokens para renovação
- Validação de CRM e CPF
- Senhas criptografadas com bcrypt

### Agendamento
- Validação de conflitos de horário
- Notificações via email
- Status da consulta
- Histórico de consultas

### Segurança
- Autenticação JWT
- Criptografia de dados sensíveis
- HTTPS em todas as comunicações
- Validação de entrada de dados
- Logs de auditoria
- Conformidade com LGPD
