using UnityEngine;

public class BaseballDirectionTracker : MonoBehaviour
{
    public Vector3 originalDirection { get; private set; }

    public void SetOriginalDirection(Vector3 dir)
    {
        originalDirection = dir.normalized;
    }
}
