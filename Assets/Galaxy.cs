using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy
{
    private List<Body> bodies;
    private float timeStep;
    private float GRAVITATIONAL_CONSTANT; //6.67408 * 10^-11 would be value in real life
    private float simulationPrecision = 0.02f; //Amount of time between fixed updates

    public Galaxy(float tStep, float G)
    {
        Time.fixedDeltaTime = simulationPrecision;
        timeStep = tStep / 100;
        GRAVITATIONAL_CONSTANT = G;
        bodies = new List<Body>();
    }

    //START ADD BODY FUNCTIONS
    //NOTE: Cannot be used to add stars (body will not emit light); use addStar()
    public void addBody(double mass, float radius, float x, float y, float z)
    {
        bodies.Add(new Body(mass, radius, x, y, z, "" + bodies.Count));
    }

    //NOTE: Cannot be used to add stars (body will not emit light); use addStar()
    public void addBody(double mass, float radius, float x, float y, float z, float vX, float vY, float vZ)
    {
        bodies.Add(new Body(mass, radius, x, y, z, vX * timeStep, vY * timeStep, vZ * timeStep, "" + bodies.Count));
    }

    //NOTE: Cannot be used to add stars (body will not emit light); use addStar()
    public void addBody(double mass, float radius, float x, float y, float z, string id)
    {
        bodies.Add(new Body(mass, radius, x, y, z, id));
    }

    //NOTE: Cannot be used to add stars (body will not emit light); use addStar()
    public void addBody(double mass, float radius, float x, float y, float z, float vX, float vY, float vZ, string id)
    {
        bodies.Add(new Body(mass, radius, x, y, z, vX * timeStep, vY * timeStep, vZ * timeStep, id));
    }

    public void addStar(double mass, float radius, float x, float y, float z, float lightVal, float lightRange)
    {
        bodies.Add(new Star(mass, radius, x, y, z, lightVal, lightRange, "" + bodies.Count));
    }

    public void addStar(double mass, float radius, float x, float y, float z, float vX, float vY, float vZ, float lightVal, float lightRange)
    {
        bodies.Add(new Star(mass, radius, x, y, z, vX * timeStep, vY * timeStep, vZ * timeStep, lightVal, lightRange, "" + bodies.Count));
    }

    public void addStar(double mass, float radius, float x, float y, float z, float lightVal, float lightRange, string id)
    {
        bodies.Add(new Star(mass, radius, x, y, z, lightVal, lightRange, id));
    }

    public void addStar(double mass, float radius, float x, float y, float z, float vX, float vY, float vZ, float lightVal, float lightRange, string id)
    {
        bodies.Add(new Star(mass, radius, x, y, z, vX * timeStep, vY * timeStep, vZ * timeStep, lightVal, lightRange, id));
    }
    //END ADD BODY FUNCTIONS

    public float getSimulationSpeed()
    {
        return timeStep;
    }

    public void setSimulationSpeed(float newSpeed)
    {
        float oldStep = timeStep;
        timeStep = newSpeed / 100;

        foreach (Body b in bodies)
        {
            b.setVX(Convert.ToSingle(b.getVX() / oldStep * timeStep));
            b.setVY(Convert.ToSingle(b.getVY() / oldStep * timeStep));
            b.setVZ(Convert.ToSingle(b.getVZ() / oldStep * timeStep));
            b.getAppearance().GetComponent<TrailRenderer>().Clear();
        }
    }

    public float getSimulationPrecision()
    {
        return simulationPrecision;
    }

    public void setSimulationPrecision(float newPrecision)
    {
        Time.fixedDeltaTime = newPrecision;

        foreach (Body b in bodies)
        {
            b.getAppearance().GetComponent<TrailRenderer>().Clear();
        }
    }

    public void simulateTStep()
    {
        float a = 0;
        float aX = 0;
        float aY = 0;
        float aZ = 0;
        float r = 0;
        float[] angles = new float[3];

        foreach (Body b in bodies)
        {
            if(b.getID().Equals("DEFAULT (ORIGIN)"))
            {
                continue;
            }

            b.setXPos(b.getAppearance().transform.position.x * 100);
            b.setYPos(b.getAppearance().transform.position.y * 100);
            b.setZPos(b.getAppearance().transform.position.z * 100);


            aX = 0;
            aY = 0;
            aZ = 0;
            foreach (Body i in bodies)
            {
                float xDist = b.getDistance(i).x;
                float yDist = b.getDistance(i).y;
                float zDist = b.getDistance(i).z;

                if (Math.Abs(xDist) > Math.Pow(10, -3) || Math.Abs(yDist) > Math.Pow(10, -3) || Math.Abs(zDist) > Math.Pow(10, -3))
                { //Make sure body isnt iterating over itself/other body at same location
                    r = Convert.ToSingle(Math.Sqrt(Math.Pow(xDist, 2) + Math.Pow(yDist, 2) + Math.Pow(zDist, 2)));
                    angles = Vector3Angles(xDist, yDist, zDist);
                } else
                {
                    angles = new float[3] { 0, 0, 0 };
                    continue;
                }

                a = Convert.ToSingle((GRAVITATIONAL_CONSTANT * ((i.getMass()) / Math.Pow(r, 2))));
                //Account for the timestep
                //Debug.Log(a);
                a *= timeStep;
                //Debug.Log(a);
                //Debug.Log("");

                aX += Convert.ToSingle(a * Math.Cos(angles[0]) * b.getAXDirection(i));
                aY += Convert.ToSingle(a * Math.Cos(angles[1]) * b.getAYDirection(i));
                aZ += Convert.ToSingle(a * Math.Cos(angles[2]) * b.getAZDirection(i));
                //Debug.Log(aX);
                //Debug.Log(aY);
                //Debug.Log(aZ);
                //Debug.Log("");
            }

            //Update velocities each time object is calculated
            b.setAX(aX);
            b.setAY(aY);
            b.setAZ(aZ);
            //Debug.Log(b.getID() + " ay + vy: " + (b.getAY() + b.getVY()));
            b.setVX(Convert.ToSingle((b.getVX()) + aX * timeStep * 100 * Time.fixedDeltaTime));
            b.setVY(Convert.ToSingle((b.getVY()) + aY * timeStep * 100 * Time.fixedDeltaTime));
            b.setVZ(Convert.ToSingle((b.getVZ()) + aZ * timeStep * 100 * Time.fixedDeltaTime));

            //b.setVX(Convert.ToSingle((b.getVX()) + aX * timeStep / 1f));
            //b.setVY(Convert.ToSingle((b.getVY()) + aY * timeStep / 1f));
            //b.setVZ(Convert.ToSingle((b.getVZ()) + aZ * timeStep / 1f));

            if (b.getID() == "Earth")
            {
                //Debug.Log(b.getVX());
                //Debug.Log(b.getID() + " ry: " + r);
                //Debug.Log(b.getID() + " ay: " + b.getAY());
                //Debug.Log(b.getID() + " vy: " + b.getVY());
                //Debug.Log(b.getVZ());
                //Debug.Log("");
            }


            //Don't need to update location; unity already does based on velocity
        }

    }

    public float[] Vector3Angles(float xComp, float yComp, float zComp)
    {
        //Returns angles of 3D vector in order: thetaX, thetaY, thetaZ
        float magnitude = Convert.ToSingle(Math.Sqrt(Math.Pow(xComp, 2) + Math.Pow(yComp, 2) + Math.Pow(zComp, 2)));
        float[] angles = new float[3];
        angles[0] = Convert.ToSingle(Math.Acos(xComp / magnitude));
        angles[1] = Convert.ToSingle(Math.Acos(yComp / magnitude));
        angles[2] = Convert.ToSingle(Math.Acos(zComp / magnitude));

        return angles;
    }

    public Body getBody(String id)
    {
        foreach (Body b in bodies)
        {
            if(b.getID() == id)
            {
                return b;
            }
        }
        return null;
    }

    public float getGravitationalConstant()
    {
        return GRAVITATIONAL_CONSTANT;
    }

    public List<Body> getBodies()
    {
        return bodies;
    }

}
