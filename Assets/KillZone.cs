using UnityEngine;

public class KillZone : MonoBehaviour
{
    [Tooltip("Destroy the object (true) or just deactivate it (false)")]
    public bool destroyObject = true;

    private void OnTriggerEnter(Collider other)
    {
        Kill(other.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Kill(collision.gameObject);

    }

    private void Kill(GameObject obj)
    {
        if (destroyObject)
        {
            Destroy(obj);
            Debug.Log($"Killed (destroyed): {obj.name}");
        }
        else
        {
            obj.SetActive(false);
            Debug.Log($"Killed (deactivated): {obj.name}");
        }
    }
}
