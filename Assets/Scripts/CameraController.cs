using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float maxRayDistance = 30.0f;

    private Camera _mainCamera;
    private bool _isHitARObject = false;
    public bool IsHitARObject { get { return _isHitARObject; } }

    void Start ()
    {
        _mainCamera = Camera.main;
	}
	
	void Update ()
    {
        //use center of screen for focusing
        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0f);

        Ray ray = Camera.main.ScreenPointToRay(center);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRayDistance))
        {
            _isHitARObject = (hit.collider.tag == "ARObject");
        }
    }

}
