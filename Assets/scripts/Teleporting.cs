using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Teleporting : MonoBehaviour
{
    [SerializeField] Transform home;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "corner")
        {
            //transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            transform.position = home.position;
        }else if(collision.gameObject.name == "Sphere")
        {
            StartCoroutine("MoveTo", 3.0f);
        }
    }

    IEnumerator MoveTo(float interval)
    {
        //start
        Debug.Log("start");
        //sleep
        yield return new WaitForSeconds(interval);
        //wake up
        Debug.Log("wake up");
        transform.position = home.position;
    }
}
