# Tech Challenge - Fase 2

## O Problema
A fase anterior do Tech Challenge envolveu a criação de um aplicativo .NET para cadastro de contatos regionais, com funcionalidades de adicionar, consultar, atualizar e excluir contatos, utilizando Entity Framework Core ou Dapper para persistência de dados e implementação de validações de dados.   

Agora, vamos elevar o nível deste projeto, integrando práticas de Integração Contínua (CI), testes de integração e monitoramento de performance.


## Objetivos

- **Testes de Integração:** assegurar que os componentes do sistema funcionem corretamente quando integrados.
- **Integração Contínua (CI) com GitHub Actions:** automatizar testes unitários, testes de integração e build.
- **Monitoramento com Prometheus e Grafana:** implementar métricas para monitorar a saúde e o desempenho do aplicativo (é possível utilizar o Docker local para esta etapa).

## Requisitos Técnicos Detalhados

**GitHub Actions**   

**CI Pipeline:**
- **Build:** compilar o projeto para garantir que não há erros de compilação.
- **Testes Unitários:** executar testes unitários para garantir que as funcionalidades estão trabalhando conforme o esperado.
- **Testes de Integração:** executar testes de integração para validar o funcionamento correto entre os componentes do sistema, como o banco de dados e a aplicação.


**Prometheus:**
- Integrar Prometheus ao aplicativo para coletar métricas como latência das requisições, uso de CPU e memória.
- Configurar os endpoints de métricas no aplicativo.

**Grafana:**
- Configurar um dashboard em Grafana para visualizar as métricas coletadas pelo Prometheus.
- Criar painéis para exibir métricas específicas como latência por endpoint, contagem de requisições por status de resposta, uso de recursos do sistema etc

___

# Tech Challenge - Fase 1

## O Problema

O Tech Challenge desta fase será desenvolver um aplicativo utilizando a plataforma .NET 8 para cadastro de contatos regionais, considerando a persistência de dados e a qualidade do software.

## Requisitos Funcionais

- **Cadastro de contatos:** permitir o cadastro de novos contatos, incluindo nome, telefone e e-mail. Associe cada contato a um DDD correspondente à região.
- **Consulta de contatos:** implementar uma funcionalidade para consultar e visualizar os contatos cadastrados, os quais podem ser filtrados pelo DDD da região.
- **Atualização e exclusão:** possibilitar a atualização e exclusão de contatos previamente cadastrados.

## Requisitos Técnicos

- **Persistência de Dados:** utilizar um banco de dados para armazenar as informações dos contatos. Escolha entre Entity Framework Core ou Dapper para a camada de acesso a dados.
- **Validações:** implementar validações para garantir dados consistentes (por exemplo: validação de formato de e-mail, telefone, campos obrigatórios).
- **Testes unitários:** desenvolver testes unitários utilizando xUnit ou NUnit.
