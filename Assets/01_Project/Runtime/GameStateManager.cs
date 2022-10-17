using UnityEngine;
public enum GameState
{
    MainMenu,
    Gaming,
    Pausing,
    Dialog,
    Loading,
    PlayerDead
}

public class GameStateManager : MonoSingleton<GameStateManager>
{
    [field: SerializeField] public GameState CurrentState { get; private set; }

    public void ChangState(GameState changeState)
    {
        CurrentState = changeState;
    }
}
