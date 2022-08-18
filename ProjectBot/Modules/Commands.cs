using System.Diagnostics;
using Discord;
using Discord.Commands;



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
                         "   2. Write 666 Sacrifice @'Jesper\r\n" +
                         "   3. Finish the project.\r\n");
    }

    [Command("HelloBot")]
    public async Task HelloBot(){
        var id=  Context.Message.Author.Id;
        var userName = Context.Guild.GetUser(id).DisplayName;
        Console.WriteLine(userName);
        var helloUser =  $@"Console.WriteLine(""Hello {{{userName}}}"");";
        await ReplyAsync($@"
```cs
public static class Program
{{
    public static void Main(string[] args)
    {{
         Console.WriteLine(""Hello {userName}!"");
    }}
}}
```");
    }
    
    
    
}