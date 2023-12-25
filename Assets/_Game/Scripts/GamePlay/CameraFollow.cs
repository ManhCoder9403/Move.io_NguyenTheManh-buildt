using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed;
    private Transform _thisTransform;

    private void Awake()
    {
        _thisTransform = this.transform;
    }
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(_thisTransform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
            _thisTransform.position = smoothedPosition;
            _thisTransform.LookAt(target.position);
        }
    }
    public void ZoomOut(int playerLevel)
    {
        this.offset = Vector3.Lerp(offset, offset * 2, 1f / (11 - (playerLevel / 4)));
    }
}
