using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AggressiveEnemy2HardVR : MonoBehaviour
{

    public Rigidbody sub, enemy;

    public float tolerantDist = 50, intolerabledist = 20;
    public Text wrn;
    public int enemySpeed = 10, diffDanger = 2, smartDiff = 5, smartDiffOne = 2, smartDiffTwo = 3, timeToHit = 1;
    private float nextTime, initDist, finalDist;
    private float nextTimeSmarti, nextTimeSmartf, nextTimeSmartm, nextTimeSmart;
    private Vector3 initPos, finalPos, myPos, toleranceVector;
    private Vector3 initPosSmart, finalPosSmart, midPosSmart, PosSmart;
    private int flag = 0, flagSmarti = 0, flagSmartf = 0, flagSmartm = 0, flagSmart = 0, switchLoop = 0;

    public int r = 100, speed = 2000, incrementTheta = 15;
    public float toleranceWidth = 30f, delay = 0.5f;
    public float radius = 20;
    private Vector3 pos, posl, posr;
    private int flagOther = 0, tried = 0, findWay = 0, ctr = 0;
    private float nextTimeOther, theta = 0, thetal, thetar, x, z, xl, xr, zl, zr, y, noWayTime, findWayFlag = 0;

    private float nextTimeDownwardForce, DownwardForceFlag = 0, ExtraFlag = 0;
    private float elseFlag = 0;

    public float upperTolerance = 20, timeTakenToShoot = 2;
    public int higherDamageShot = 25, lowerDamageShot = 20;
    private int f = 0, f1 = 0, f2 = 0, f3 = 0, f4 = 0;

    private float[] xvis, zvis;
    //public Text status;

    private float maxRayDistanceNextTime, maxRayDistanceFlag = 0;
    public float maxRayDistance = 200;
    public AudioSource TorpedoImpactClip, missileAttackClip;

    private int dispflag = 0;
    private float dispNextTime;

    public static Vector3 DestroyedAtPos;
    public static float DestroyedAtTime;
    private float var;

    private Ray shootRay1, shootRay2;
    private RaycastHit shootHit1, shootHit2;

    private float extraflagdisp = 0, extranexttime, dist;
    private float eraseDispFlag = 0, eraseDispNextTime;

    private int flag1 = 0, flag2 = 0;

    public GameObject beam;
    private LineRenderer line;

    private float rayVisibleFlag = 0, rayVisibleNextTime;
    private float staticFlag = 0, staticNextTime;
    public float staticTimeUnit = 3;
    private Vector3 staticInitPos, staticFinalPos;

    public float downwardSpeed = 5000, downwardTime = 2;

    // Use this for initialization
    void Start()
    {
        line = beam.AddComponent<LineRenderer>();

        toleranceVector = new Vector3(0.1f, 0.1f, 0.1f);
        wrn.text = "";
        xvis = new float[10000];
        zvis = new float[10000];
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Ray raytest = new Ray(transform.position, sub.transform.position - transform.position);
        RaycastHit hittest;
        Debug.DrawRay(transform.position, (sub.transform.position - transform.position).normalized * maxRayDistance, Color.red);


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


        Ray raytestGoDown = new Ray(transform.position, new Vector3(0, -1, 0));
        RaycastHit hittestgd;
        Debug.DrawRay(transform.position, new Vector3(0, -1, 0), Color.red);

        Ray raytestGoUp = new Ray(transform.position, new Vector3(0, 1, 0));
        RaycastHit hittestgu;
        Debug.DrawRay(transform.position, new Vector3(0, 1, 0), Color.red);

        if ((Mathf.Abs(sub.transform.position.y - enemy.transform.position.y) <= 2) && sub.transform.position.y >= -4
            && DownwardForceFlag == 0 && Physics.Raycast(raytestGoDown, out hittestgd, 2) == false)
        {
            enemy.AddForce(new Vector3(0, -downwardSpeed, 0), ForceMode.Force);
            nextTimeDownwardForce = Time.time + downwardTime;
            DownwardForceFlag = 1;
        }
        if (Mathf.Abs(sub.transform.position.y - enemy.transform.position.y) <= 2 && sub.transform.position.y <= -4
            && DownwardForceFlag == 0 && Physics.Raycast(raytestGoUp, out hittestgu, 2) == false)
        {
            enemy.AddForce(new Vector3(0, downwardSpeed, 0), ForceMode.Force);
            nextTimeDownwardForce = Time.time + downwardTime;
            DownwardForceFlag = 1;
        }
        if (DownwardForceFlag == 1 && Time.time >= nextTimeDownwardForce)
        {
            DownwardForceFlag = 0;
            enemy.velocity = Vector3.zero;
        }

        if (Physics.Raycast(raytest, out hittest, maxRayDistance) == true)
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
        else if (maxRayDistanceFlag == 0)
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
            if (maxRayDistance > 60)
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

        Ray final = new Ray(enemy.transform.position, sub.transform.position);
        RaycastHit finalHit;

        //SELF-GUIDE    
        if ((f == 1 || f1 == 1 || f2 == 1 || f3 == 1 || f4 == 1)
            && Physics.Raycast(final, out finalHit, Vector3.Distance(sub.transform.position, enemy.transform.position) - 10))
        {
            if (elseFlag == 1)
            {
                elseFlag = 0;
                enemy.velocity = Vector3.zero;
            }

            //Debug.Log(hittest.collider.name +" -> in the loop");

            if (findWayFlag == 0)
            {
                noWayTime = Time.time + 100;
                findWayFlag = 1;
            }
            if (Time.time >= noWayTime && findWayFlag == 1)
            {
                radius += 30;
                findWayFlag = 0;
            }


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

                if (Physics.Raycast(ray, out hit, radius) == false && Physics.Raycast(rayl, out hitl, radius) == false
                    && Physics.Raycast(rayr, out hitr, radius) == false)
                {
                    flagOther = 1;
                    nextTimeOther = Time.time + delay;

                    xvis[ctr] = x;
                    zvis[ctr] = z;
                    ctr++;

                    enemy.AddForce(pos * speed, ForceMode.Force);

                }
                else //if (tried == 0)
                {
                    theta = (theta + incrementTheta) % 360;
                    if (theta == 359)
                    {
                        ;//status.text="??";//tried = 1;
                    }
                }


            }

            if (flagOther == 1)
            {
                if (Time.time >= nextTimeOther)
                {
                    //player.AddForce(pos * speed, ForceMode.Force);
                    flagOther = 0;
                    enemy.velocity = Vector3.zero;
                }
            }
        }

        //OTHER A-I
        else
        {
            if (elseFlag == 0)
            {
                elseFlag = 1;
                enemy.velocity = Vector3.zero;
            }

            radius = 20;
            switchLoop = 0;

            enemy.AddForce((GameObject.FindGameObjectWithTag("sub_player").transform.position -
                enemy.transform.position) * enemySpeed, ForceMode.Force);

            dist = Vector3.Distance(GameObject.FindGameObjectWithTag("sub_player").transform.position,
                enemy.transform.position);

            if (dist < tolerantDist)
            {
                //missileAttackClip.Play();

                if (staticFlag == 0)
                {
                    staticNextTime = Time.time + staticTimeUnit;
                    staticFlag = 1;
                    staticInitPos = sub.transform.position;
                    missileAttackClip.Play();
                }
                if (staticFlag == 1 && Time.time >= staticNextTime)
                {
                    staticFinalPos = sub.transform.position;
                    if (staticFinalPos == staticInitPos)
                    {
                        PlayerControlHardVR.health -= higherDamageShot;
                        wrn.text = "Shot by Enemy !";

                        eraseDispFlag = 1;
                        eraseDispNextTime = Time.time + 2;

                        rayVisibleNextTime = Time.time + 0.5f;
                        rayVisibleFlag = 1;

                        line.SetPosition(0, transform.position);
                        line.SetPosition(1, sub.transform.position);
                        line.SetColors(Color.red, Color.red);
                        line.SetWidth(0.2f, 0.2f);
                        line.enabled = true;

                        TorpedoImpactClip.Play();
                    }
                    staticFlag = 0;
                }

                if (dist < intolerabledist)
                {
                    //in enemy's line of fire
                    if (flag == 0)
                    {
                        myPos = enemy.transform.position;
                        initDist = dist;

                        nextTime = Time.time + diffDanger;
                        initPos = GameObject.FindGameObjectWithTag("sub_player").transform.position;
                        flag = 1;
                    }

                    if (Time.time >= nextTime && flag == 1)
                    {
                        finalPos = GameObject.FindGameObjectWithTag("sub_player").transform.position;

                        /*if ((((initPos - myPos).normalized.x - (finalPos - myPos).normalized.x < 0.1) &&
                             ((initPos - myPos).normalized.y - (finalPos - myPos).normalized.y < 0.1) &&
                            ((initPos - myPos).normalized.z - (finalPos - myPos).normalized.z < 0.1)) ||
                            (((myPos - initPos).normalized.x - (finalPos - myPos).normalized.x < 0.1) &&
                            ((myPos - initPos).normalized.y - (finalPos - myPos).normalized.y < 0.1) &&
                            ((myPos - initPos).normalized.z - (finalPos - myPos).normalized.z < 0.1)))*/

                        if (Mathf.Abs((finalPos - initPos).normalized.x) < 0.1 && Mathf.Abs((finalPos - initPos).normalized.y) < 0.1
                            && Mathf.Abs((finalPos - initPos).normalized.z) < 0.1)
                        {
                            dispflag = 1;
                            dispNextTime = Time.time + timeTakenToShoot;

                            flag1 = 1;
                            //shootRay1 = new Ray(myPos, finalPos - myPos);
                            shootRay1 = new Ray(initPos, finalPos - initPos);
                            missileAttackClip.Play();
                        }

                        flag = 0;
                        //nextTime = 0;   // Time.time;
                    }
                }

                else
                {
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
                        if ((Mathf.Abs((midPosSmart - initPosSmart).normalized.x - (finalPosSmart - midPosSmart).normalized.x) < 0.1) &&
                            (Mathf.Abs((midPosSmart - initPosSmart).normalized.y - (finalPosSmart - midPosSmart).normalized.y) < 0.1) &&
                            (Mathf.Abs((midPosSmart - initPosSmart).normalized.z - (finalPosSmart - midPosSmart).normalized.z) < 0.1))
                        {
                            PosSmart = GameObject.FindGameObjectWithTag("sub_player").transform.position;

                            if ((Mathf.Abs((finalPosSmart - midPosSmart).normalized.x - (PosSmart - finalPosSmart).normalized.x) < 0.1) &&
                                (Mathf.Abs((finalPosSmart - midPosSmart).normalized.y - (PosSmart - finalPosSmart).normalized.y) < 0.1) &&
                                (Mathf.Abs((finalPosSmart - midPosSmart).normalized.z - (PosSmart - finalPosSmart).normalized.z) < 0.1))
                            {
                                dispflag = 1;
                                dispNextTime = Time.time + timeTakenToShoot;

                                flag2 = 1;

                                //shootRay2 = new Ray(enemy.transform.position, finalPosSmart - enemy.transform.position);
                                shootRay2 = new Ray(initPosSmart, finalPosSmart - initPosSmart);
                                missileAttackClip.Play();

                                flag = 0;
                                flagSmart = 0;
                                flagSmarti = 0;
                                flagSmartf = 0;
                                flagSmartm = 0;
                            }
                            else
                            {
                                wrn.text = "Just missed !";
                                eraseDispFlag = 1;
                                eraseDispNextTime = Time.time + 2;

                                //dispflag = 1;
                                //dispNextTime = Time.time + timeTakenToShoot;
                                flagSmart = 0;
                            }
                        }
                    }
                }
            }

        }

        if (dispflag == 1 && Time.time >= dispNextTime)
        {
            missileAttackClip.Stop();

            if (Physics.Raycast(shootRay1, out shootHit1, tolerantDist) && flag1 == 1)
            {
                flag1 = 0;

                if (shootHit1.collider.tag == "sub_player")
                {
                    PlayerControlHardVR.health -= higherDamageShot;
                    wrn.text = "Shot by Enemy !";

                    eraseDispFlag = 1;
                    eraseDispNextTime = Time.time + 2;

                    rayVisibleNextTime = Time.time + 0.5f;
                    rayVisibleFlag = 1;

                    line.SetPosition(0, transform.position);
                    line.SetPosition(1, sub.transform.position);
                    line.SetColors(Color.red, Color.red);
                    line.SetWidth(0.2f, 0.2f);
                    line.enabled = true;

                    TorpedoImpactClip.Play();
                }
            }
            if (Physics.Raycast(shootRay2, out shootHit2, tolerantDist) && flag2 == 1)
            {
                flag2 = 0;

                if (shootHit2.collider.tag == "sub_player")
                {
                    PlayerControlHardVR.health -= lowerDamageShot;
                    wrn.text = "Shot by Enemy !";

                    eraseDispFlag = 1;
                    eraseDispNextTime = Time.time + 2;

                    rayVisibleNextTime = Time.time + 0.5f;
                    rayVisibleFlag = 1;

                    line.SetPosition(0, transform.position);
                    line.SetPosition(1, sub.transform.position);
                    line.SetColors(Color.red, Color.red);
                    line.SetWidth(0.2f, 0.2f);
                    line.enabled = true;

                    TorpedoImpactClip.Play();
                }
            }

            extranexttime = Time.time + 2;
            extraflagdisp = 1;
            dispflag = 0;
        }

        if (extraflagdisp == 1 && Time.time >= extranexttime)
        {
            extraflagdisp = 0;
        }

        if (eraseDispFlag == 1 && Time.time >= eraseDispNextTime)
        {
            eraseDispFlag = 0;
            wrn.text = "";
        }

        if (rayVisibleFlag == 1 && Time.time >= rayVisibleNextTime)
        {
            rayVisibleFlag = 0;
            line.enabled = false;
        }

        var = Time.time;
    }


    private void OnDestroy()
    {
        DestroyedAtPos = transform.position;
        DestroyedAtTime = var;
    }

}
