using Dapper;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Hackathon.Fiap.Infra.Utils.DBContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace Hackathon.Fiap.Teste.Integracao
{
    public class HackatonApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
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

                    CREATE TABLE `Usuarios` (
                      `id` int NOT NULL AUTO_INCREMENT,
                      `nome` varchar(255) NOT NULL,
                      `email` varchar(255) DEFAULT NULL,
                      `cpf` varchar(14) DEFAULT NULL,
                      `hash` varchar(255) NOT NULL,
                      `tipo` enum('Medico','Paciente','Administrador') CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
                      `criado_em` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
                      PRIMARY KEY (`id`),
                      UNIQUE KEY `email` (`email`),
                      UNIQUE KEY `cpf` (`cpf`)
                    ) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

                    INSERT INTO `Usuarios` VALUES (1,'Victor Obfuscado','obfuscado@hotmail.com','16520400784','VVGM/EaxD7EYh7UQB7mBAQ==','Medico','2025-03-12 00:59:26'),
                                                  (2,'Usuario Marcondes','obfuscado2@gmail.com','16520400684','VVGM/EaxD7EYh7UQB7mBAQ==','Paciente','2025-03-13 00:31:32');

                    INSERT INTO `Usuarios` VALUES (3,'SIS_MEDICO','SIS@MEDICO.com','00000000000','VVGM/EaxD7EYh7UQB7mBAQ==','Medico','2025-03-12 00:59:26');
                    INSERT INTO `Usuarios` VALUES (4,'SIS_PACIENTE','SIS@PACIENTE.com','00000000001','VVGM/EaxD7EYh7UQB7mBAQ==','Paciente','2025-03-12 00:59:26');
                    INSERT INTO `Usuarios` VALUES (5,'SIS_ADMINISTRADOR','SIS@ADMINISTRADOR.com','00000000002','VVGM/EaxD7EYh7UQB7mBAQ==','Administrador','2025-03-12 00:59:26');
                    

                    CREATE TABLE `Medicos` (
                      `id` int NOT NULL,
                      `crm` varchar(20) NOT NULL,
                      PRIMARY KEY (`id`),
                      UNIQUE KEY `crm` (`crm`),
                      CONSTRAINT `Medicos_ibfk_1` FOREIGN KEY (`id`) REFERENCES `Usuarios` (`id`) ON DELETE CASCADE
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


                    INSERT INTO `Medicos` VALUES (1,'052');

                    CREATE TABLE `Consultas` (
                      `id` int NOT NULL AUTO_INCREMENT,
                      `paciente_id` int NOT NULL,
                      `medico_id` int NOT NULL,
                      `data_hora` datetime NOT NULL,
                      `valor` decimal(10,2) NOT NULL,
                      `status` enum('Pendente','Aceita','Recusada','Cancelada') DEFAULT 'Pendente',
                      `justificativa_cancelamento` text,
                      `criado_em` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
                      `horarios_disponiveis_id` int DEFAULT NULL,
                      PRIMARY KEY (`id`),
                      KEY `medico_id` (`medico_id`),
                      KEY `idx_consultas_paciente` (`paciente_id`),
                      CONSTRAINT `Consultas_ibfk_1` FOREIGN KEY (`paciente_id`) REFERENCES `Usuarios` (`id`) ON DELETE CASCADE,
                      CONSTRAINT `Consultas_ibfk_2` FOREIGN KEY (`medico_id`) REFERENCES `Medicos` (`id`) ON DELETE CASCADE
                    ) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


                    INSERT INTO `Consultas` VALUES (1,1,1,'2025-03-13 00:31:32',9252.00,'Cancelada','string','2025-03-13 23:58:14',NULL);
                    INSERT INTO `Consultas` VALUES (2,1,1,'2025-03-13 00:31:32',9252.00,'Pendente','string','2025-03-13 23:58:15',NULL);

                    CREATE TABLE `Especialidades` (
                      `id` int NOT NULL AUTO_INCREMENT,
                      `nome` varchar(255) NOT NULL,
                      `descricao` varchar(800) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
                      PRIMARY KEY (`id`),
                      UNIQUE KEY `nome` (`nome`)
                    ) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

             
                    INSERT INTO `Especialidades` VALUES (1,'Especialista.', 'Descricao');

                    CREATE TABLE `Horarios_Disponiveis` (
                      `id` int NOT NULL AUTO_INCREMENT,
                      `medico_id` int NOT NULL,
                      `paciente_id` int DEFAULT NULL,
                      `data_hora_inicio` datetime NOT NULL,
                      `status` enum('Disponivel','Reservado','Cancelado') DEFAULT 'Disponivel',
                      `data_hora_fim` datetime NOT NULL,
                      PRIMARY KEY (`id`),
                      KEY `paciente_id` (`paciente_id`),
                      KEY `idx_horarios_medico` (`medico_id`,`data_hora_inicio`),
                      CONSTRAINT `Horarios_Disponiveis_ibfk_1` FOREIGN KEY (`medico_id`) REFERENCES `Medicos` (`id`) ON DELETE CASCADE,
                      CONSTRAINT `Horarios_Disponiveis_ibfk_2` FOREIGN KEY (`paciente_id`) REFERENCES `Usuarios` (`id`) ON DELETE SET NULL
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
                    
                    INSERT INTO techchallenge.Horarios_Disponiveis (id, medico_id, paciente_id, data_hora_inicio, status, data_hora_fim)
                                                             VALUES(1, 1, 1, '2025-03-18 08:00:00', 'Disponivel', '2025-03-18 08:30:00');
                    
                    CREATE TABLE `Medico_Especialidades` (
                      `medico_id` int NOT NULL,
                      `especialidade_id` int NOT NULL,
                      PRIMARY KEY (`medico_id`,`especialidade_id`),
                      KEY `especialidade_id` (`especialidade_id`),
                      CONSTRAINT `Medico_Especialidades_ibfk_1` FOREIGN KEY (`medico_id`) REFERENCES `Medicos` (`id`) ON DELETE CASCADE,
                      CONSTRAINT `Medico_Especialidades_ibfk_2` FOREIGN KEY (`especialidade_id`) REFERENCES `Especialidades` (`id`) ON DELETE CASCADE
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

                    INSERT INTO `Medico_Especialidades` VALUES (1,1);
                    ";

                    connection.Execute(createTablesSql);
                }

                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DapperContext));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddSingleton(new DapperContext(connectionString));

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
