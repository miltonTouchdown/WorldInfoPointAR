using Mapbox.Unity.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityARInterface;
using UnityEngine;

public class TouchScreen : MonoBehaviour {

    private float _delayTouch = .7f;
    private float _currTime = 0f;
    public float maxRayDistance = 30.0f;

    void Start ()
    {
	}

    void Update()
    {
        _currTime += Time.deltaTime;
 
        if (Input.touchCount > 0 && _currTime > _delayTouch && APPManager.Instance.CurrStatus == StatusAPP.CreateObjectLocation)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxRayDistance))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("ARGameObject"))
                {
                    ObjectLocationManager.Instance.createInterestPoint();
                }
                if(hit.collider.tag == "ARObject")
                {
                    hit.collider.GetComponent<MessageLocation>().edit();
                }
            }

            _currTime = 0f;
        }
	}
}
