
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform target;   

    public float speed;
    public float timeDelay = 1.5f;
    
    private float curTime;
    private Rigidbody _playerRB;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.RETURN_TO_CHECKPOINT, UpdatePosition);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.RETURN_TO_CHECKPOINT, UpdatePosition);
    }

    void Start()
    {
        _playerRB = GetComponent<Rigidbody>();
        
        curTime = 0;
        
        if (Managers.Player != null) Managers.Player.UpdateLastAutosavePosition(transform.position);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N)) Managers.Mission.ReachObjective(); // Не легальный чит. ¯\_(ツ)_/¯
    }

    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        if (inputHorizontal != 0 || inputVertical != 0)
        {
            movement.x = inputHorizontal * speed;
            movement.z = inputVertical * speed;
            movement = Vector3.ClampMagnitude(movement, speed);

            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0); // С камеры поворот.
            movement = target.TransformDirection(movement);
            movement.Set(movement.x, 0, movement.z);
            target.rotation = tmp;
        }

        UpdatedTimeForScore();

        if (Input.GetButton("Jump")) // Легальный чит. ¯\_(ツ)_/¯
        {
            _playerRB.velocity = Vector3.zero;
            movement = Vector3.zero;
        }

        _playerRB.AddForce(movement * speed);
    }

    /// <summary>
    /// Изменение очков.
    /// </summary>
    private void UpdatedTimeForScore()
    {
        if (Managers.Player != null)
        {
            curTime += Time.deltaTime;
            if (curTime > timeDelay)
            {
                curTime = 0;
                Managers.Player.ChangeScore(-1);
            }
        }
    }

    /// <summary>
    /// Позиция для чекпоинта.
    /// </summary>
    private void UpdatePosition()
    {
        gameObject.transform.position = Managers.Player.lastSavePosition;
        _playerRB.Sleep();
    }
}