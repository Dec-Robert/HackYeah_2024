using System.Collections.Generic;
using UnityEngine;

public class RopeManager : MonoBehaviour
{
    // Reference to the segment prefab (assign in Unity Inspector)
    public GameObject ropeSegmentPrefab;

    // Distance between each segment
    public float segmentSpacing = 0.5f;

    // Maximum number of segments (adjust with power-ups)
    public int maxSegments = 10;

    // The speed at which segments follow the player
    public float segmentFollowSpeed = 5.0f;

    // Reference to the player
    public Transform player;

    // List to store rope segments
    private List<Transform> segments = new List<Transform>();

    // Tracks the position of the last segment
    private Vector3 lastSegmentPosition;

    void Start()
    {
        // Create the initial segments for the rope
        lastSegmentPosition = player.position;

        for (int i = 0; i < maxSegments; i++)
        {
            AddSegment(lastSegmentPosition);
        }
    }

    void Update()
    {
        // Make the segments follow the player in a chain
        MoveSegments();

        // Drop any segments if the player has no more to extend
        if (segments.Count == 0)
        {
            return; // No segments left to follow the player
        }
    }

    // Method to move the segments behind the player
    void MoveSegments()
    {
        // First segment follows the player directly
        if (segments.Count > 0)
        {
            segments[0].position = Vector3.Lerp(segments[0].position, player.position, segmentFollowSpeed * Time.deltaTime);
        }

        // Each subsequent segment follows the one before it
        for (int i = 1; i < segments.Count; i++)
        {
            Vector3 prevSegmentPos = segments[i - 1].position;
            Vector3 newSegmentPos = Vector3.Lerp(segments[i].position, prevSegmentPos, segmentFollowSpeed * Time.deltaTime);
            float distance = Vector3.Distance(newSegmentPos, prevSegmentPos);

            // Ensure the segment stays spaced properly behind the previous one
            if (distance > segmentSpacing)
            {
                segments[i].position = prevSegmentPos - (prevSegmentPos - segments[i].position).normalized * segmentSpacing;
            }
        }
    }

    // Method to add a new segment (triggered by power-ups)
    public void AddSegment(Vector3 position)
    {
        if (segments.Count >= maxSegments)
        {
            return; // Already at max segments
        }

        // Instantiate a new segment and add it to the list
        GameObject newSegment = Instantiate(ropeSegmentPrefab, position, Quaternion.identity);
        segments.Add(newSegment.transform);
    }

    // Method to handle the power-up logic
    public void CollectPowerUp(int segmentsToAdd)
    {
        for (int i = 0; i < segmentsToAdd; i++)
        {
            AddSegment(segments[segments.Count - 1].position); // Add new segments to the last position
        }

        // Increase the max number of segments if needed
        maxSegments += segmentsToAdd;
    }

    // Method to drop the last segment when reaching the end of the rope
    public void DropLastSegment()
    {
        if (segments.Count > 0)
        {
            // Drop the last segment and remove it from the list
            Transform lastSegment = segments[segments.Count - 1];
            lastSegment.gameObject.AddComponent<Rigidbody>(); // Add physics to make it fall naturally
            segments.RemoveAt(segments.Count - 1);
        }
    }
}
