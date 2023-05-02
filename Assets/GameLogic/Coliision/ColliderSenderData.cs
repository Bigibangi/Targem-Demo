using Leopotam.EcsLite;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[DisallowMultipleComponent]
public class ColliderSenderData : MonoBehaviour {
    [SerializeField] private Collider _collider;

    private void Awake() {
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.TryGetComponent<EntityReference>(out var entityReference)) {
            ref var pack = ref entityReference.entityPack;
            if (pack.Unpack(out var world, out var entity)) {
                Debug.Log(entity.ToString());
                Debug.Log(world.ToString());
            }
        }
    }
}