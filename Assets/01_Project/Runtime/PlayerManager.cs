using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    [field: SerializeField] public GameObject Player { get; private set; }
}
