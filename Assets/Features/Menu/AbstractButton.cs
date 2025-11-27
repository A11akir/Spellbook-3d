using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> _showList;
    [SerializeField] private List<GameObject> _hideList;

    public void Execute()
    {
        Show();
        Hide();
        OnExecute();
    }

    protected abstract void OnExecute();

    protected void Show()
    {
        foreach (var s in _showList)
            s.SetActive(true);
    }

    protected void Hide()
    {
        foreach (var h in _hideList)
            h.SetActive(false);
    }
}