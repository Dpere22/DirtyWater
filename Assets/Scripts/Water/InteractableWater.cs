// using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// using UnityEditor;
// using UnityEngine.UIElements;
// using UnityEditor.UIElements;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(EdgeCollider2D))]
[RequireComponent(typeof(WaterTriggerHandler))]
public class InteractableWater : MonoBehaviour
{
    private static readonly int LineColor = Shader.PropertyToID("_LineColor");
    private static readonly int BodyColor = Shader.PropertyToID("_BodyColor");
    private static readonly int CausticColor = Shader.PropertyToID("_CausticColor");
    [FormerlySerializedAs("_spriteConstant")] [Header("Springs")] [SerializeField] private float spriteConstant = 1.4f;
    [FormerlySerializedAs("_damping")] [SerializeField] private float damping = 1.1f;
    [FormerlySerializedAs("_spread")] [SerializeField] private float spread = 6.5f;
    [FormerlySerializedAs("_wavePropogationIterations")] [SerializeField, Range(1, 10)] private int wavePropogationIterations = 8;
    [FormerlySerializedAs("_speedMult")] [SerializeField, Range(0f, 20f)] private float speedMult = 5.5f;

    [FormerlySerializedAs("ForceMultiplier")] [Header("Force")] 
    public float forceMultiplier = 0.2f;
    [FormerlySerializedAs("MaxForce")] [Range(1f, 50f)] public float maxForce = 5f;

    [FormerlySerializedAs("_playerCollisionRadiusMult")] [Header("Collision")] [SerializeField, Range(1f, 10f)]
    private float playerCollisionRadiusMult = 4.15f;

    [SerializeField] private Material mat;
    [SerializeField] private Color dirtyBodyColor;
    [SerializeField] private Color dirtyCausticsColor;
    [SerializeField] private Color healthyBodyColor;
    [SerializeField] private Color healthyCausticsColor;
    
    
    [FormerlySerializedAs("NumOfXVertices")]
    [Header("Mesh Generation")]
    [Range(2, 500)] public int numOfXVertices = 70;

    [FormerlySerializedAs("Width")] public float width = 10f;
    [FormerlySerializedAs("Height")] public float height = 4f;
    [FormerlySerializedAs("WaterMaterial")] public Material waterMaterial;
    private const int NumOfYVertices = 2;
    
    [FormerlySerializedAs("GizmoColor")] [Header("Gizmo")]
    public Color gizmoColor = Color.white;

    [FormerlySerializedAs("_mesh")] public Mesh mesh;
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private Vector3[] _vertices;
    private int[] _topVerticesIndex;

    private EdgeCollider2D _coll;
    

    private class WaterPoint
    {
        public float Velocity, Pos, TargetHeight;
    }
    private readonly List<WaterPoint> _waterPoints = new();
    
    
    
    private void Start()
    {
        _coll = GetComponent<EdgeCollider2D>();
        GenerateMesh();
        CreateWaterPoints();
    }
    private void Reset()
    {
        _coll = GetComponent<EdgeCollider2D>();
        _coll.isTrigger = true;
    }

    private void FixedUpdate()
    {
        for (int i = 1; i < _waterPoints.Count - 1; i++)
        {
            WaterPoint point = _waterPoints[i];

            float x = point.Pos - point.TargetHeight;
            float acceleration = -spriteConstant * x - damping * point.Velocity;
            point.Pos += point.Velocity * speedMult * Time.fixedDeltaTime;
            _vertices[_topVerticesIndex[i]].y = point.Pos;
            point.Velocity += acceleration * speedMult * Time.fixedDeltaTime;
        }

        for (int j = 0; j < wavePropogationIterations; j++)
        {
            for (int i = 1; i < _waterPoints.Count - 1; i++)
            {
                float leftDelta = spread * (_waterPoints[i].Pos - _waterPoints[i - 1].Pos) * speedMult * Time.fixedDeltaTime;
                _waterPoints[i + 1].Velocity += leftDelta;
                
                float rightDelta = spread * (_waterPoints[i].Pos - _waterPoints[i+1].Pos) * speedMult * Time.fixedDeltaTime;
                _waterPoints[i + 1].Velocity += rightDelta;
            }
        }
        mesh.vertices = _vertices;
    }
    public void Splash(Collider2D collision, float force)
    {
        float radius = collision.bounds.extents.x * playerCollisionRadiusMult;
        Vector2 center = collision.transform.position;

        for (int i = 0; i < _waterPoints.Count; i++)
        {
            Vector2 vertexWorldPos = transform.TransformPoint(_vertices[_topVerticesIndex[i]]);
            if (IsPointInsideCircle(vertexWorldPos, center, radius))
            {
                _waterPoints[i].Velocity = force;
            }
        }
    }

