using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{
    public GameObject wall;
    float currentValue = 0f;
    float oldValue = 0f;
    public float maxY;
    public float minY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentValue <= oldValue)
        {
            
            if(wall.transform.position.y > minY + 7)
            {
                wall.transform.position -= new Vector3(0, 0.1f, 0);
            }
        }
        oldValue = currentValue;

    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Player"))
        {
            currentValue += Time.fixedDeltaTime;

            if(wall.transform.position.y < maxY + 7)
            {
                wall.transform.position += new Vector3(0, 0.1f, 0);
            }
            else
            {
                wall.transform.position = new Vector3 (wall.transform.position.x, maxY + 7, wall.transform.position.z);
            }
        }
    }

}
