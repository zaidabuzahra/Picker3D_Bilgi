using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Enums;

public class CoreGameSignals : MonoBehaviour
{
    #region Singleton

    public static CoreGameSignals Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    public UnityAction<GameState> onChangeGameState = delegate { };
    public UnityAction<int> onLevelInitialize = delegate { };
    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onLevelFailed = delegate { };
    public UnityAction onLevelSuccessful = delegate { };
    public UnityAction onNextLevel = delegate { };
    public UnityAction onRestartLevel = delegate { };
    public UnityAction onReset = delegate { };
}
