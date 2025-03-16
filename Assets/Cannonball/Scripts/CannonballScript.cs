using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballScript : MonoBehaviour
{
    private Rigidbody rb;
    private CannonControlScript cannon;
    public float airSpeed = 10;
    public float explosionPower = 10;
    public float explosionRadius = 10;

    private void Awake() 
    {
        // LESSON 3-7: Add code below. 
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(CannonControlScript cannon, float power, float angle)
    {
        this.cannon = cannon;

        // LESSON 3-7: Add code below. 

        rb.AddRelativeForce(transform.up * power, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // LESSON 3-7: Add code below. 

        float x = Input.GetAxis("Horizontal") * airSpeed;
        float z = Input.GetAxis("Vertical") * airSpeed;
        Vector3 airMovement = new Vector3(x, 0, z);

        rb.AddForce(airMovement);

    }

    private void OnCollisionEnter(Collision other) 
    {
        Explode();
        StartCoroutine(cannon.ReturnCamera());
        Destroy(this.gameObject, 1);
    }

    public void Explode()
    {
        // Get every object (rayhit) that is within range of explosion
        foreach(var rayhit in Physics.SphereCastAll(transform.position,explosionRadius, transform.forward, explosionRadius*2))
        {
            DestructableBuilding block = rayhit.collider.GetComponent<DestructableBuilding>();
            
            // LESSON 3-7: Add code below. 

            if(block && block.gameObject.GetComponent<Rigidbody>())
            {
                block.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, this.transform.position, explosionRadius);
            }

        }
    }
}
