using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Makes an object follow the mouse
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class FollowMouse : MonoBehaviour
{
    private const float canvasScale = 100;
    private Vector3 scale = new Vector3(0.1f, 0.15f, 1f);
    [Header("Camera")]
    [SerializeField] private Camera mainCamera;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0;
        rectTransform.SetPositionAndRotation(mousePosition, Quaternion.identity);
    }
}
