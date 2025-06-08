using Entities.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace ConsumoAPI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Teste Consumo API!");

            Thread.Sleep(10000);

            GetProduto();

            foreach (var item in ListaDeProdutos)
            {
                Console.WriteLine("Codigo: " + item.Id);
                Console.WriteLine("Nome: " + item.Nome);
            }
            
            Console.ReadLine();
            

           // ListarProdutos();
        }

        public static string Token { get; set; }

        public  static List<Produto> ListaDeProdutos { get; set; }

        public static void GetToken()
        {

            string urlApiGeraToken = "https://localhost:5001/api/CreateToken";

            using (var cliente = new HttpClient())
            {
                string login = "usuario@gmail.com";
                string senha = "Senha123!";

                var dados = new
                {
                    Email = login,
                    Password = senha
                };

                string JsonObjeto = JsonConvert.SerializeObject(dados);    
                var content = new StringContent(JsonObjeto, Encoding.UTF8, "application/json");


                var resultado = cliente.PostAsync(urlApiGeraToken, content);
                resultado.Wait();
                if (resultado.Result.IsSuccessStatusCode)
                {
                    var tokenJson = resultado.Result.Content.ReadAsStringAsync();
                    Token = JsonConvert.DeserializeObject(tokenJson.Result).ToString();
                }
            }

        }

        public static void GetProduto()
        {
            GetToken();
            if (!string.IsNullOrWhiteSpace(Token))
            {
                using (var cliente = new HttpClient())
                {
                    string UrlListaProdutos = "https://localhost:5001/api/ListaProdutos";
                    cliente.DefaultRequestHeaders.Clear();
                    cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
                    var response = cliente.GetStringAsync(UrlListaProdutos);
                    response.Wait();
                    var listaRetorno = JsonConvert.DeserializeObject<Produto[]>(response.Result).ToList();

                    ListaDeProdutos = listaRetorno;
                }
            }
        }


        private static List<Produto> ListarProdutos()
        {
            string urlApiGeraToken = "https://localhost:5001/api/ListaProdutos";
            string retornoStringAPI = ChamadaApis(HttpMethod.Get, urlApiGeraToken, null, true);
            var objRetorno = JsonConvert.DeserializeObject<List<Produto>>(retornoStringAPI);

            return objRetorno;
        }

        private static string ChamadaApis(HttpMethod tipoHttpMethod, string api, object objeto, bool usarLogin = false)
        {
            string retorno = String.Empty;
            string login = "valdir@valdir.com";
            string senha = "@Michelle2";

            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(tipoHttpMethod, api))
                {
                    if (usarLogin)
                    {
                        var token = GerarToken(login, senha);
                        if (token != null)
                        {
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        }
                    }
                    client.Timeout = new TimeSpan(0, 0, 100);

                    if (objeto != null)
                    {
                        string dadoSerializado = JsonConvert.SerializeObject(objeto);
                        request.Content = new StringContent(dadoSerializado, Encoding.UTF8, "application/json");
                    }

                    var response = client.SendAsync(request).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        retorno = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }

            return retorno;
        }
        private static string GerarToken(string login, string senha)
        {
            string urlApiGeraToken = "https://localhost:5001/api/CreateToken";

            var dados = new
            {
                Email = login,
                Password = senha
            };

            string retornoStringAPI = ChamadaApis(HttpMethod.Post, urlApiGeraToken, dados, false);
            var tokenRetorno = JsonConvert.DeserializeObject<string>(retornoStringAPI);
            return tokenRetorno;
        }
    }
}
