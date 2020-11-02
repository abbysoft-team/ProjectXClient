﻿using UnityEngine;

class ScrollAndPitch : MonoBehaviour
{
#if UNITY_IOS || UNITY_ANDROID
    public GameObject camera;
    public bool Rotate;
    protected Plane Plane;

    private void Awake()
    {
        if (camera == null)
            camera = Camera.main.gameObject;
    }

    private void Update()
    {

        // Produce sound
        if (Input.GetTouch(0).phase.Equals(TouchPhase.Began))
        {
            SoundManager.instance.PlaySound("click");
        }

        Vector3 pos1b = Vector3.zero;
        Vector3 pos1 = Vector3.zero;

        //Update Plane
        if (Input.touchCount >= 1) {
            Plane.SetNormalAndPosition(transform.up, transform.position);
            pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
        }

        var Delta1 = Vector3.zero;
        var Delta2 = Vector3.zero;

        //Pinch
        if (Input.touchCount >= 2)
        {
            pos1 = PlanePosition(Input.GetTouch(0).position);
            var pos2 = PlanePosition(Input.GetTouch(1).position);
            var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

            var midPoint = (pos1b + pos2) / 2;

            if (Rotate && pos2b != pos2) {
                camera.transform.RotateAround(midPoint, Plane.normal, Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, Plane.normal));
                return;
            }

            //calc zoom
            var zoom = Vector3.Distance(pos1, pos2) /
                       Vector3.Distance(pos1b, pos2b);

            //edge case
            //if (zoom <= 3 || zoom > 5)
            //    return;

            //Move cam amount the mid ray
            camera.transform.position = Vector3.LerpUnclamped(midPoint, camera.transform.position, 1 / zoom);

            return;
        }

        //Scroll
        if (Input.touchCount >= 1)
        {
            Delta1 = PlanePositionDelta(Input.GetTouch(0));
            Delta1 /= Vector3.Distance(pos1, camera.transform.position);
            Delta1 *= 12;
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                camera.transform.Translate(Delta1, Space.World);
        }

    }

    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //not moved
        if (touch.phase != TouchPhase.Moved)
            return Vector3.zero;

        //delta
        var rayBefore = Camera.main.ScreenPointToRay(touch.position - touch.deltaPosition);
        var rayNow = Camera.main.ScreenPointToRay(touch.position);
        if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        //not on plane
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //position
        var rayNow = Camera.main.ScreenPointToRay(screenPos);
        if (Plane.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }
#endif
}