﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {


    //[SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    //[SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    //[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
    //[SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
    //[SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    //private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    //const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    //private bool m_Grounded;            // Whether or not the player is grounded.
    //private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    //const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    //private Animator m_Anim;            // Reference to the player's animator component.
    //private Rigidbody2D m_Rigidbody2D;
    //private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    //private void Awake()
    //{
    //    // Setting up references.
    //    m_GroundCheck = transform.Find("GroundCheck");
    //    m_CeilingCheck = transform.Find("CeilingCheck");
    //    m_Anim = GetComponent<Animator>();
    //    m_Rigidbody2D = GetComponent<Rigidbody2D>();
    //}


    //private void FixedUpdate()
    //{
    //    m_Grounded = false;

    //    // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
    //    // This can be done using layers instead but Sample Assets will not overwrite your project settings.
    //    Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
    //    for (int i = 0; i < colliders.Length; i++)
    //    {
    //        if (colliders[i].gameObject != gameObject)
    //            m_Grounded = true;
    //    }
    //    m_Anim.SetBool("Ground", m_Grounded);

    //    // Set the vertical animation
    //    m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    //}
}