    private bool IsPointInsideCircle(Vector2 point, Vector2 center, float radius)
    {
        float distanceSquared = (point - center).sqrMagnitude;
        return distanceSquared <= radius * radius;
    }
    
    public void ResetEdgeCollider()
    {
        _coll = GetComponent<EdgeCollider2D>();
        Vector2[] newPoints = new Vector2[2];
        Vector2 firstPoint = new Vector2(_vertices[_topVerticesIndex[0]].x, _vertices[_topVerticesIndex[0]].y);
        newPoints[0] = firstPoint;
        
        Vector2 secondPoint = new Vector2(_vertices[_topVerticesIndex[^1]].x, _vertices[_topVerticesIndex[^1]].y);
        newPoints[1] = secondPoint;

        _coll.offset = Vector2.zero;
        _coll.points = newPoints;
    }

    public void GenerateMesh()
    {
        mesh = new Mesh();
        
        _vertices = new Vector3[numOfXVertices * NumOfYVertices];
        _topVerticesIndex = new int[numOfXVertices];
        for (int y = 0; y < NumOfYVertices; y++)
        {
            for (int x = 0; x < numOfXVertices; x++)
            {
                float xPos = (x/(float)(numOfXVertices-1))*width-width/2;
                float yPos = (y/(float)(NumOfYVertices-1))*height-height/2;
                _vertices[y * numOfXVertices + x] = new Vector3(xPos, yPos, 0f);

                if (y == NumOfYVertices - 1)
                    _topVerticesIndex[x] = y * numOfXVertices + x;
            }
        }
        
        int[] triangles = new int[(numOfXVertices - 1) * (NumOfYVertices - 1) * 6];
        int index = 0;

        for (int y = 0; y < NumOfYVertices - 1; y++)
        {
            for (int x = 0; x < numOfXVertices - 1; x++)
            {
                int bottomLeft = y * numOfXVertices + x;
                int bottomRight = bottomLeft + 1;
                int topLeft = bottomLeft + numOfXVertices;
                int topRight = topLeft + 1;

                triangles[index++] = bottomLeft;
                triangles[index++] = topLeft;
                triangles[index++] = bottomRight;

                triangles[index++] = bottomRight;
                triangles[index++] = topLeft;
                triangles[index++] = topRight;
            }
        }
        
        //UVs
        Vector2[] uvs = new Vector2[_vertices.Length];
        for (int i = 0; i < _vertices.Length; i++)
        {
            uvs[i] = new Vector2((_vertices[i].x + width / 2) / width, (_vertices[i].y + height / 2) / height);
        }
        if(!_meshRenderer)
            _meshRenderer = GetComponent<MeshRenderer>();
        if(!_meshFilter)
            _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer.material = waterMaterial;

        mesh.vertices = _vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        
        _meshFilter.mesh = mesh;

    }

    private void CreateWaterPoints()
    {
        _waterPoints.Clear();

        foreach (var t in _topVerticesIndex)
        {
            _waterPoints.Add(new WaterPoint{
                Pos = _vertices[t].y,
                TargetHeight = _vertices[t].y
            });
        }
    }
}

