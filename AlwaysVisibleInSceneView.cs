using UnityEngine;

[ExecuteInEditMode]
public class AlwaysVisibleInSceneView : MonoBehaviour
{
    [Header("General Settings")]
    public float gizmoSize = 0.5f;
    public bool drawOnSelect = false;
    public bool drawLabel = false;

    [Space(10)]
    [Header("Gizmo Colors")]
    public Color gizmosColor = Color.yellow;
    public Color selectedGizmosColor = Color.cyan;

    // Global toggle for all instances
    public static bool ShowGizmos = true;

    private void OnDrawGizmos()
    {
        if (!ShowGizmos) return;

        Gizmos.color = gizmosColor;
        Gizmos.DrawWireCube(transform.position, Vector3.one * gizmoSize);
        if (drawLabel)
        {
            UnityEditor.Handles.Label(
                transform.position + Vector3.up * (gizmoSize + 0.2f), gameObject.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!ShowGizmos) return;

        if (drawOnSelect)
        {
            Gizmos.color = selectedGizmosColor;
            Gizmos.DrawWireSphere(transform.position, gizmoSize * 1.5f);
        }
    }
}
