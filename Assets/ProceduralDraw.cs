using GameLogic.Core.Components;
using Leopotam.EcsLite;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class ProceduralDraw : MonoBehaviour {
    [SerializeField] private Mesh _mesh;
    [SerializeField] private Material _material;
    [SerializeField, Range(1,6)] private int _depth = 2;

    private NativeArray<ModelPart>[] _parts;
    private NativeArray<float4x4>[] _matrices;
    private ComputeBuffer[] _matricesBuffers;
    private EntityReference _entityReference;

    private static readonly int matriceID = Shader.PropertyToID("_Matrices");
    private static MaterialPropertyBlock propertyBlock;

    private void OnEnable() {
        if (gameObject.TryGetComponent(out _entityReference)) {
            _entityReference.entityPack.Unpack(out var world, out var entity);
            ref var model = ref world.GetPool<Model>().Get(entity);
            _parts = new NativeArray<ModelPart>[_depth];
            _matrices = new NativeArray<float4x4>[_depth];
            _matricesBuffers = new ComputeBuffer[_depth];
            var stride = 16 * 4;
            for (int i = 0, length = 1; i < _parts.Length; i++, length *= 5) {
                _parts[i] = new NativeArray<ModelPart>(length, Allocator.Persistent);
                _matrices[i] = new NativeArray<float4x4>(length, Allocator.Persistent);
                _matricesBuffers[i] = new ComputeBuffer(_parts.Length, stride);
            }
            _parts[0][0] = model.root;
            model.parts = _parts;
            model.matrices = _matrices;
            for (int li = 1; li < _parts.Length; li++) {
                var levelParts = _parts[li];
                for (int fpi = 0; fpi < levelParts.Length; fpi += 5) {
                    for (int ci = 0; ci < 5; ci++) {
                        levelParts[fpi + ci] = CreatePart(ci);
                    }
                }
            }
        }
        propertyBlock ??= new MaterialPropertyBlock();
    }

    private void OnDisable() {
        for (int i = 0; i < _matricesBuffers.Length; i++) {
            _matricesBuffers[i].Release();
            _parts[i].Dispose();
            _matrices[i].Dispose();
        }
        _parts = null;
        _matrices = null;
        _matricesBuffers = null;
    }

    private void OnValidate() {
        if (_parts != null && enabled) {
            OnDisable();
            OnEnable();
        }
    }

    private void Update() {
        if (gameObject.TryGetComponent<EntityReference>(out var entityReference)) {
            entityReference.entityPack.Unpack(out var world, out var entity);
            var model = world.GetPool<Model>().Get(entity);
            _parts[0][0] = model.root;
            _matrices[0][0] = model.matrices[0][0];

            var bounds = new Bounds(_parts[0][0].worldPosition,Vector3.one * _depth);
            for (int i = 0; i < _matricesBuffers.Length; i++) {
                var buffer = _matricesBuffers[i];
                buffer.SetData(_matrices[i]);
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

    private ModelPart CreatePart(int childIndex) =>
        new ModelPart();
}