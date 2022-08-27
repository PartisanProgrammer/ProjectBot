using System.Net;
using Newtonsoft.Json;

namespace ProjectBot.Modules.Wikipedia;

public static class Wikipedia{
    public static async Task<Result?> Connect(string searchTerm){
        WebClient client = new WebClient();
        Stream stream = client.OpenRead(
            $"https://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&titles={searchTerm}&exintro=");
        StreamReader reader = new StreamReader(stream);
            
        JsonSerializer ser = new JsonSerializer();
        Result? result = ser.Deserialize<Result>(new JsonTextReader(reader));
        return result;
    }
}