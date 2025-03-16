using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControlScript : MonoBehaviour
{
    public Camera cam;
    public GameObject cannonBarrel;
    public GameObject cannonBase;
    public Transform firepoint;
    public GameObject cannonBallPrefab;

    public float minAngle = 10;
    public float maxAngle = 80;

    public float minPower = 5;
    public float maxPower = 20;

    public float currentAngle = 75;
    public float currentPower = 10;


    private float powerChangerValue = 0.25f;
    private bool hasFired = false;

    void Start()
    {
        currentAngle = Mathf.Clamp(cannonBarrel.transform.localEulerAngles.x, minAngle, maxAngle);
        hasFired = false;
        if(cam == null)
        {
            cam = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveBarrel = Input.GetAxis("Vertical");
        float moveBase = Input.GetAxis("Horizontal");

        currentAngle = Mathf.Clamp(cannonBarrel.transform.localEulerAngles.x + moveBarrel, minAngle, maxAngle);

        cannonBarrel.transform.localEulerAngles =  new Vector3(
            currentAngle,
            cannonBarrel.transform.localEulerAngles.y,
            cannonBarrel.transform.localEulerAngles.z
            );
        cannonBase.transform.localEulerAngles += Vector3.up * moveBase;

        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentPower = Mathf.Clamp(currentPower + powerChangerValue, minPower, maxPower);
        }
        if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            currentPower = Mathf.Clamp(currentPower - powerChangerValue, minPower, maxPower);
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            GetComponent<CannonPathLine>()?.ToggleLine();
        }

        if(Input.GetKeyDown(KeyCode.Space) && !hasFired)
        {
            FireCannonball();
        }
    }

    public void FireCannonball()
    {
        hasFired = true;
        GameObject cannonball =  Instantiate(cannonBallPrefab);
        cam.GetComponent<ThirdPersonOrbitCamBasic>().player = cannonball.transform;
        cannonball.transform.position = firepoint.position;
        cannonball.transform.rotation = firepoint.rotation;
        cannonball.GetComponent<CannonballScript>()?.Launch(this, currentPower, currentAngle);
    }

    public IEnumerator ReturnCamera()
    {
        cam.GetComponent<ThirdPersonOrbitCamBasic>().player = this.transform;
        while(false)
        {
            yield return new WaitForFixedUpdate();
        }
        hasFired = false;
    }
}
