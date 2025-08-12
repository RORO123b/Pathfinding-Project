using UnityEngine;
using UnityEditor;

public class NodesGenerator : EditorWindow
{
    private float nodesPerUnit = 0.3f;
    private int minNodes = 2;
    private float verticalOffset = 1f;

    [MenuItem("Tools/Platform Node Generator")]
    public static void ShowWindow()
    {
        GetWindow<NodesGenerator>("Platform Node Generator");
    }

    void OnGUI()
    {
        GUILayout.Label("Auto Node Generator", EditorStyles.boldLabel);

        nodesPerUnit = EditorGUILayout.FloatField("Nodes per Unit", nodesPerUnit);
        minNodes = EditorGUILayout.IntField("Minimum Nodes", minNodes);
        verticalOffset = EditorGUILayout.FloatField("Vertical Offset", verticalOffset);

        if (GUILayout.Button("Generate Nodes"))
        {
            GenerateNodes();
        }
    }

    void GenerateNodes()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        if (platforms.Length == 0)
        {
            Debug.LogWarning("No GameObjects found with tag 'Platform'.");
            return;
        }

        GameObject parent = new GameObject("GeneratedNodes");

        foreach (GameObject platform in platforms)
        {
            Renderer renderer = platform.GetComponent<Renderer>();

            float platformWidth = renderer.bounds.size.x;
            int nodeCount = Mathf.Max(minNodes, Mathf.RoundToInt(platformWidth * nodesPerUnit));

            for (int i = 0; i < nodeCount; i++)
            {
                float t = (nodeCount == 1) ? 0.5f : (float)i / (nodeCount - 1); // even spacing
                float xPos = renderer.bounds.min.x + t * platformWidth;
                float yPos = renderer.bounds.max.y + verticalOffset;

                GameObject Node = new GameObject($"Node_{platform.name}_{i}");
                Node.transform.position = new Vector3(xPos, yPos, platform.transform.position.z);
                Node.transform.SetParent(parent.transform);
            }
        }

    }
}
