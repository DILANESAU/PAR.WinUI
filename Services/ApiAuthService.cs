using Newtonsoft.Json;
using PAR.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PAR.WinUI.Services
{
    internal class ApiAuthService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl = "http://localhost:5198/api/";

        public async Task<UsuarioModel> HacerLoginAsync(string username, string password)
        {
            try
            {
                var request = new { Usuario = username, Password = password };
                var jsonRequest = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");


                HttpResponseMessage response = await _httpClient.PostAsync($"{_baseUrl}auth/login", content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorDetalle = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"❌ [API RECHAZÓ EL PAQUETE] Detalles: {errorDetalle}");
                    return null;
                }

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UsuarioModel>(jsonResponse);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
