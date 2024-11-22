﻿using System;
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
    internal class ResponseService
    {
        private HttpClient httpClient;
        private FullResponse fullResponse; //isso aqui é para get byid que só traz uma
        private JsonSerializerOptions jsonSerializerOptions; // configurar/formatar o JSON
        Uri uri = new Uri("http://api.openweathermap.org/data/2.5/weather?q=");
        String key = "&APPID=ab73f17bcbdd1656c1518494388ce6e8";

        public ResponseService()
        {

            httpClient = new HttpClient();
            jsonSerializerOptions = new JsonSerializerOptions
            {
                //propriedades dos serializer options
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };


        }



        public async Task<FullResponse> GetResponseByIdAsync(String cidade) // TASK: usado no await
        {
            Debug.WriteLine("Chamou!! o GetResponseByIdAsync");
            
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"{uri}{cidade}{key}");//quero saber todos os posts;
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();// tranforma o conteudo em string;

                    Debug.WriteLine($"Resposta JSON: {content}");

                    fullResponse = JsonSerializer.Deserialize<FullResponse>(content, jsonSerializerOptions);
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
            
            return fullResponse;
        }


    }
}