using System.Diagnostics;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;


namespace ProjectBot.Modules;

public class Commands : ModuleBase<SocketCommandContext>{
    [Command("Marco")]
    public async Task Ping(){
        await ReplyAsync("Polo");
    }
    [Command("Polo")]
    public async Task Polo(){
        await ReplyAsync("Breh... No");
    }
    [Command("Sacrifice")]
    public async Task Sacrifice(IGuildUser user = null){

        if (user == null){
            await ReplyAsync("Please specify who you want to sacrifice");
            return;
        }

        var deathCauses = new DeathCauses();
        await ReplyAsync($"Sacrificing {user.Mention} by means of {deathCauses.getCause()}");
    }
    
    //Create a command that will allow the user to change the bot's nickname
    [Command("ChangeBotNickname")]
    public async Task ChangeBotNickname(string nickname){
        await Context.Guild.CurrentUser.ModifyAsync(x => {
            x.Nickname = nickname + " bot";
        });
        await ReplyAsync($"Changed my nickname to {nickname}");
    }
    [Command("Ritual")]
    public async Task Ritual(){
       
        await ReplyAsync(@"
            Eko, eko, azarak. Eko, eko, zomelak.
            Bagabi lacha bachabe, Lamac cahi achababe.
            Karrellyos.
            Lamac lamac bachalyas.
            Cabahagy sabalyos. Baryolos.
            Lagoz atha cabyolas. Smnahac atha famolas.
            Hurrahya.");
    }
    
    [Command("Penance")]
    public async Task Penance(){
        var id=  Context.Message.Author.Id;
        var userName = Context.Guild.GetUser(id).Nickname;
        await (ReplyAsync($"{userName} lashes themself with a whip"));
            
    }
    
    //Command that @everyone
    [Command("atEveryone")]
    public async Task Everyone(){
        await ReplyAsync("@everyone\r\n"+
                         "You Have Been Summoned For A Grand Ritual!\r\n\r\n"+
                         "You Are To Perform The Following Rite:\r\n" +
                         "   1. Join the lounge\r\n" +
                         "   2. Write 666 Sacrifice @Jesper D Engineer\r\n" +
                         "   3. Finish the project.\r\n");
    }

    [Command("HelloBot")]
    public async Task HelloBot(){
        var id=  Context.Message.Author.Id;
        var userName = Context.Guild.GetUser(id).DisplayName;
        await ReplyAsync($@"
```cs
public static class Program
{{
    public static void Main(string[] args)
    {{
         Console.WriteLine(""Hello {userName}!"");
    }}
}}
```

Hello {userName}!");
    }
    
    [Command("Scriptable Object")]
    public async Task ScriptableObject(){
        await ReplyAsync($@"
```cs
[CreateAssetMenu(fileName = ""New Demon SO"", menuName = ""Hell/Demon"")]
public class DemonSO : ScriptableObject
{{
    [SerializeField] string _name;
    [SerializeField] int _level;
    [SerializeField] int _health;
    [SerializeField] int _mana;
    [SerializeField] int _strength;
    [SerializeField] HumanSO _master;   
}}
```");
    }
    
    [Command("Help")]
    public async Task Help(){
        await ReplyAsync("```cs\r\n" +
                         "Commands:\r\n" +
                         "   1. atEveryone\r\n" +
                         "   2. ChangeBotNickname <nickname>\r\n" +
                         "   3. HelloBot\r\n" +
                         "   4. Poem\r\n" +
                         "   5. Ritual\r\n" +
                         "   6. Scriptable Object\r\n" +
                         "   7. Sacrifice\r\n" +
                         "   8. Penance\r\n" +
                         "   9. Help\r\n" +
                         "   10. Marco\r\n" +
                         "   11. Polo\r\n" +
                         "```");
    }
    
    //Command that can react to a message with a tea emoji
    [Command("Tea")]
    public async Task TeaReaction(){
        var message = await Context.Channel.SendMessageAsync("Tea time!");
        await message.AddReactionAsync(new Emoji("\uD83C\uDF75"));
    }
    
    //Command that listens for all messages by a specific user and reacts to them with a tea emoji


    [Command("Demon Poem")]
    public async Task DemonPoem(){
        await Context.Channel.SendMessageAsync("Meop Nomed.") .ContinueWith(async (msg) => {
            await LoopEdit(msg);
        });
    }


    public async Task LoopEdit(Task<RestUserMessage> task){
        while (true){
            await Task.Delay(2000);
            await task.Result.ModifyAsync(m => { m.Content = SetLoopString(); });
        }

    }

//Return a word made out of random characters
    // - length is random between 3 and 10
    // - each character is a random character from the alphabet
    string ReturnWord(){
        
        // string word = "";
        // for (int i = 0; i < Random.Shared.Next(3,8); i++){
        //     //word += (char)Random.Shared.Next(97, 123);
        //     word+= ScaryAlphabet.alphabet[Random.Shared.Next(0,ScaryAlphabet.alphabet.Count)];
        // }
        return ScaryAlphabet.alphabet[Random.Shared.Next(0,ScaryAlphabet.alphabet.Count)];
    }   
    string SetLoopString(){
        string loopString = "";
        for (int i = 0; i < 5; i++){
            
            for (int y = 0; y < Random.Shared.Next(3,8); y++){
                loopString += ReturnWord() + " ";
            }
            loopString += "\r\n";
        }
        
        
        return loopString;
        
    }
}