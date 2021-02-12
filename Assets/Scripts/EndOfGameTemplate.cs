using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* public abstract class EndOfGame : MonoBehaviour {
	public abstract void EndGame();
} */

public interface GameWin  {
	void OnGameWin();
}

public interface GameLoss
{
	void OnGameLoss();
}

/* public interface EndOfGame
{
	void EndGame();
} */