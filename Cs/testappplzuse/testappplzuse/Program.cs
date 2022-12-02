

using System.Net.Http.Json;
using System.Text.Json;

namespace testappplzuse
{
    internal class Program
    {
        class pokemon
        {
            private int _hp, _attack;
            public int hp
            {
                get { return _hp;}
                set { _hp = value; }
            }
        }

        async Task<string> get_pokemon()
        {
            var url = "https://pokeapi.co/api/v2/pokemon/totodile";
            var client = new HttpClient();
            var content = await client.GetAsync(url);
            var responseString = await content.Content.ReadAsStringAsync();

            return responseString ;
        }
        

        
         static async Task  Main(string[] args)
        {
           
            //var url = "https://www.boredapi.com/api/activity";
            
            
            



            Console.WriteLine(responseString);



            return;
        }
    }
}