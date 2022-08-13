using Discord;
using Discord.Commands;

namespace ProjectBot.Modules;

public class Commands : ModuleBase<SocketCommandContext>{
    [Command("Marco")]
    public async Task Ping(){
        await ReplyAsync("Polo");
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
            x.Nickname = nickname + "bot";
        });
        await ReplyAsync($"Changed my nickname to {nickname}");
    }
    
    

}
