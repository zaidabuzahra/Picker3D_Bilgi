using Managers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UIEventSubscriber : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private UIEventSubscitionTypes type;
    [SerializeField] private Button button;

    #endregion

    #region Private Variables

    [ShowInInspector] private UIManager _manager;

    #endregion

    #endregion

    private void Awake()
    {
        FindReferences();
    }

    private void FindReferences()
    {
        _manager = FindObjectOfType<UIManager>();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        switch (type)
        {
            case UIEventSubscitionTypes.OnPlay:
                {
                    button.onClick.AddListener(_manager.Play);
                    break;
                }
            case UIEventSubscitionTypes.OnNextLevel:
                {
                    button.onClick.AddListener(_manager.NextLevel);
                    break;
                }
            case UIEventSubscitionTypes.OnRestartLevel:
                {
                    button.onClick.AddListener(_manager.RestartLevel);
                    break;
                }
        }
    }

    private void UnSubscribeEvents()
    {
        switch (type)
        {
            case UIEventSubscitionTypes.OnPlay:
                {
                    button.onClick.RemoveListener(_manager.Play);
                    break;
                }
            case UIEventSubscitionTypes.OnNextLevel:
                {
                    button.onClick.RemoveListener(_manager.NextLevel);
                    break;
                }
            case UIEventSubscitionTypes.OnRestartLevel:
                {
                    button.onClick.RemoveListener(_manager.RestartLevel);
                    break;
                }
        }
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}