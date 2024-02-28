using Domain.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.BackgroundWorkers;

public class PersonRegistrationWorker : BackgroundService
{

        private readonly IPersonRegistrationQueue _personRegistrationQueue;
        private readonly IExternalPartnerService _externalPartnerService;
        private readonly ISecondExternalPartnerService _secondExternalPartnerService;
        private readonly ILogger<PersonRegistrationWorker> _logger;

        public PersonRegistrationWorker(IPersonRegistrationQueue personRegistrationQueue, IExternalPartnerService externalPartnerService, ISecondExternalPartnerService secondExternalPartnerService, ILogger<PersonRegistrationWorker> logger)
        {
            _personRegistrationQueue = personRegistrationQueue;
            _externalPartnerService = externalPartnerService;
            _secondExternalPartnerService = secondExternalPartnerService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("PersonRegistrationWorker iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var person = await _personRegistrationQueue.DequeueAsync(stoppingToken);

                if (person != null)
                {
                    try
                    {
                        _logger.LogInformation("Iniciando o cadastro da pessoa {PersonId} nos serviços externos...",
                            person.Id);

                        // Tenta registrar a pessoa no primeiro serviço externo
                        var successFirstService = await _externalPartnerService.RegisterPersonAsync(person);

                        if (successFirstService)
                        {
                            // Se o cadastro no primeiro serviço foi bem-sucedido, tenta registrar no segundo serviço
                            var successSecondService = await _secondExternalPartnerService.RegisterPersonAsync(person);

                            if (successSecondService)
                            {
                                _logger.LogInformation(
                                    "Pessoa {PersonId} cadastrada com sucesso em ambos os serviços externos.",
                                    person.Id);
                            }
                            else
                            {
                                _logger.LogError("Falha ao cadastrar pessoa {PersonId} no segundo serviço externo.",
                                    person.Id);
                                // Aqui você pode adicionar lógica para tentar novamente, enviar para uma fila de erro, etc.
                            }
                        }
                        else
                        {
                            _logger.LogError("Falha ao cadastrar pessoa {PersonId} no primeiro serviço externo.",
                                person.Id);
                            // Aqui você pode adicionar lógica para tentar novamente, enviar para uma fila de erro, etc.
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Erro ao cadastrar pessoa {PersonId} nos serviços externos.", person.Id);
                        // Aqui você pode adicionar lógica para tentar novamente, enviar para uma fila de erro, etc.
                    }
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(1),
                        stoppingToken); // Aguarda um tempo antes de verificar novamente a fila
                }
            }

            _logger.LogInformation("PersonRegistrationWorker parado.");
        }
    }

