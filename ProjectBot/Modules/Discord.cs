using Discord.Commands;

namespace ProjectBot.Modules;

public static class Discord{
    public async static Task SplitMessageAndModify(SocketCommandContext context, string finalString){
        if (finalString.Length > 2000){
            var splitMessage = await SplitMessage(finalString);
            var message = await context.Channel.SendMessageAsync(splitMessage[0]);
            foreach (var splitString in splitMessage){
                await message.ModifyAsync(m => { m.Content = splitString; });
                await Task.Delay(1000);
            }
        }
        else{
            await context.Channel.SendMessageAsync(finalString);
        }
    }

    public static async Task<List<string>> SplitMessage(string? finalString = default){
        var splitMessage = new List<string>();
        if (finalString.Length < 2000){
            splitMessage.Add(finalString);
            return splitMessage;
        }
        
        var splitMessageCount = finalString.Length / 2000;
        for (int i = 0; i < splitMessageCount; i++){
            splitMessage.Add(finalString.Substring(i * 2000, 2000));
        }
        return splitMessage;
    }
}