using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    [SerializeField] private int _sortingOrderBase = 5000;
    [SerializeField] private int _offset = 0;
    [SerializeField] private bool _runOnlyOnce = false;

    private float _timer;
    private float _timerMax = 0.1f;
    private Renderer _renderer;

    void Start()
    {
        _renderer = gameObject.GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer = _timerMax;
            _renderer.sortingOrder = (int)(_sortingOrderBase - transform.position.y - _offset);
            if (_runOnlyOnce)
            {
                Destroy(this);
            }
        }
    }
}
