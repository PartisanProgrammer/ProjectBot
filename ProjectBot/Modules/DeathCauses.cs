namespace ProjectBot.Modules;

public class DeathCauses{
    public List<string> causes = new List<string>();

   // By means of ...[insert death cause]
   public DeathCauses(){
        causes.Add("a holy hand grenade");
        causes.Add("a grenade");
        causes.Add("a molotov cocktail");
        causes.Add("a tear gas grenade");
        causes.Add("drainage of their blood");
        causes.Add("drowning");
        causes.Add("Defenestration");
        causes.Add("a thousand cuts");
        causes.Add("a thousand stabs");
        causes.Add("a thousand slashes");
        causes.Add("satanic ritual gone wrong oh no no no no it's coming for us all now!");
        causes.Add("food poisoning");
        causes.Add("an unknown disease");
        causes.Add("a bug bite");
        causes.Add("a snake bite");
        causes.Add("a bite from a rabid dog");
        causes.Add("a bite from a rabid cat");
        causes.Add("a bite from a rabid bear");
        causes.Add("a bite from a rabid horse");
        causes.Add("a bite from a rabid rabbit");
        causes.Add("an awkward hug");
        causes.Add("a slap");
        causes.Add("thrusting a dagger into their heart, summoning the demon of death");
        causes.Add("thrusting a dagger into their heart, summoning Cthulhu");
        causes.Add("thrusting a dagger into their heart, summoning an unspeakable horror");
        causes.Add("thrusting a dagger into their heart, summoning a... cute caterpillar?");
        causes.Add("eating their heart");
        
    }
   
   public string getCause(){
       return causes[Random.Shared.Next(0, causes.Count-1)];
   }
}