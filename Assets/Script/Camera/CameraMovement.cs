using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Variables

    private Vector3 _origin;

    private Camera _mainCamera;

    private bool _isDragging;

    private Bounds _cameraBounds;
    private Vector3 _targetPosition;
    private Camera side;
    #endregion

    private void Awake()
    {
        side = GetComponentInChildren<Camera>();
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        var height = _mainCamera.orthographicSize;
        var width = height * _mainCamera.aspect;

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.max.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.max.y - height;

        _cameraBounds = new Bounds();
        _cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0.0f),
            new Vector3(maxX, maxY, 0.0f)
            );
    }

    private void LateUpdate()
    {
        /*
         * Note: Follow the tutorial here: https://youtu.be/CJWRx2qaakg
         * Whatever is following "transform.position" in your camera movement script,
         * set _targetPosition to that value.
         */


        _targetPosition = GetCameraBounds();
        transform.position = _targetPosition;
    }

    private Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(Globals.playerTransform.position.x, _cameraBounds.min.x, _cameraBounds.max.x),
            Mathf.Clamp(Globals.playerTransform.position.y, _cameraBounds.min.y, _cameraBounds.max.y),
            transform.position.z
        );
    }
}
