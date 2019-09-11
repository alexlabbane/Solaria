using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body
{
    private const double pi = 3.1415926535897932384626433832795;

    public GameObject appearance;
    private Rigidbody rigidBody;
    private TrailRenderer trail;

    private double mass;
    private double volume;
    private double density;
    private float radius;
    private float accelerationX;
    private float accelerationY;
    private float accelerationZ;
    public float VXi;
    public float VYi;
    public float VZi;
    private float xPos;
    private float yPos;
    private float zPos;
    private string ID;
    private int isStar;


    public Body (double m, float r, float x, float y, float z, string id, Color color = default(Color))
    {
        mass = m;
        radius = 10 * r;
        volume = (4 / 3) * pi * (r * r * r);
        density = mass / volume;
        xPos = x;
        yPos = y;
        zPos = z;
        accelerationX = 0;
        accelerationY = 0;
        accelerationZ = 0;
        ID = id;
        
        appearance = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        appearance.transform.position = new Vector3(xPos / 100, yPos / 100, zPos / 100);
        appearance.transform.localScale = new Vector3((10 * 2 * r) / 100, (10 * 2 * r) / 100, (10 * 2 * r) / 100);
        rigidBody = appearance.AddComponent<Rigidbody>();
        rigidBody.detectCollisions = false;

        trail = appearance.AddComponent<TrailRenderer>();
        trail.time = 5;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        trail.receiveShadows = false;
        trail.widthMultiplier = 5;

        UnityEngine.Material mat = new UnityEngine.Material(Shader.Find("Standard"));
        if (color == default(Color))
        {
            color = Color.gray;
            isStar = 0;
        }else
        {
            isStar = 1;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", new Color(255, 187, 0));
            //mat.EnableKeyword("_EMISSION");
            //mat.SetColor("_EmissionColor", new Color(255, 187, 0));
        }
        mat.color = color;
        appearance.GetComponent<Renderer>().material = mat;

        //Apply texture
        /*UnityEngine.Material mater = Earth;
        mater.EnableKeyword("_EMISSION");
        mater.SetColor("_EmissionColor", new Color(255, 187, 0));
        appearance.GetComponent<Renderer>().material = mater;*/
    }

    public Body(double m, float r, float x, float y, float z, float vX, float vY, float vZ, string id, Color color = default(Color))
    {
        VXi = vX;
        VYi = vY;
        VZi = vZ;

        mass = m;
        radius = 10 * r;
        volume = (4 / 3) * pi * (r * r * r);
        density = mass / volume;
        xPos = x;
        yPos = y;
        zPos = z;
        accelerationX = 0;
        accelerationY = 0;
        accelerationZ = 0;
        ID = id;

        appearance = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        appearance.transform.position = new Vector3(xPos / 100, yPos / 100, zPos / 100);
        appearance.transform.localScale = new Vector3((10 * 2 * r) / 100, (10 * 2 * r) / 100, (10 * 2 * r) / 100);
        rigidBody = appearance.AddComponent<Rigidbody>();
        rigidBody.velocity = new Vector3(vX, vY, vZ);
        rigidBody.detectCollisions = false;

        trail = appearance.AddComponent<TrailRenderer>();
        trail.time = 500;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        trail.receiveShadows = false;
        trail.widthMultiplier = 5;

        UnityEngine.Material mat = new UnityEngine.Material(Shader.Find("Standard"));
        if (color == default(Color))
        {
            color = Color.gray;
            isStar = 0;
        } else
        {
            //UnityEngine.Material sunMat = new UnityEngine.Material(Shader.Find("Legacy Shaders/Self-Illumin/Specular"));
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", new Color(255, 187, 0));
            isStar = 1;
            //sunMat.color = color;
            //appearance.GetComponent<Renderer>().material = sunMat;
        }
        mat.color = color;
        appearance.GetComponent<Renderer>().material = mat;

        Debug.Log("Initial vx,vy,vz");
        Debug.Log(vX);
        Debug.Log(vY);
        Debug.Log(vZ);

        //Apply texture
        /*UnityEngine.Material mater = Earth;
        mater.EnableKeyword("_EMISSION");
        mater.SetColor("_EmissionColor", new Color(255, 187, 0));
        appearance.GetComponent<Renderer>().material = mater;*/
    }

    public void accelerateBody(float aX, float aY, float aZ)
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x + aX, rigidBody.velocity.y + aY, rigidBody.velocity.z + aZ);
    }

    public double getMass()
    {
        return mass;
    }

    public double getRadius()
    {
        return radius;
    }

    public float[] getPos()
    {
        float[] pos = new float[3] { xPos, yPos, zPos };
        return pos;
    }

    public void setXPos(float newX)
    {
        xPos = newX;
    }

    public void setYPos(float newY)
    {
        yPos = newY;
    }
    
    public void setZPos(float newZ)
    {
        zPos = newZ;
    }

    public void updatePos()
    {
        appearance.transform.position = new Vector3(xPos, yPos, zPos);
    }

    public Vector3 getDistance(Body b)
    {
        float xDistance = Math.Abs(xPos - b.getPos()[0]);
        float yDistance = Math.Abs(yPos - b.getPos()[1]);
        float zDistance = Math.Abs(zPos - b.getPos()[2]);

        return new Vector3(xDistance, yDistance, zDistance);
    }

    public int getAXDirection(Body b)
    {
        double dist = xPos - b.getAppearance().transform.position.x;
        if (dist <= 0)
        {
            return 1;
        }
        return -1;
    }

    public int getAYDirection(Body b)
    {
        double dist = yPos - b.getAppearance().transform.position.y;
        if (dist <= 0)
        {
            return 1;
        }
        return -1;
    }

    public int getAZDirection(Body b)
    {
        double dist = zPos - b.getAppearance().transform.position.z;
        if (dist <= 0)
        {
            return 1;
        }
        return -1;
    }

    public string getID()
    {
        return ID;
    }

    public double getVX()
    {
        return rigidBody.velocity.x;
    }

    public double getVY()
    {
        //Debug.Log("Current vy:" + rigidBody.velocity.y);
        return rigidBody.velocity.y;
    }

    public double getVZ()
    {
        return rigidBody.velocity.z;
    }

    public void setVX(float vX)
    {
        rigidBody.velocity = new Vector3(vX, rigidBody.velocity.y, rigidBody.velocity.z);
    }

    public void setVY(float vY)
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, vY, rigidBody.velocity.z);
    }

    public void setVZ(float vZ)
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, vZ);
    }

    public double getAX()
    {
        return accelerationX;
    }

    public double getAY()
    {
        return accelerationY;
    }

    public double getAZ()
    {
        return accelerationZ;
    }

    public void setAX(float aX)
    {
        accelerationX = aX;
    }

    public void setAY(float aY)
    {
        accelerationY = aY;
    }

    public void setAZ(float aZ)
    {
        accelerationZ = aZ;
    }

    public GameObject getAppearance()
    {
        return appearance;
    }

    public int isAStar()
    {
        return isStar;
    }

    public void showPath(bool show)
    {
        if (show)
        {
            appearance.GetComponent<TrailRenderer>().widthMultiplier = 5;
        } else
        {
            appearance.GetComponent<TrailRenderer>().widthMultiplier = 0;
        }
    }

}
