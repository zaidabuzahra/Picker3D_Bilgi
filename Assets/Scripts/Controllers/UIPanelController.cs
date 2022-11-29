using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class UIPanelController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private List<Transform> layers = new List<Transform>();

    #endregion
    #endregion

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void SubscribeEvents()
    {

    }

    private void UnSubscribeEvents()
    {

    }

    [Button(name:"OpenPanel")]
    private void OnOpenPanel(UIPanelTypes type, int layersPos)
    {
        Instantiate(Resources.Load<GameObject>($"Screens/{type}Panel"), layers[layersPos]);
    }
    [Button(name:"ClosePanel")]
    private void OnClosePanel(int layerPos)
    {
        if (layers[layerPos].transform.childCount > 0)
            Destroy(layers[layerPos].GetChild(0).gameObject);
        
    }

    private void OnCloseAllPanels()
    {
        for(int i = 0; i < layers.Count; i++)
        {
            if (layers[i].transform.childCount > 0)
                Destroy(layers[i].GetChild(0).gameObject);
        }
    }
}
