using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserScript : MonoBehaviour
{
    [Header("Settings")]
    public LayerMask layerMask; // Set this to include the layers for surfaces you want the laser to interact with
    public float defaultLength = 50f;
    public int numberOfReflections;

    private Ray ray;
    private LineRenderer _lineRenderer;
    private RaycastHit hit;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        ReflectLaser();
    }

    void ReflectLaser()
    {
        ray = new Ray(transform.position, transform.forward);
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);
        float remainingLength = defaultLength;

        for (int i = 0; i < numberOfReflections; i++)
        {
            // Check if the ray intersects any objects
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength, layerMask))
            {
                // Check if the hit object is a lightbulb
                if (hit.collider.CompareTag("Lightbulb")) // Make sure your lightbulb has the "Lightbulb" tag
                {
                    // Change the color of the lightbulb
                    LightbulbColorChange lightbulb = hit.collider.GetComponent<LightbulbColorChange>();
                    if (lightbulb != null)
                    {
                        lightbulb.ChangeColor(Color.green); // Change color to green
                    }

                    // Move the ray forward by a small amount to continue checking for reflections
                    ray = new Ray(hit.point + ray.direction * 0.01f, ray.direction); // Slightly offset to avoid hitting the same collider
                }
                else
                {
                    // Reflect the laser if the hit object is not a lightbulb
                    _lineRenderer.positionCount += 1;
                    _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);
                    remainingLength -= Vector3.Distance(ray.origin, hit.point);
                    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal)); // Reflect the laser
                }
            }
            else
            {
                // No hit detected, extend the laser to its maximum length
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, ray.origin + (ray.direction * remainingLength));
                break; // Exit the loop if no hit
            }
        }

        // If the laser continues beyond the last hit, add its final position
        if (remainingLength > 0)
        {
            _lineRenderer.positionCount += 1;
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, ray.origin + (ray.direction * remainingLength));
        }
    }
}