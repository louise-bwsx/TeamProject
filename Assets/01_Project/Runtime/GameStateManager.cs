using UnityEngine;
public enum GameState
{
    MainMenu,
    Gaming,
    Pausing,
    Dialogue,
    Loading,
    PlayerDead,
    GameEnd
}

public class GameStateManager : MonoSingleton<GameStateManager>
{
    [field: SerializeField] public GameState CurrentState { get; private set; }
    [field: SerializeField] public bool IsMobile { get; private set; }

    protected override void OnAwake()
    {
        //在Editor中會是false
        IsMobile = Application.isMobilePlatform;
        //IsMobile = true;
    }

    public void ChangState(GameState changeState)
    {
        CurrentState = changeState;
    }

    public bool IsGaming()
    {
        return CurrentState == GameState.Gaming;
    }
}