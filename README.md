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
| Victor Nascimetno Peroba  | 357907 | 


## Pipeline do Projeto

A pipeline CI é executada a cada commit em qualquer branch que tenha o prefixo 'feature/*', já a pipeline CD é executada a cada merge na branch main.

https://github.com/edinanxs/hackathon-fiap/actions


## Arquitetura

<img width="564" alt="arquitetura" src="https://github.com/user-attachments/assets/fcd522f0-52c7-4eb7-8f17-8edee1058f29" />


## Subdomínios

![subdominios](https://github.com/user-attachments/assets/23d3df6f-0fdb-4edb-b150-d0e773952f90)


## Objetos de Valor

![objetos de velor](https://github.com/user-attachments/assets/eb2da194-1883-49cd-928d-14e705d8d0c1)



## Domain Storytelling

### Cadastro de Médico

![Cadastro Medico](https://github.com/user-attachments/assets/ef3aa9d0-b26e-4f2c-a30c-434f377ec61c)


### Cadastro de Paciente

![Cadastro Paciente](https://github.com/user-attachments/assets/0d32dbfb-b21a-4ffd-8d16-fce6cfe45fe7)


### Agendamento de Consulta

![Agendamento Consulta](https://github.com/user-attachments/assets/4d1bd5e8-492f-4222-bec1-a646d0e219a3)


### Aceite/Recusa de Consulta

![Aceite e Recusa de consulta](https://github.com/user-attachments/assets/689bb1fe-070a-4c8c-b933-aae00a0a5355)



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
