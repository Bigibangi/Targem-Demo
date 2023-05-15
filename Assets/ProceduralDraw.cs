using GameLogic.Core.Components;
using Leopotam.EcsLite;
using UnityEngine;

[DisallowMultipleComponent]
public class ProceduralDraw : MonoBehaviour {
    [SerializeField] private Mesh _mesh;
    [SerializeField] private Material _material;
    [SerializeField, Range(1,6)] private int _depth = 2;

    private ComputeBuffer[] _matricesBuffers;
    private EntityReference _entityReference;

    private static readonly int matriceID = Shader.PropertyToID("_Matrices");
    private static MaterialPropertyBlock propertyBlock;

    private void OnEnable() {
        if (gameObject.TryGetComponent(out _entityReference)) {
            _entityReference.OnEntityChanged += OnValidate;
        }
        _entityReference.entityPack.Unpack(out var world, out var entity);
        if (world != null) {
            ref var model = ref world.GetPool<Model>().Get(entity);
            _depth = model.depth;
            _matricesBuffers = new ComputeBuffer[_depth];
            var stride = 16 * 4;
            for (int i = 0, length = 1; i < model.parts.Length; i++, length *= 5) {
                _matricesBuffers[i] = new ComputeBuffer(model.parts.Length, stride);
            }
        }
        propertyBlock ??= new MaterialPropertyBlock();
    }

    private void OnDisable() {
        _entityReference.OnEntityChanged -= OnValidate;
        if (_matricesBuffers != null) {
            for (int i = 0; i < _matricesBuffers.Length; i++) {
                _matricesBuffers[i].Release();
            }
        }
        _matricesBuffers = null;
    }

    private void OnValidate() {
        if (enabled) {
            OnDisable();
            OnEnable();
        }
    }

    private void Update() {
        if (gameObject.TryGetComponent<EntityReference>(out var entityReference)) {
            entityReference.entityPack.Unpack(out var world, out var entity);
            var model = world.GetPool<Model>().Get(entity);
            var bounds = new Bounds(model.root.worldPosition,Vector3.one * model.depth);
            if (_matricesBuffers == null) { OnValidate(); }
            for (int i = 0; i < _matricesBuffers.Length; i++) {
                var buffer = _matricesBuffers[i];
                buffer.SetData(model.matrices[i]);
                propertyBlock.SetBuffer(matriceID, buffer);
                Graphics.DrawMeshInstancedProcedural(
                    _mesh,
                    0,
                    _material,
                    bounds,
                    buffer.count,
                    propertyBlock);
            }
        }
    }
}