using Leopotam.EcsLite;
using UnityEngine;

[RequireComponent(typeof(EntityReference))]
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
                var collisionedPool = world.GetPool<CollisionEventRequest>();
                var collisionRequest = collisionedPool.Add(entity);
                collisionRequest.targetEntityPack = pack;
                entityReference = gameObject.GetComponent<EntityReference>();
                collisionRequest.senderEntityPack = entityReference.entityPack;
            }
        }
    }
}