using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DialRotationHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    // This event echoes the new local angle to which we have been dragged
    public event Action<Quaternion> OnAngleChanged;

    // Snap increment in degrees (set to 5 for 5° steps)
    public float snapIncrement = 1f;

    Quaternion dragStartRotation;
    Quaternion dragStartInverseRotation;


    private void Awake()
    {
        // As an example: rotate the attached object
        OnAngleChanged += (rotation) => transform.localRotation = rotation;
    }


    // This detects the starting point of the drag more accurately than OnBeginDrag,
    // because OnBeginDrag won't fire until the mouse has moved from the point of mousedown
    public void OnPointerDown(PointerEventData eventData)
    {
        dragStartRotation = transform.localRotation;
        Vector3 worldPoint;
        if (DragWorldPoint(eventData, out worldPoint))
        {
            // We use Vector3.forward as the "up" vector because we assume we're working in a Canvas
            // and so mostly care about rotation around the Z axis
            dragStartInverseRotation = Quaternion.Inverse(Quaternion.LookRotation(worldPoint - transform.position, Vector3.forward));
        }
        else
        {
            Debug.LogWarning("Couldn't get drag start world point");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Do nothing (but this has to exist or OnDrag won't work)
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // Do nothing (but this has to exist or OnDrag won't work)
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPoint;
        if (DragWorldPoint(eventData, out worldPoint))
        {
            Quaternion currentDragAngle = Quaternion.LookRotation(worldPoint - transform.position, Vector3.forward);
            if (OnAngleChanged != null)
            {
                // Compute the desired rotation from drag math
                Quaternion desired = currentDragAngle * dragStartInverseRotation * dragStartRotation;

                // Snap the Z (local) rotation to the nearest increment to make rotation discrete
                Vector3 euler = desired.eulerAngles;
                float snappedZ = Mathf.Round(euler.z / snapIncrement) * snapIncrement;
                Quaternion snapped = Quaternion.Euler(euler.x, euler.y, snappedZ);

                OnAngleChanged(snapped);
            }
        }
    }


    // Gets the point in worldspace corresponding to where the mouse is
    private bool DragWorldPoint(PointerEventData eventData, out Vector3 worldPoint)
    {
        return RectTransformUtility.ScreenPointToWorldPointInRectangle(
            GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out worldPoint);
    }
}