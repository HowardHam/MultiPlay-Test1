using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Photon.PunBehaviour
{
    public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public float m_Speed = 12f;                 // How fast the tank moves forward and back.
    public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.

    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;         // The current value of the movement input.
    private float m_TurnInputValue;             // The current value of the turn input.

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        if(!photonView.isMine)
        {
            enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

    }

    private void FixedUpdate()
    {
        Move();
        Turn();
        
    }

    private void Move()
    {
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    private void Turn()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}
