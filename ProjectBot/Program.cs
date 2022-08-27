using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using ProjectBot.Private;


class Program
{
    DiscordSocketClient _client;
    CommandService _commands;
    IServiceProvider _services;

    static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();
    
    public async Task RunBotAsync()
    {
        _client = new DiscordSocketClient();
        _commands = new CommandService();

        _services = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .BuildServiceProvider();
        

        _client.Log += _client_Log;

        await RegisterCommandsAsync();

        await _client.LoginAsync(TokenType.Bot, PrivateInfo.token);

        await _client.StartAsync();
        
        //Listens to messages, useful for realtime feedback and responses
        _client.MessageReceived+= AddReaction;

        await Task.Delay(-1);



    }

    public static async Task AddReaction(SocketMessage message){
        if (message.Author.Id == PrivateInfo.BEN_ID){
            //Tea Emoji
             ReactMultipleTimes(message);
        }
        
    }

    static async Task ReactMultipleTimes(SocketMessage message){
        for (int i = 0; i < 10; i++){
            await ReactAndUnReact(message, "\uD83C\uDF75");
        }
    }

    static async Task ReactAndUnReact(SocketMessage message, string emoteString){
        await message.AddReactionAsync(new Emoji(emoteString));
        await Task.Delay(50);
        await message.RemoveAllReactionsAsync();
        await Task.Delay(1000);
    }

    private Task _client_Log(LogMessage arg)
    {
        Console.WriteLine(arg);
        return Task.CompletedTask;
    }

    public async Task RegisterCommandsAsync()
    {
        _client.MessageReceived += HandleCommandAsync;
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
    }

    private async Task HandleCommandAsync(SocketMessage arg)
    {
        var message = arg as SocketUserMessage;
        var context = new SocketCommandContext(_client, message);
        if (message.Author.IsBot) return;

        int argPos = 0;
        if (message.HasStringPrefix("666 ", ref argPos))
        {
            var result = await _commands.ExecuteAsync(context, argPos, _services);
            if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            if (result.Error.Equals(CommandError.UnmetPrecondition)) await message.Channel.SendMessageAsync(result.ErrorReason);
        }
    }
}