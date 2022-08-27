using Discord.Commands;

namespace ProjectBot.Modules;

public class ScriptCommands: ModuleBase<SocketCommandContext>{
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

Hello {userName}!");}
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
    
    
}