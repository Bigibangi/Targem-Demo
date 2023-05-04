using GameLogic.Gravity.Components;
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
                var gravityPool = world.GetPool<GravitySource>();
                if (gravityPool.Has(entity)) return;
                var collisionedPool = world.GetPool<CollisionEventRequest>();
                var component = collisionedPool.Add(entity);
                component.targetGameobject = pack;
                component.contactPoint = other.GetContact(0);
                if (gameObject.TryGetComponent(out entityReference)) {
                    component.senderGameobject = entityReference.entityPack;
                }
            }
        }
    }
}