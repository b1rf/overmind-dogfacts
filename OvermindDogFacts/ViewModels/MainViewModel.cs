using Microsoft.Extensions.Logging;
using OvermindDogFacts.Interfaces;
using OvermindDogFacts.Model;
using OvermindDogFacts.Models;
using OvermindDogFacts.Utils;
using OvermindDogFacts.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OvermindDogFacts.ViewModels
{
    internal class MainViewModel : ViewModelBase, IMainViewModel
    {

        private ConfigurationFact configFact;
        private IRepositorio repositorio;
        private IDogFactService docFactService;

        public MainViewModel(IRepositorio repositorio, ConfigurationFact configFact, IDogFactService docFactService)
        {
            this.repositorio = repositorio;
            this.configFact = configFact;
            this.docFactService = docFactService;
        }

        private string _messageValidation { get; set; }

        public string messageValidation
        {
            get
            {
                return _messageValidation;
            }

            set
            {
                if (_messageValidation != value)
                {
                    _messageValidation = value;
                    base.OnPropertyChanged("messageValidation");
                }
            }
        }

        
        private string _messageError { get; set; }

        public string messageError
        {
            get
            {
                return _messageError;
            }

            set
            {
                if (_messageError != value)
                {
                    _messageError = value;
                    base.OnPropertyChanged("messageError");
                }
            }
        }

        private Visibility _circularVisible { get; set; } = Visibility.Hidden;

        public Visibility circularVisible
        {
            get
            {
                return _circularVisible;
            }

            set
            {
                if (_circularVisible != value)
                {
                    _circularVisible = value;
                    base.OnPropertyChanged("circularVisible");
                }
            }
        }

        private bool _btnEnable { get; set; } = false;

        public bool btnEnable
        {
            get
            {
                return _btnEnable;
            }

            set
            {
                if (_btnEnable != value)
                {
                    _btnEnable = value;
                    base.OnPropertyChanged("btnEnable");
                }
            }
        }

        public string endPoint
        {
            get
            {
                return configFact.EndPoint;
            }

            set
            {
                if (configFact.EndPoint != value)
                {
                    configFact.EndPoint = value;
                    base.OnPropertyChanged("endPoint");

                    ValitationParameters();
                }
            }
        }

        public string pathInput
        {
            get
            {
                return configFact.Input;
            }

            set
            {
                if (configFact.Input != value)
                {
                    configFact.Input = value;
                    base.OnPropertyChanged("pathInput");

                    ValitationParameters();
                }
            }
        }

        public string pathOutput
        {
            get
            {
                return configFact.Output;
            }

            set
            {
                if (configFact.Output != value)
                {
                    configFact.Output = value;
                    base.OnPropertyChanged("pathOutput");

                    ValitationParameters();
                }
            }
        }


        public string pathProcessed
        {
            get
            {
                return configFact.Processed;
            }

            set
            {
                if (configFact.Processed != value)
                {
                    configFact.Processed = value;
                    base.OnPropertyChanged("pathProcessed");

                    ValitationParameters();
                }
            }
        }

        private void ValitationParameters()
        {
            var resultado = configFact.validade();

            messageValidation = "";
            circularVisible = Visibility.Hidden;

            if (string.IsNullOrWhiteSpace(resultado))
            {
                btnEnable = true;
                return;
            }

            messageValidation = resultado;
            btnEnable = false;
        }

        private CommandWrapper _CmdExecute;
        public ICommand CmdExecute
        {
            get
            {
                if (_CmdExecute == null)
                {
                    _CmdExecute = new CommandWrapper(
                        param => Execute()
                    );
                }

                return _CmdExecute;
            }
        }

        private CommandWrapper _CmdSelecionarDirEntrada;
        public ICommand CmdSelecionarDirEntrada
        {
            get
            {
                if (_CmdSelecionarDirEntrada == null)
                {
                    _CmdSelecionarDirEntrada = new CommandWrapper(
                        param => SelecionarDirEntrada()
                    );
                }

                return _CmdSelecionarDirEntrada;
            }
        }

        private void SelecionarDirEntrada()
        {
            pathInput = Util.SelecionarDiretorio();
        }

        private CommandWrapper _CmdSelecionarDirSaida;
        public ICommand CmdSelecionarDirSaida
        {
            get
            {
                if (_CmdSelecionarDirSaida == null)
                {
                    _CmdSelecionarDirSaida = new CommandWrapper(
                        param => SelecionarDirSaida()
                    );
                }

                return _CmdSelecionarDirSaida;
            }
        }

        private void SelecionarDirSaida()
        {
            pathOutput = Util.SelecionarDiretorio();
        }

        private CommandWrapper _CmdSelecionarDirProcessados;
        public ICommand CmdSelecionarDirProcessados
        {
            get
            {
                if (_CmdSelecionarDirProcessados == null)
                {
                    _CmdSelecionarDirProcessados = new CommandWrapper(
                        param => SelecionarDirProcessados()
                    );
                }

                return _CmdSelecionarDirProcessados;
            }
        }

        private void SelecionarDirProcessados()
        {
            pathProcessed = Util.SelecionarDiretorio();
        }

        public async void Execute()
        {
            messageError = String.Empty;
            circularVisible = Visibility.Visible;
            btnEnable = false;

            try
            {
                await docFactService.ExecuteAsync(configFact);

                messageError = "Arquivos processados com sucesso.";
            }
            catch (Exception error)
            {
                messageError = error.Message;
            }
            finally 
            {
                btnEnable = true;
                circularVisible = Visibility.Hidden;
            }
        }
    }
}
