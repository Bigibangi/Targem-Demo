using UnityEngine;

[CreateAssetMenu(menuName = "Settings/SceneSettings", fileName = "SceneSettings")]
public class SceneSettings : ScriptableObject {
    [SerializeField] private GameObject _geometryPrefab;
    [SerializeField] private GameObject _centerOfMassPrefab;
    [SerializeField] private Mesh _geometryMesh;
    [SerializeField] private Material _geometryMaterial;
    [SerializeField, Range(1,100)] private int count;
    [SerializeField, Range(1,5)] private int depth;
    [SerializeField, Range(1,5)] private int childCount;

    public GameObject GeometryPrefab => _geometryPrefab;
    public GameObject CenterOfMass => _centerOfMassPrefab;
    public Mesh GeometryMesh => _geometryMesh;
    public Material GeometryMaterial => _geometryMaterial;
    public int Count => count;
    public int Depth => depth;
    public int ChildCount => childCount;
}