﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;

    public void Update()
    {
        // Fix for using both teleportation and moving at the same time
        if (input.axis.magnitude > 0.1f)
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y)) * Time.deltaTime;
            Vector3 horizontalDirection = Vector3.ProjectOnPlane(direction, Vector3.up);

            transform.position += horizontalDirection;
        }
    }
}