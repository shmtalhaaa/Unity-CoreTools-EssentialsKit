#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class AlwaysShowCameraFrustum : MonoBehaviour
{
    [Header("Per-Camera Settings")]
    [SerializeField] private bool drawGizmos = true;
    [SerializeField] private bool showLabel = true;
    [SerializeField, Range(0.1f, 10f)] private float lineThickness = 1f;
    [SerializeField] private Color gizmosColor = Color.white;

    private Camera cameraComponent;

    // Global toggle for all frustums
    public static bool DrawAllFrustums = true;

    private void OnEnable()
    {
        cameraComponent ??= GetComponent<Camera>();
    }

    private void OnDrawGizmos()
    {
        DrawFrustumIfAllowed();
    }

    private void OnDrawGizmosSelected()
    {
        DrawFrustumIfAllowed();
    }

    private void DrawFrustumIfAllowed()
    {
        // Check per-camera & global toggle
        if (!drawGizmos || !DrawAllFrustums) return;

        // Only draw in SceneView
        if (SceneView.currentDrawingSceneView == null) return;

        cameraComponent ??= GetComponent<Camera>();
        if (cameraComponent == null) return;

        DrawFrustum(cameraComponent, gizmosColor, lineThickness);

        if (showLabel)
        {
            Handles.Label(cameraComponent.transform.position, cameraComponent.name);
        }
    }

    private void DrawFrustum(Camera cam, Color color, float thickness)
    {
        // Frustum dimensions
        float near = cam.nearClipPlane;
        float far = cam.farClipPlane;
        float fov = cam.fieldOfView;
        float aspect = cam.aspect;

        float halfHeightNear = Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad) * near;
        float halfWidthNear = halfHeightNear * aspect;
        float halfHeightFar = Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad) * far;
        float halfWidthFar = halfHeightFar * aspect;

        // 8 corners
        Vector3[] points = new Vector3[8];
        // Near plane
        points[0] = new Vector3(-halfWidthNear, halfHeightNear, near);
        points[1] = new Vector3(halfWidthNear, halfHeightNear, near);
        points[2] = new Vector3(halfWidthNear, -halfHeightNear, near);
        points[3] = new Vector3(-halfWidthNear, -halfHeightNear, near);
        // Far plane
        points[4] = new Vector3(-halfWidthFar, halfHeightFar, far);
        points[5] = new Vector3(halfWidthFar, halfHeightFar, far);
        points[6] = new Vector3(halfWidthFar, -halfHeightFar, far);
        points[7] = new Vector3(-halfWidthFar, -halfHeightFar, far);

        // Transform to world space
        for (int i = 0; i < points.Length; i++)
            points[i] = cam.transform.TransformPoint(points[i]);

        Handles.color = color;

        // Draw near plane
        Handles.DrawAAPolyLine(thickness, points[0], points[1], points[2], points[3], points[0]);
        // Draw far plane
        Handles.DrawAAPolyLine(thickness, points[4], points[5], points[6], points[7], points[4]);
        // Connect near & far planes
        for (int i = 0; i < 4; i++)
            Handles.DrawAAPolyLine(thickness, points[i], points[i + 4]);
    }

    // Optional menu to toggle all frustums
    [MenuItem("Gizmos/Toggle All Camera Frustums")]
    private static void ToggleAllFrustums()
    {
        DrawAllFrustums = !DrawAllFrustums;
        SceneView.RepaintAll();
    }

}
#endif
