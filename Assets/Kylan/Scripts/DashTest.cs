using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTest : MonoBehaviour
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private Rigidbody player;
    //[SerializeField] private float dashCooldown;
    //private float times = 1;

    [Range(0, 1.5f)] public float charge = 1.5f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hi");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (charge > 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                //times = dashCooldown;
                player.AddForce(transform.forward * dashSpeed, ForceMode.Force);
                charge = charge - Time.fixedDeltaTime;
            }
        }
        
        if (!Input.GetKey(KeyCode.Space))
        {
            charge = charge + Time.fixedDeltaTime;
        }
        

    }
}
