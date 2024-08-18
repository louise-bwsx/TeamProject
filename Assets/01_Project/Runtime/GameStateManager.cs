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
    [field: SerializeField] public bool IsMobile { get; private set; }

    protected override void OnAwake()
    {
        IsMobile = true;
    }

    public void ChangState(GameState changeState)
    {
        CurrentState = changeState;
    }
}
