public class GameResults{
	public bool gameWon {get;set;}
	public float fuelLeft{get;set;}
	public int successfulLandings{get;set;}
	public int totalLandingPads{get;set;}
	public Scenes gameType{get;}

	public GameResults(Scenes gameType/*, int totalLandingPads*/){
		gameWon = false;
		//this.totalLandingPads=totalLandingPads;
		this.gameType = gameType;
	}
}