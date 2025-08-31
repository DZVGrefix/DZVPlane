using UnityEngine;

public class CameraBorder : MonoBehaviour
{
    
    public Color color = Color.yellow;

    void OnDrawGizmos()
    {
        var cam = GetComponent<Camera>();
        if (!cam) return;

        Gizmos.color = color;

        Vector3[] near = new Vector3[4];
        Vector3[] far  = new Vector3[4];

        cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.nearClipPlane,
            Camera.MonoOrStereoscopicEye.Mono, near);
        cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.farClipPlane,
            Camera.MonoOrStereoscopicEye.Mono, far);

        for (int i = 0; i < 4; i++)
        {
            near[i] = transform.TransformPoint(near[i]);
            far[i]  = transform.TransformPoint(far[i]);
        }

        // Near téglalap
        Gizmos.DrawLine(near[0], near[1]);
        Gizmos.DrawLine(near[1], near[2]);
        Gizmos.DrawLine(near[2], near[3]);
        Gizmos.DrawLine(near[3], near[0]);

        // Far téglalap
        Gizmos.DrawLine(far[0], far[1]);
        Gizmos.DrawLine(far[1], far[2]);
        Gizmos.DrawLine(far[2], far[3]);
        Gizmos.DrawLine(far[3], far[0]);

        // Oldalélek
        Gizmos.DrawLine(near[0], far[0]);
        Gizmos.DrawLine(near[1], far[1]);
        Gizmos.DrawLine(near[2], far[2]);
        Gizmos.DrawLine(near[3], far[3]);
    }
}