// //--------------------------------------------------- CODE FOR ADJUSTING WATER DO NOT SHIP - BUILD WILL CRASH -----------------------------------------------------
//
// [CustomEditor(typeof(InteractableWater))]
// public class InteractableWaterEditor : Editor
// {
//     private InteractableWater _water;
//
//     private void OnEnable()
//     {
//         _water = (InteractableWater)target;
//     }
//
//     public override VisualElement CreateInspectorGUI()
//     {
//         VisualElement root = new VisualElement();
//         InspectorElement.FillDefaultInspector(root, serializedObject, this);
//         root.Add(new VisualElement{style = {height = 10}});
//         Button generateMeshButton = new Button(() => _water.GenerateMesh())
//         {
//             text = "Generate Mesh"
//         };
//         root.Add(generateMeshButton);
//         Button placeEdgeColliderButton = new Button(() => _water.ResetEdgeCollider())
//         {
//             text = "Place Edge Collider"
//         };
//         root.Add(placeEdgeColliderButton);
//         return root;
//     }
//
//     private void ChangeDimensions(ref float width, ref float height, float calculatedWidthMax,
//         float calculatedHeightMax)
//     {
//         width = Mathf.Max(0.1f, calculatedWidthMax);
//         height = Mathf.Max(0.1f, calculatedHeightMax);
//     }
//     private void OnSceneGUI()
//     {
//         Handles.color = _water.gizmoColor;
//         Vector3 center = _water.transform.position;
//         Vector3 size = new Vector3(_water.width, _water.height, 0.1f);
//         Handles.DrawWireCube(center, size);
//         
//         float handleSize = HandleUtility.GetHandleSize(center) * 0.1f;
//         Vector3 snap = Vector3.one * 0.1f;
//         
//         //corner handles
//         Vector3[] corners = new Vector3[4];
//         corners[0] = center + new Vector3(-_water.width / 2, -_water.height / 2, 0); //Btm Left
//         corners[1] = center + new Vector3(_water.width / 2, -_water.height / 2, 0); //Btm right
//         corners[2] = center + new Vector3(-_water.width / 2, _water.height / 2, 0); //Top Left
//         corners[3] = center + new Vector3(_water.width / 2, _water.height / 2, 0); //Top Right
//         
//         EditorGUI.BeginChangeCheck();
//         Vector3 newBottomLeft = Handles.FreeMoveHandle(corners[0], handleSize, snap, Handles.CubeHandleCap);
//         if (EditorGUI.EndChangeCheck())
//         {
//             ChangeDimensions(ref _water.width, ref _water.height, corners[1].x - newBottomLeft.x, corners[3].y - newBottomLeft.y);
//             _water.transform.position += new Vector3((newBottomLeft.x - corners[0].x) / 2, (newBottomLeft.y - corners[0].y) / 2, 0);
//         }
//         
//         EditorGUI.BeginChangeCheck();
//         Vector3 newBottomRight = Handles.FreeMoveHandle(corners[1], handleSize, snap, Handles.CubeHandleCap);
//         if (EditorGUI.EndChangeCheck())
//         {
//             ChangeDimensions(ref _water.width, ref _water.height, newBottomRight.x - corners[0].x, corners[3].y - newBottomRight.y);
//             _water.transform.position += new Vector3((newBottomRight.x - corners[1].x) / 2, (newBottomRight.y - corners[1].y) / 2, 0);
//         }
//         
//         EditorGUI.BeginChangeCheck();
//         Vector3 newTopLeft = Handles.FreeMoveHandle(corners[2], handleSize, snap, Handles.CubeHandleCap);
//         if (EditorGUI.EndChangeCheck())
//         {
//             ChangeDimensions(ref _water.width, ref _water.height, corners[3].x - newTopLeft.x, newTopLeft.y - corners[0].y);
//             _water.transform.position += new Vector3((newTopLeft.x - corners[2].x)/ 2, (newTopLeft.y - corners[2].y) / 2, 0);
//         }
//         EditorGUI.BeginChangeCheck();
//         Vector3 newTopRight = Handles.FreeMoveHandle(corners[3], handleSize, snap, Handles.CubeHandleCap);
//         if (EditorGUI.EndChangeCheck())
//         {
//             ChangeDimensions(ref _water.width, ref _water.height, newTopRight.x - corners[2].x, newTopRight.y - corners[1].y);
//             _water.transform.position += new Vector3((newTopRight.x - corners[3].x)/2, (newTopRight.y - corners[3].y) / 2, 0);
//         }
//
//         if (GUI.changed)
//         {
//             _water.GenerateMesh();
//         }
//     }
// }
