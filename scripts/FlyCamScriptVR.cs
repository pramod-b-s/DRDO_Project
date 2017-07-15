using UnityEngine;
using System.Collections;

public class FlyCamScriptVR : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    private float yaw = 0f;
    private float pitch = 0f;
    public int speed = 1000;
    private int rot=0,f=0;

    float cameraDistanceMax = 20f;
    float cameraDistanceMin = 5f;
    float cameraDistance = 10f;
    float scrollSpeed = 0.5f;
    float fov,initFOV,minfov=15f,maxfov=90f;
    public float sensitivity;
    private int flag = 0;
    //public Camera cam;

    public GameObject ch1, ch2, ch3, ch4, periscope;

    private void Start()
    {
        initFOV = Camera.main.fieldOfView;
        //cam = GetComponent<Camera>();
        
        GameObject.FindGameObjectWithTag("sub_player").GetComponent<Renderer>().enabled = false;

        ch1.GetComponent<Renderer>().enabled = false;
        ch2.GetComponent<Renderer>().enabled = false;
        ch3.GetComponent<Renderer>().enabled = false;
        ch4.GetComponent<Renderer>().enabled = false;
        
    }
    

    private void Update()
    {
       
        if (Input.GetButton("Fire2"))
        {
            if (flag == 0)
            {
                Camera.main.transform.position += Camera.main.transform.forward * 200;
                periscope.GetComponent<Renderer>().enabled = false;
                flag = 1;
            }
            
            GameObject.FindGameObjectWithTag("sub_player").GetComponent<Renderer>().enabled = true;

            ch1.GetComponent<Renderer>().enabled = true;
            ch2.GetComponent<Renderer>().enabled = true;
            ch3.GetComponent<Renderer>().enabled = true;
            ch4.GetComponent<Renderer>().enabled = true;
        }
        
        else
        {
            if (flag == 1)
            {
                Camera.main.transform.position -= Camera.main.transform.forward * 200;
                periscope.GetComponent<Renderer>().enabled = true;
                flag = 0;
            }

            ch1.GetComponent<Renderer>().enabled = false;
            ch2.GetComponent<Renderer>().enabled = false;
            ch3.GetComponent<Renderer>().enabled = false;
            ch4.GetComponent<Renderer>().enabled = false;

        }

        
    }

}

