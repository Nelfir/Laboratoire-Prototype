﻿using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Microsoft.MixedReality.Toolkit.Experimental.UI
{
    /// <summary>
    /// Adds touch events to the NonNativeKeyboard buttons (and a tap sound)
    /// </summary>
    public class NonNativeKeyboardTouchAssistant2 : MonoBehaviour
    {
        [SerializeField] private AudioClip clickSound = null;

        private AudioSource clickSoundPlayer;

        private void Start()
        {
            if (CoreServices.InputSystem is IMixedRealityCapabilityCheck capabilityChecker && capabilityChecker.CheckCapability(MixedRealityCapability.ArticulatedHand))
            {
                EnableTouch();
            }
        }

        private void EnableTouch()
        {
            clickSoundPlayer = gameObject.AddComponent<AudioSource>();
            clickSoundPlayer.playOnAwake = false;
            clickSoundPlayer.spatialize = true;
            clickSoundPlayer.clip = clickSound;
            var buttons = GetComponentsInChildren<Button>();
            foreach (var button in buttons)
            {
                var ni = button.gameObject.EnsureComponent<NearInteractionTouchableUnityUI>();
                ni.EventsToReceive = TouchableEventType.Pointer;
                button.onClick.AddListener(PlayClick);
            }
        }

        private void PlayClick()
        {
            if (clickSound != null)
            {
                clickSoundPlayer.Play();
            }
        }
    }
}