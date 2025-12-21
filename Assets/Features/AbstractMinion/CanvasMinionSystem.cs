using Features.AbstractMinion;
using UnityEngine;

public class CanvasMinionSystem : MonoBehaviour
{
    private GameObject _enemyPrefab;
    private IHealth _health;

    public void Init(GameObject prefab, IHealth health)
    {
        _enemyPrefab = prefab;
        _health = health;

        transform.localRotation = Camera.main.transform.rotation;
        _health.OnDeath += OnDeath;
        UpdateCanvasPos();
    }

    private void OnDestroy()
    {
        if (_health != null)
            _health.OnDeath -= OnDeath;
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }

    private void Update() => UpdateCanvasPos();

    private void UpdateCanvasPos()
    {
        if (!_enemyPrefab) return;
        transform.position = _enemyPrefab.transform.position;
    }
}