using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (Managers.Player == null) return;
        
        if (other.CompareTag("Player"))
        {
            Transform newPosition = transform;
            newPosition.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
            Managers.Player.UpdateLastAutosavePosition(newPosition.position);
            Debug.Log("Checkpoint.");
        }
    }
}
