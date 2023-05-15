using System;
using Unity.Collections;
using Unity.Mathematics;

namespace GameLogic.Core.Components {

    internal struct Model : IDisposable {
        public ModelPart root;
        public int depth;
        public NativeArray<ModelPart>[] parts;
        public NativeArray<float4x4>[] matrices;

        public void Dispose() {
            if (parts != null || matrices != null) {
                for (int i = 0; i < parts.Length; i++) {
                    if (parts[i] != null) {
                        parts[i].Dispose();
                        matrices[i].Dispose();
                    }
                }
            }
        }
    }
}