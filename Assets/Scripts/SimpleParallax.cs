using UnityEngine;

public class SimpleParallax : MonoBehaviour
{
    public Transform cam; 
    public float[] parallaxFactors; // One factor per child layer

    private Vector3 previousCamPos;

    void Start()
    {
        if (cam == null)
            cam = Camera.main.transform;

        previousCamPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cam.position - previousCamPos;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform layer = transform.GetChild(i);
            float factor = i < parallaxFactors.Length ? parallaxFactors[i] : 1f;

            layer.position += new Vector3(delta.x * factor, delta.y * factor, 0);
        }

        previousCamPos = cam.position;
    }
}
