using UnityEngine;

namespace GameLogic.Core.Components {

    internal struct ProceduralView {
        public Mesh mesh;
        public Material material;
        public MaterialPropertyBlock propertyBlock;
        public ComputeBuffer[] _matricesBuffers;
    }
}