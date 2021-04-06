using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Tayx.Graphy.Utils.NumString;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Displays a countdown and invokes OnCompleted on countdown completed and auto-destructs.
/// </summary>
public class CountdownCanvas : MonoBehaviour
{
    private int currentCountdown = 3;

    private TextMeshProUGUI _textMeshProUGUI;

    public Vector3 StartPos;
    public Vector3 EndPos;
    
    public UnityAction OnCompleted;

    // Start is called before the first frame update
    void Start()
    {
        // Get the TMP component to display the countdown
        _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();

        // Set the start and end positions to tween between
        StartPos = transform.position;
        EndPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);

        // Start loop
        StartCoroutine(nameof(WaitAndCountdown));
    }

    public IEnumerator WaitAndCountdown()
    {
        // Move slightly backward
        transform.DOMove(EndPos, 1);

        // If countdown has ended
        if (currentCountdown == 0)
        {
            // Call the OnCompleted event
            OnCompleted.Invoke();

            // Destroy the countdown
            Destroy(this.gameObject);
        }

        // Update the countdown text
        UpdateText();

        // Wait 1 second
        yield return new WaitForSeconds(1);

        // Count down
        currentCountdown--;

        // Restart at the beginning pos
        transform.position = StartPos;

        // Loop
        StartCoroutine(nameof(WaitAndCountdown));
    }

    // Update is called once per frame
    void UpdateText()
    {
        _textMeshProUGUI.text = currentCountdown.ToStringNonAlloc();
    }
}