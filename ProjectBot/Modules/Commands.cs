using System.Text.RegularExpressions;
using Discord;
using Discord.Commands;
using ProjectBot.Modules.Wikipedia;


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

    //Create a command that will allow the user to change the bot's nickname
    [Command("ChangeBotNickname")]
    public async Task ChangeBotNickname(string nickname){
        await Context.Guild.CurrentUser.ModifyAsync(x => {
            x.Nickname = nickname + " bot";
        });
        await ReplyAsync($"Changed my nickname to {nickname}");
    }

    //Command that can react to a message with a tea emoji
    [Command("Tea")]
    public async Task TeaReaction(){
        var message = await Context.Channel.SendMessageAsync("Tea time!");
        await message.AddReactionAsync(new Emoji("\uD83C\uDF75"));
    }
    
    //Command that reads a wiki page and returns the first paragraph without special characters
    [Command("Wiki")]
    public async Task Wiki(string searchTerm1, string? searchTerm2 = default){
        var searchTerm = searchTerm1;
        if (searchTerm2 != null){
            searchTerm += " " + searchTerm2.ToLower();
        }

        var finalMessage = new List<string>();
        var result = await Wikipedia.Wikipedia.Connect(searchTerm);
        var pageValues = result?.query.pages.Values;
        foreach (Page page in pageValues){
            var extract = page.extract;
            if(extract == null){
                await ReplyAsync("No results found");
                return;
            }
            var extractWithoutSpecialCharacters = Regex.Replace(extract, @"<[^>]*>", "").TrimStart();
            
            //Splits the string into an array of strings, 2000 chars long.
            finalMessage = await Discord.SplitMessage(extractWithoutSpecialCharacters);

            if(finalMessage[0]==""){
                await ReplyAsync("No results found");
                return;
            }
            foreach (var splitString in finalMessage){
                await ReplyAsync(splitString);
            }
            
        }
           
    }
    
    //List every command here
    [Command("Help")]
    public async Task Help(){
        await ReplyAsync("Normal Commands: \n" +
                         "Marco - returns Polo \n" +
                         "Polo - returns Breh... No \n" +
                         "ChangeBotNickname - changes the bot's nickname \n" +
                         "Tea - reacts to a message with a tea emoji \n" +
                         "Wiki - returns the first paragraph of a wikipedia page \n" +
                         "Help - returns a list of commands");
    }
}