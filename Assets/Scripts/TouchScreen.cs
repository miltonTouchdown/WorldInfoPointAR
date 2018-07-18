using UnityEngine;
using UnityEngine.EventSystems;

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
            // Check if finger is over a UI element
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                // Se esta tocando la UI
                return;
            }

            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxRayDistance))
            {
                //if (hit.collider.gameObject.layer == LayerMask.NameToLayer("ARGameObject"))
                //{
                //    ObjectLocationManager.Instance.createInterestPoint();
                //}
                if(hit.collider.tag == "ARObject")
                {
                    hit.collider.GetComponent<MessageLocation>().edit();
                }
                else
                {
                    ObjectLocationManager.Instance.createInterestPoint();
                }
            }

            _currTime = 0f;
        }
	}
}
