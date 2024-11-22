using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherAPP.Models;

namespace WeatherAPP.Services
{
    internal class InformacoesService
    {
        private HttpClient httpClient;
        private ObservableCollection<Informacoes> informacoes; //isso aui é para um get geral, que traz varia, ou seja uma collection
        private Informacoes informacoe; //isso aqui é para get byid que só traz uma
        private JsonSerializerOptions jsonSerializerOptions; // configurar/formatar o JSON
        Uri uri = new Uri("http://api.openweathermap.org/data/2.5/weather?q=");
        String key = "&APPID=ab73f17bcbdd1656c1518494388ce6e8";

        public InformacoesService()
        {

            httpClient = new HttpClient();
            jsonSerializerOptions = new JsonSerializerOptions
            {
                //propriedades dos serializer options
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };


        }

        public async Task<ObservableCollection<Informacoes>> GetInformacoesAsync() // TASK: usado no await
        {

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);//quero saber todos os posts;
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();// tranforma o conteudo em string;
                    informacoes = JsonSerializer.Deserialize<ObservableCollection<Informacoes>>(content, jsonSerializerOptions);
                }
            }
            catch
            {

            }
            return informacoes;
        }



        public async Task<Informacoes> GetSalaByIdAsync(String cidade) // TASK: usado no await
        {
            Debug.WriteLine("Chamou!! o GetSalaByIdAsync");
            Informacoes sala = new Informacoes
                ();
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"{uri}{cidade}{key}");//quero saber todos os posts;
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();// tranforma o conteudo em string;

                    Debug.WriteLine($"Resposta JSON: {content}");

                    informacoe = JsonSerializer.Deserialize<Informacoes>(content, jsonSerializerOptions);
                }
                else
                {
                    // se der erro na chama da API mostra
                    Debug.WriteLine($"Erro na chamada à API: {response.StatusCode}");
                }
            }
            catch (JsonException ex)
            {
                // se der alguma exeption ai mostra
                Debug.WriteLine($"Exceção ocorrida: {ex.Message}");
            }
            catch (Exception ex)
            {
                // se der alguma exeption ai mostra
                Debug.WriteLine($"Exceção ocorrida: {ex.Message}");
            }
            
            return informacoe;
        }


    }
}
