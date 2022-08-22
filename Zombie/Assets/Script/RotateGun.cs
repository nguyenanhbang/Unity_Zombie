using UnityEngine;
using System.Collections;

public class RotateGun : MonoBehaviour {
    public Vector3 target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        LookAtCursor();
	}

    void LookAtCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            target = hit.point;
        }

        transform.LookAt(target);
    }
}
