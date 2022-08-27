using System.Text.RegularExpressions;
using Discord;
using Discord.Commands;
using Discord.Rest;
using ProjectBot.Modules.Wikipedia;

namespace ProjectBot.Modules;

public class DemonCommands: ModuleBase<SocketCommandContext>{
    
    [Command("Sacrifice")]
    public async Task Sacrifice(IGuildUser user = null){
        if (user == null){
            await ReplyAsync("Please specify who you want to sacrifice");
            return;
        }

        var deathCauses = new DeathCauses();
        await ReplyAsync($"Sacrificing {user.Mention} by means of {deathCauses.getCause()}");
    }
    
    [Command("Demon Wiki")]
    public async Task DemonWiki(string searchTerm){
        
        var result = await Wikipedia.Wikipedia.Connect(searchTerm);
            
        foreach(Page page in result.query.pages.Values){
            var extract = page.extract;
            var extractWithoutSpecialCharacters = Regex.Replace(extract, @"<[^>]*>", "");

            var finalString = DemonAlphabet.ConvertToDemonSpeech(extractWithoutSpecialCharacters);
                
            await Discord.SplitMessageAndModify(Context,finalString);
        }
    }
    
    [Command("Ritual")] //TODO: Remove? Rework?
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
    
    [Command("Demon Poem")]
    public async Task DemonPoem(){
        await Context.Channel.SendMessageAsync("Meop Nomed.") .ContinueWith(async (msg) => {
            await TempSentenceClass.LoopEdit(msg);
        });
    }
    
    [Command("Penance")]
    public async Task Penance(){
        var id=  Context.Message.Author.Id;
        var userName = Context.Guild.GetUser(id).Nickname;
        await (ReplyAsync($"{userName} lashes themself with a whip"));
            
    }
    
    
    [Command("atEveryone")] //TODO: Remove or Rework into a targeting a specific person.
    public async Task Everyone(){
        await ReplyAsync("@everyone\r\n"+
                         "You Have Been Summoned For A Grand Ritual!\r\n\r\n"+
                         "You Are To Perform The Following Rite:\r\n" +
                         "   1. Join the lounge\r\n" +
                         "   2. Write 666 Sacrifice @Jesper D Engineer\r\n" +
                         "   3. Finish the project.\r\n");
    }
    
    //List every command here
    [Command("Demon help")]
    public async Task Help(){
        await ReplyAsync("Demon Commands:\r\n" +
                         "   Sacrifice\r\n" +
                         "   Demon Wiki\r\n" +
                         "   Ritual\r\n" +
                         "   Demon Poem\r\n" +
                         "   Penance\r\n" +
                         "   atEveryone\r\n");
    }
}

public class TempSentenceClass{
    
    public static async Task LoopEdit(Task<RestUserMessage> task){
        while (true){
            await Task.Delay(2000);
            await task.Result.ModifyAsync(m => { m.Content = CreateSentence(); });
        }

    }

//Return a word made out of random characters
    // - length is random between 3 and 10
    // - each character is a random character from the alphabet
    static string ReturnWord(){
        var word = DemonAlphabet.GetRandomLetter();
        //Sets amount of letters in a word
        for (int i = 0; i < Random.Shared.Next(3,8); i++){
            //Sets space between letters
            for (int x = 0; x <1; x++){
                word += " ";
            }
            word += DemonAlphabet.GetRandomLetter();
            
        }
        return word;
    }

    static string CreateSentence(){
        var loopString = ReturnWord();
        
        for (int i = 0; i <1; i++){
            loopString += " ";
            loopString += " ";
            loopString += " ";
            loopString += " ";
            loopString += ReturnWord();
        }

        //Sets space between words and words in a sentence.
        for (int i = 0; i < 3; i++){
            
            loopString += "\r\n";
            loopString += ReturnWord();
            
        }
        return loopString;
        
    }
}