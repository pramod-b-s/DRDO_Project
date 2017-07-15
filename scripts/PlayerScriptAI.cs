using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScriptAI : MonoBehaviour {

    public Rigidbody player;
    public int r=10, speed=25, incrementTheta = 5;
    public float toleranceWidth = 1.5f, delay = 0.5f, radius=100f;
    private Vector3 pos, posl, posr;
    private int flag=0, tried=0, findWay = 0;
    private float nextTime, theta=0, thetal, thetar, x, z, xl, xr, zl, zr, y;
    public Text status;

	// Use this for initialization
	void Start () {
        player = GetComponent<Rigidbody>();

        status.text = "";
        theta = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        y = 0;// player.transform.position.y;
        
            if (flag == 0)
            {
                //direct free measure
                x = r * Mathf.Cos(theta * 2 * Mathf.PI / 360);
                z = r * Mathf.Sin(theta * 2 * Mathf.PI / 360);

                pos = new Vector3(x, y, z);
                Ray ray = new Ray(transform.position, pos);

                //left width measure
                thetal = theta - toleranceWidth;
                xl = r * Mathf.Cos(thetal * 2 * Mathf.PI / 360);
                zl = r * Mathf.Sin(thetal * 2 * Mathf.PI / 360);

                posl = new Vector3(xl, y, zl);
                Ray rayl = new Ray(transform.position, posl);

                //right width measure
                thetar = theta + toleranceWidth;
                xr = r * Mathf.Cos(thetar * 2 * Mathf.PI / 360);
                zr = r * Mathf.Sin(thetar * 2 * Mathf.PI / 360);

                posr = new Vector3(xr, y, zr);
                Ray rayr = new Ray(transform.position, posr);


                RaycastHit hit, hitl, hitr;

                if (tried == 0 && Physics.Raycast(ray, out hit, radius) == false && Physics.Raycast(rayl, out hitl, radius) == false
                    && Physics.Raycast(rayr, out hitr, radius) == false)
                {
                    flag = 1;
                    nextTime = Time.time + delay;

                    //theta += 30;

                    player.AddForce(pos * speed, ForceMode.Force);
                }
                else if (tried == 0)
                {
                    theta = (theta + incrementTheta) % 360;
                    if (theta == 359)
                    {
                        ;//status.text="??";//tried = 1;
                    }
                }
                /*else if (tried == 1)
                {
                    if (Physics.Raycast(ray, out hit, 100) == false)
                    {
                        player.AddForce(pos * speed, ForceMode.Force);
                        tried = 0;
                        //player.isKinematic = false;
                    }
                    else
                    {
                        theta = (theta + 1) % 360;
                        if (theta == 359)
                        {
                            status.text="no way out !!";
                        }
                    }
                }*/

            }

            if (flag == 1)
            {
                if (Time.time >= nextTime)
                {
                    //player.AddForce(pos * speed, ForceMode.Force);
                    flag = 0;
                    player.velocity = Vector3.zero;
                }
            }
        }
    
}
