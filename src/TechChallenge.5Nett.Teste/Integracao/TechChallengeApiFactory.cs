using Dapper;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Utils.DBContext;

namespace Integracao.TechChallengeApi.Factory
{
    public class TechChallengeApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {

        private readonly IContainer mysqlContainer = new ContainerBuilder()
            .WithImage("mysql:latest")
            .WithEnvironment("MYSQL_ROOT_PASSWORD", "Teste123")
            .WithEnvironment("MYSQL_DATABASE", "techchallenge")
            .WithPortBinding(3306, 3306)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(3306))
            .Build();


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                string connectionString = "Server=localhost; Database=techchallenge; Uid=root; Pwd=Teste123;AllowUserVariables=true;";

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string createDatabaseSql = "CREATE DATABASE IF NOT EXISTS `techchallenge`;";
                    connection.Execute(createDatabaseSql);

                    connection.ChangeDatabase("techchallenge");

                    string createTablesSql = @"
                            CREATE TABLE techchallenge.contatos (
                              id int NOT NULL AUTO_INCREMENT,
                              nome varchar(100) DEFAULT NULL,
                              email varchar(100) DEFAULT NULL,
                              ddd int DEFAULT NULL,
                              telefone varchar(15) DEFAULT NULL,
                              PRIMARY KEY(id)
                            );

                            CREATE TABLE techchallenge.permissoes (
                              id int NOT NULL AUTO_INCREMENT,
                              nome varchar(100) DEFAULT NULL,
                              PRIMARY KEY(id)
                            );

                            CREATE TABLE techchallenge.regioes (
                              ddd int NOT NULL,
                              estado varchar(2) DEFAULT NULL,
                              regiao varchar(20) DEFAULT NULL,
                              PRIMARY KEY(ddd)
                            );

                            CREATE TABLE techchallenge.usuarios (
                              id int NOT NULL AUTO_INCREMENT,
                              nome varchar(100) DEFAULT NULL,
                              email varchar(100) DEFAULT NULL,
                              hash varchar(1000) DEFAULT NULL,
                              data_criacao timestamp NOT NULL,
                              permissao int DEFAULT NULL,
                              PRIMARY KEY(id)
                            );

                            INSERT INTO techchallenge.regioes (ddd,estado,regiao) VALUES
	                             (11,'SP','Sudeste'),
	                             (21,'RJ','Sudeste'),
	                             (27,'ES','Sudeste'),
	                             (41,'PR','Sul'),
	                             (61,'DF','Centro-Oeste'),
	                             (71,'BA','Nordeste'),
	                             (91,'PA','Norte');
                    ";

                    connection.Execute(createTablesSql);
                }

                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DapperContext));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddSingleton<DapperContext>(new DapperContext(connectionString));

            });
        }

        public async Task InitializeAsync()
        {
            await mysqlContainer.StartAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await mysqlContainer.StopAsync();
        }
    }
}
