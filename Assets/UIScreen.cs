using System;
using TMPro;
using UnityEngine;

public class UIScreen : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _timeCount;
    [SerializeField] private TextMeshProUGUI _collisionsCount;

    public Action<int> OnCollisionChanged;

    private int _currentTimeInSeconds;
    private int _currentCollisionsCount;
    private float _tick = 0f;

    private void Awake() {
        OnCollisionChanged += RegisterCollision;
    }

    private void OnValidate() {
        ResetCounts();
    }

    private void Update() {
        if (_timeCount != null) {
            _tick += Time.deltaTime;
            if (_tick >= 1f) {
                _currentTimeInSeconds += (int) _tick;
                _timeCount.text = _currentTimeInSeconds.ToString();
                _tick = 0f;
            }
        }
    }

    private void OnDestroy() {
        OnCollisionChanged -= RegisterCollision;
    }

    private void RegisterCollision(int count) {
        _currentCollisionsCount += count;
        _collisionsCount.text = _currentCollisionsCount.ToString();
    }

    internal void ResetCounts() {
        _currentTimeInSeconds = 0;
        _timeCount.text = "0";
        _currentCollisionsCount = 0;
        _collisionsCount.text = "0";
    }
}