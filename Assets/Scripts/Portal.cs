using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cube; // Объект для перемещения через портал.
    [SerializeField] private Transform reciever; // Другой портал.

    private bool objectIsOverlapping = false;
    private bool whoUsingTeleport;

    void Update()
    {
        if (objectIsOverlapping)
        {
            Transform _transform;
            if (whoUsingTeleport) _transform = cube.transform;
            else _transform = player.transform;
            
            Vector3 portalToObject = _transform.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToObject);

            if (dotProduct < 0f) // true - происходит проход через портал.
            {
                float rotationDifference = Quaternion.Angle(transform.rotation, reciever.rotation); // Телепортация.
                rotationDifference -= 180;
                
                if (!whoUsingTeleport) player.transform.Rotate(Vector3.up, rotationDifference); // Игрок или предмет - поворот.
                else cube.transform.Rotate(Vector3.up, rotationDifference); // Ниже такое же условие для позиции.

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDifference, 0f) * portalToObject;

                if (!whoUsingTeleport) player.transform.position = reciever.position + positionOffset; 
                else cube.transform.position = reciever.position + positionOffset;

                objectIsOverlapping = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            objectIsOverlapping = true;
            whoUsingTeleport = false;
        }
        else if (other.gameObject == cube)
        {
            objectIsOverlapping = true;
            whoUsingTeleport = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player || other.gameObject == cube)
        {
            objectIsOverlapping = false;
        }
    }
}