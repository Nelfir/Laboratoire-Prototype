using System.Collections;
using System.Collections.Generic;
using PlanetExpress.Scripts.Planet;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GrabPoint : MonoBehaviour
{
    private bool _isFollowing;


    private Vector3 _originalPos;


    private Quaternion originalOffset;

    public void Start()
    {
        _originalPos = transform.localPosition;
        RegisterGrabEvents();
    }

    private void RegisterGrabEvents()
    {
        InteractableHoverEvents interactable = GetComponent<InteractableHoverEvents>();

        interactable.onAttachedToHand.AddListener(() =>
        {
            originalOffset = PlanetController.Instance.transform.rotation;
            _isFollowing = true;
            Debug.Log("Start following!");
        });

        interactable.onDetachedFromHand.AddListener(() =>
        {
         
            Debug.Log("End following!");
            _isFollowing = false;
            transform.localPosition = _originalPos;
        });
    }

    public void Update()
    {
        if (_isFollowing)
        {
            // Determine which direction to rotate towards
            
            var newOffset = Quaternion.LookRotation(PlanetController.Instance.transform.position - transform.position);
            PlanetController.Instance.transform.rotation = newOffset * Quaternion.Inverse(originalOffset);
        }
    }
}