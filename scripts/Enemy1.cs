using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour
{

    public Rigidbody sub, enemy1;
    //private Vector3 subpos, currpos;
    public float tolerantDist = 500, intolerabledist = 20;
    public Text wrn1; 
    public int enemySpeed, diff=2, smartDiff=5, smartDiffOne=2, smartDiffTwo =3, timeToHit=1;
    private float nextTime, initDist, finalDist;
    private float nextTimeSmarti, nextTimeSmartf, nextTimeSmartm, nextTimeSmart;
    private Vector3 initPos, finalPos, myPos, toleranceVector;
    private Vector3 initPosSmart, finalPosSmart, midPosSmart, PosSmart;
    private int flag = 0, flagSmarti=0, flagSmartf = 0, flagSmartm=0, flagSmart=0, switchLoop=0;

    public int r = 100, speed = 5000, incrementTheta = 1;
    public float toleranceWidth = 15f, delay = 0.5f;
    public float radius = 20;
    private Vector3 pos, posl, posr;
    private int flagOther = 0, tried = 0, findWay = 0, ctr=0;
    private float nextTimeOther, theta = 0, thetal, thetar, x, z, xl, xr, zl, zr, y, noWayTime, findWayFlag=0;

    private float nextTimeDownwardForce, DownwardForceFlag = 0, ExtraFlag = 0;
    private float elseFlag=0;

    public float upperTolerance=50;
    private int f = 0, f1 = 0, f2 = 0, f3 = 0, f4 = 0;

    private float[] xvis, zvis;
    //public Text status;

    public static int health1 = 100;
    private float maxRayDistanceNextTime, maxRayDistanceFlag = 0;
    public float maxRayDistance = 50;
    public AudioSource TorpedoImpactClip, missileAttackClip;

    private int dispflag = 0;
    private float dispNextTime;

    public static Vector3 DestroyedAtPos1;
    public static float DestroyedAtTime1;
    private float var;

    // Use this for initialization
    void Start()
    {

        toleranceVector = new Vector3(0.1f, 0.1f, 0.1f);
        wrn1.text = "";
        xvis = new float[10000];
        zvis = new float[10000];
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray raytest = new Ray(transform.position, sub.transform.position - transform.position);
        RaycastHit hittest;
        Debug.DrawRay(transform.position,(sub.transform.position - transform.position).normalized * maxRayDistance, Color.red);
        

        Ray raytestXLower = new Ray(transform.position, sub.transform.position - transform.position + new Vector3(-upperTolerance, 0, 0));
        RaycastHit hittestxl;
        Debug.DrawRay(transform.position, (sub.transform.position - transform.position).normalized * 20 + 
            new Vector3(-upperTolerance, 0, 0), Color.red);

        Ray raytestXUpper = new Ray(transform.position, sub.transform.position - transform.position + new Vector3(upperTolerance, 0, 0));
        RaycastHit hittestxu;
        Debug.DrawRay(transform.position, (sub.transform.position - transform.position).normalized * 20 + 
            new Vector3(upperTolerance, 0, 0), Color.red);

        Ray raytestZLower = new Ray(transform.position, sub.transform.position - transform.position + new Vector3(0, 0, -upperTolerance));
        RaycastHit hittestzl;
        Debug.DrawRay(transform.position, (sub.transform.position - transform.position).normalized * 20 + 
            new Vector3(0, 0, -upperTolerance), Color.red);

        Ray raytestZUpper = new Ray(transform.position, sub.transform.position - transform.position + new Vector3(0, 0, upperTolerance));
        RaycastHit hittestzu;
        Debug.DrawRay(transform.position, (sub.transform.position - transform.position).normalized * 20 + 
            new Vector3(0, 0, upperTolerance), Color.red);


        Ray raytestGoDown = new Ray(transform.position, new Vector3(0, 1, 0));
        RaycastHit hittestgd;
        Debug.DrawRay(transform.position, new Vector3(0, 1, 0), Color.red);

        if ( (Mathf.Abs(sub.transform.position.y - enemy1.transform.position.y) <= 2) && sub.transform.position.y >= -4 
            && DownwardForceFlag==0 && Physics.Raycast(raytestGoDown, out hittestgd, 2) == false)
        {
            enemy1.AddForce(new Vector3(0, -50000, 0), ForceMode.Force);
            nextTimeDownwardForce = Time.time + 2;
            DownwardForceFlag = 1;
        }
        if (Mathf.Abs(sub.transform.position.y - enemy1.transform.position.y) <= 2 && sub.transform.position.y <= -4
            && DownwardForceFlag == 0)
        {
            enemy1.AddForce(new Vector3(0, 50000, 0), ForceMode.Force);
            nextTimeDownwardForce = Time.time + 2;
            DownwardForceFlag = 1;
        }
        if(DownwardForceFlag==1 && Time.time >= nextTimeDownwardForce)
        {
            DownwardForceFlag = 0;
        }

        if(Physics.Raycast(raytest, out hittest, maxRayDistance) == true)
        {
            if (hittest.collider.name == "submarine")
            {
                f = 0;
            }
            else
            {
                f = 1;
            }
        }
        else if(maxRayDistanceFlag == 0)
        {
            maxRayDistanceFlag = 1;
            maxRayDistanceNextTime = Time.time + 2;
            f = 0;
        }
        else
        {
            f = 0;
        }

        if (Time.time >= maxRayDistanceNextTime && maxRayDistanceFlag == 1)
        {
            maxRayDistanceFlag = 0;
            if (maxRayDistance > 20)
            { 
                maxRayDistance -= 40;
            }
        }
    

        if (Physics.Raycast(raytestXLower, out hittestxl, 20) == true)
        {
            if (hittestxl.collider.name == "submarine")
            {
                f1 = 0;
            }
            else
            {
                f1 = 1;
            }
        }
        else
        {
            f1 = 0;
        }
        if (Physics.Raycast(raytestXUpper, out hittestxu, 20) == true)
        {
            if (hittestxu.collider.name == "submarine")
            {
                f2 = 0;
            }
            else
            {
                f2 = 1;
            }
        }
        else
        {
            f2 = 0;
        }
        if (Physics.Raycast(raytestZLower, out hittestzl, 20) == true)
        {
            if (hittestzl.collider.name == "submarine")
            {
                f3 = 0;
            }
            else
            {
                f3 = 1;
            }
        }
        else
        {
            f3 = 0;
        }
        if (Physics.Raycast(raytestZUpper, out hittestzu, 20) == true)
        {
            if (hittestzu.collider.name == "submarine")
            {
                f4 = 0;
            }
            else
            {
                f4 = 1;
            }
        }
        else
        {
            f4 = 0;
        }


        /*if (Physics.Raycast(raytest, out hittest, 200) == true || Physics.Raycast(raytestZLower, out hittestzl, 200) == true
            || Physics.Raycast(raytestZUpper, out hittestzu, 200) == true || Physics.Raycast(raytestXLower, out hittestxl,200) == true 
            || Physics.Raycast(raytestXUpper, out hittestxu,200) == true)*/

        /*hittest.collider.name != "submarine" &&
        (hittest.collider.name == "extra_island" || hittest.collider.name == "extra_island1" 
        || hittest.collider.name == "extra_island2" || hittest.collider.name == "extra_island3" 
        || hittest.collider.name == "extra_island4" || hittest.collider.name == "extra_island4"
        || hittest.collider.name == "extra_island5" || hittest.collider.name == "extra_island6"
        || hittest.collider.name == "extra_island7")) || 
        Physics.Raycast(raytestXLower, out hittestxl) == true || Physics.Raycast(raytestXUpper, out hittestxu) == true
         (Physics.Raycast(raytestZLower, out hittestzl) == true && hittest.collider.name != "submarine" &&
        (hittest.collider.name == "extra_island" || hittest.collider.name == "extra_island1"
        || hittest.collider.name == "extra_island2" || hittest.collider.name == "extra_island3"
        || hittest.collider.name == "extra_island4" || hittest.collider.name == "extra_island4"
        || hittest.collider.name == "extra_island5" || hittest.collider.name == "extra_island6"
        || hittest.collider.name == "extra_island7")) || 
        (Physics.Raycast(raytestZUpper, out hittestzu) == true && hittest.collider.name != "submarine" &&
        (hittest.collider.name == "extra_island" || hittest.collider.name == "extra_island1"
        || hittest.collider.name == "extra_island2" || hittest.collider.name == "extra_island3"
        || hittest.collider.name == "extra_island4" || hittest.collider.name == "extra_island4"
        || hittest.collider.name == "extra_island5" || hittest.collider.name == "extra_island6"
        || hittest.collider.name == "extra_island7"))  */

        //SELF-GUIDE    
        if (f==1 || f1==1 || f2==1 || f3==1 || f4==1)
        {
            if (elseFlag == 1)
            {
                elseFlag = 0;
                enemy1.velocity = Vector3.zero;
            }

            //Debug.Log(hittest.collider.name +" -> in the loop");

            if (findWayFlag == 0)
            {
                noWayTime = Time.time + 100;
                findWayFlag = 1;
            }
            if (Time.time >= noWayTime && findWayFlag==1)
            {
                radius += 30;
                findWayFlag = 0;
            }
            /*if (switchLoop == 0)
            {
                enemy1.velocity = Vector3.zero;
                switchLoop = 1;
            }*/

            //Debug.Log("hitting");
            //wrn1.text = "hi !!";
            y = 0;

            if (flagOther == 0)
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

                if (/*tried == 0 && */ Physics.Raycast(ray, out hit, radius) == false && Physics.Raycast(rayl, out hitl, radius) == false
                    && Physics.Raycast(rayr, out hitr, radius) == false)
                {
                    flagOther = 1;
                    nextTimeOther = Time.time + delay;

                    //theta += 30;
                    //if (Array.Exists(xvis, ele => ele == x) == false && )
                    //{
                        xvis[ctr] = x;
                        zvis[ctr] = z;
                        ctr++;

                        enemy1.AddForce(pos * speed, ForceMode.Force);
                    ///}
                    /*else
                    {
                        noWayTime = Time.time + 10;
                    }*/
                }
                else //if (tried == 0)
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

            if (flagOther == 1)
            {
                if (Time.time >= nextTimeOther)
                {
                    //player.AddForce(pos * speed, ForceMode.Force);
                    flagOther = 0;
                    enemy1.velocity = Vector3.zero;
                }
            }
        }

        //OTHER A-I
        else
        {
            if (elseFlag == 0)
            {
                elseFlag = 1;
                enemy1.velocity = Vector3.zero;
            }

            radius = 20;
            switchLoop=0;

            enemy1.AddForce((GameObject.FindGameObjectWithTag("sub_player").transform.position -
                GameObject.FindGameObjectWithTag("sub_enemy_1").transform.position) * enemySpeed, ForceMode.Force);

            float dist = Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                GameObject.FindGameObjectWithTag("sub_enemy_1").transform.position);

            if (dist < tolerantDist)
            {
                //missileAttackClip.Play();

                if (dist < intolerabledist)
                {
                    //in enemy's line of fire
                    if (flag == 0)
                    {
                        myPos = GameObject.FindGameObjectWithTag("sub_enemy_1").transform.position;
                        initDist = dist;

                        nextTime = Time.time + diff;
                        initPos = GameObject.FindGameObjectWithTag("sub_player").transform.position;
                        flag = 1;
                    }

                    if (Time.time >= nextTime && flag == 1)
                    {
                        finalPos = GameObject.FindGameObjectWithTag("sub_player").transform.position;
                        finalDist = Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                                GameObject.FindGameObjectWithTag("sub_enemy_1").transform.position);
                        if ((((initPos - myPos).normalized.x - (finalPos - myPos).normalized.x < 0.2) &&
                             ((initPos - myPos).normalized.y - (finalPos - myPos).normalized.y < 0.2) &&
                            ((initPos - myPos).normalized.z - (finalPos - myPos).normalized.z < 0.2)) ||
                            (((myPos - initPos).normalized.x - (finalPos - myPos).normalized.x < 0.2) &&
                            ((myPos - initPos).normalized.y - (finalPos - myPos).normalized.y < 0.2) &&
                            ((myPos - initPos).normalized.z - (finalPos - myPos).normalized.z < 0.2)))
                        {
                            PlayerControl.health -= 25;
                            TorpedoImpactClip.Play();
                            if (PlayerControl.health <= 0)
                            {
                                ;// wrn1.text = "Busted !";
                            }
                        }

                        flag = 0;
                        nextTime = 0;   // Time.time;
                    }
                }


                //movement along same direction for a longtime ; predictable behavior
                if (flagSmarti == 0)
                {
                    initPosSmart = GameObject.FindGameObjectWithTag("sub_player").transform.position;
                    flagSmartm = 1;
                    flagSmarti = 1;
                    nextTimeSmarti = Time.time + smartDiffOne;
                }

                if (flagSmartm == 1 && Time.time >= nextTimeSmarti)
                {
                    midPosSmart = GameObject.FindGameObjectWithTag("sub_player").transform.position;
                    flagSmartf = 1;
                    flagSmartm = 0;
                    nextTimeSmartm = Time.time + smartDiffTwo;
                }

                if (flagSmartf == 1 && Time.time >= nextTimeSmartm)
                {
                    finalPosSmart = GameObject.FindGameObjectWithTag("sub_player").transform.position;
                    flagSmart = 1;
                    flagSmartf = 0;
                    nextTimeSmart = Time.time + timeToHit;
                }

                if (flagSmart == 1 && Time.time >= nextTimeSmart)
                {
                    if ((((midPosSmart - initPosSmart).normalized.x - (finalPosSmart - midPosSmart).normalized.x < 0.2) &&
                        ((midPosSmart - initPosSmart).normalized.y - (finalPosSmart - midPosSmart).normalized.y < 0.2) &&
                        ((midPosSmart - initPosSmart).normalized.z - (finalPosSmart - midPosSmart).normalized.z < 0.2)) ||
                         (((initPosSmart - midPosSmart).normalized.normalized.x - (finalPosSmart - midPosSmart).normalized.x < 0.2) &&
                        ((initPosSmart - midPosSmart).normalized.normalized.y - (finalPosSmart - midPosSmart).normalized.y < 0.2) &&
                        ((initPosSmart - midPosSmart).normalized.z - (finalPosSmart - midPosSmart).normalized.z < 0.2)))
                    {
                        PosSmart = GameObject.FindGameObjectWithTag("sub_player").transform.position;

                        if ((((finalPosSmart - midPosSmart).normalized.x - (PosSmart - finalPosSmart).normalized.x < 0.2) &&
                            ((finalPosSmart - midPosSmart).normalized.y - (PosSmart - finalPosSmart).normalized.y < 0.2) &&
                            ((finalPosSmart - midPosSmart).normalized.z - (PosSmart - finalPosSmart).normalized.z < 0.2)) ||
                            (((midPosSmart - finalPosSmart).normalized.x - (PosSmart - finalPosSmart).normalized.x < 0.2) &&
                            ((midPosSmart - finalPosSmart).normalized.y - (PosSmart - finalPosSmart).normalized.y < 0.2) &&
                            ((midPosSmart - finalPosSmart).normalized.z - (PosSmart - finalPosSmart).normalized.z < 0.2)))
                        {
                            PlayerControl.health -= 20;
                            wrn1.text = "Shot by Enemy 1";
                            dispflag = 1;
                            TorpedoImpactClip.Play();
                            flag = 0;
                            flagSmart = 0;
                            flagSmarti = 0;
                            flagSmartf = 0;
                            flagSmartm = 0;
                        }
                        else
                        {
                            wrn1.text = "Just missed !";
                            dispflag = 1;
                            dispNextTime = Time.time + 2;
                        }
                    }
                }
            }

            /*if(flagSmartf==1 && Time.time >= nextTimeSmartf)
            {
                //wrn1.text = "Busted !";
                PlayerControl.health -= 25;
                //nextTimeSmartf = 0;
                //nextTimeSmarti = 0;
                flagSmarti = 0;
                flagSmartf = 0;
                flagSmartm = 0;
                flagSmart = 0;
            }*/

            // *****************else not commented************************************************ 


            else
            {
                flag = 0;
                flagSmarti = 0;
                flagSmartf = 0;
                flagSmartm = 0;

            }



            /*if (dist < tolerantDist)
            {
                wrn1.text = "Busted !";
                Application.Quit();
            }*/

        }

        if (dispflag == 1 && Time.time >= dispNextTime)
        {
            dispflag = 0;
            wrn1.text = "";
        }

        var = Time.time;
    }

    /*private void OnDestroy()
    {
        if (health1 <= 0)
        {
            PlayerControl.sw.WriteLine("enemy 1 destroyed");
            PlayerControl.sw.WriteLine("at position" + transform.position);
            PlayerControl.sw.WriteLine("at time" + Time.time);
        }
        else
        {
            PlayerControl.sw.WriteLine("enemy 1 survived");
        }
    }*/

    private void OnDestroy()
    {
        DestroyedAtPos1 = transform.position;
        DestroyedAtTime1 = var;
    }

}
