using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using WeatherAPP.Models;
using WeatherAPP.Services;

namespace WeatherAPP.ViewModels
{
    partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        public String cidade;

        [ObservableProperty]
        public String descricao;

        [ObservableProperty]
        public String temperatura;

        [ObservableProperty]
        public String temperaturaMin;

        [ObservableProperty]
        public String temperaturaMax;

        [ObservableProperty]
        public String sensacao;

        [ObservableProperty]
        public String umidade;

        private ResponseService responseService = new ResponseService();

        public ICommand BuscarCommand { get; set; }
        public MainViewModel()
        {
            BuscarCommand = new Command(Buscar);
        }

        public async void Buscar()
        {
            if (Cidade==null)
            {
                Debug.WriteLine("Cidade ta nulo");
                return;
            }
            FullResponse fullResponse = await responseService.GetResponseByCidadeAsync(Cidade);

            Descricao = "Descrição: " + fullResponse.Weather[0].Description;
            Temperatura = "Temperatura atual: " + fullResponse.Main.Temp.ToString() + " °C";
            TemperaturaMax = "Temperatura máxima: " + fullResponse.Main.TempMax.ToString() + " °C";
            TemperaturaMin = "Temperatura mínima: " + fullResponse.Main.TempMin.ToString() + " °C";
            Sensacao = "Sensação térmica: " + fullResponse.Main.FeelsLike.ToString() + " °C";
            Umidade = "Umidade: " + fullResponse.Main.Humidity.ToString() + "%";
            
        }
    }
}
