using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class main : MonoBehaviour
{
    public UnityEngine.Material Earth;
    public Galaxy testGalaxy;
    // Start is called before the first frame update
    void Start()
    {
        testGalaxy = new Galaxy(100, 1);
            /*GameObject sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Light sunLight = sun.AddComponent<Light>();
            sun.transform.position = new Vector3(0, 1, -12);
            sunLight.intensity = 1;
            sunLight.range = 100;*/

            //Star starTest = new Star(10, 10, 0, 1, -12, 0, 0, 0, 1, 100, "Test");

            /* List<GameObject> bodies = new List<GameObject>();
             List<Rigidbody> rBodies = new List<Rigidbody>();


             for (int i = 0; i < 10; i ++)
             {
                 bodies.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
                 rBodies.Add(bodies[i].AddComponent<Rigidbody>());
                 bodies[i].transform.position = new Vector3(i * 1, i * 1, i * 1);
                 rBodies[i].velocity = new Vector3(0.5f * i, 0, 0);
             }*/

            //Body test = new Body(10, 10, 0, 0, 0, 0, 0.1f, 0, "Test");
            //testGalaxy.addStar(5000000, 1000, 0, 0, 0, 0, 0, 0, 1, 5000000, "Sun");
            //testGalaxy.addStar(100000000, 1000, 100000, 0, 0, 0, 0, 0, 1, 5000000, "Sun");
            //testGalaxy.addStar(5000000, 1000, 5000, 0, 0, 0, 3, 0, 1, 5000000, "Sun");
            //testGalaxy.addBody(1, 500, 10000, 0, 0, 0, -3, 0, "Earth");

            //testGalaxy.addBody(1, 500, 5000, 0, 0, 0, 0, -7, "Eearth");
            //testGalaxy.addBody(1, 10, 0, 209000, 0, 10, 15, -21.87f, "Earth");
            //placeInOrbit(testGalaxy, 1, 100000, 50000, 0, 0, 0, testGalaxy.getBody("Sun"));

            System.Random rnd = new System.Random();
            for (int i = 0; i < 0; i++)
            {
                int apoap = rnd.Next(50000, 1000000);
                int periap = apoap - rnd.Next(0, apoap - 20000);

            }
        //ConvertToCartesian(0, 100000, 30, 0, 60, 0, 0, testGalaxy);
        //ConvertToCartesian(0, 100000, 30, 0, 45, 0, 0, testGalaxy);
        //ConvertToCartesian(0, 100000, 45, 45, 45, 0, 0, testGalaxy);
        //ConvertToCartesian(0, 100000, 0, 0, 0, 0, 0, testGalaxy);
        //ConvertToCartesian(0, 99000, Convert.ToSingle(Math.PI / 4.0), 0, 0, 0, 0, testGalaxy);
        //ConvertToCartesian(0, 98000, Convert.ToSingle(Math.PI / 2.0), 0, 0, 0, 0, testGalaxy);

        //createOrbit(147098074000, 147098074000, 0, 0, 0, 0, 0, testGalaxy, testGalaxy.getBody("Sun"), Mathf.Pow(59.72f, 14), 500000000, false, "Earth");

        //createOrbit(500000, 100000, 0, 0, 0, 0, 0, testGalaxy, testGalaxy.getBody("Sun"));

        //TEST INCLINATION
        //createOrbit(100000, 90000, 0, 0, 0, 0, 0, testGalaxy, testGalaxy.getBody("Sun"));
        //createOrbit(100000, 50000, Mathf.PI/2, 0, 0, 0, 0, testGalaxy, testGalaxy.getBody("Sun"));
        //createOrbit(100000, 50000, 3*Mathf.PI/4, 0, 0, 0, 0, testGalaxy, testGalaxy.getBody("Sun"));
        //createOrbit(100000, 50000, Mathf.PI, 0, 0, 0, 0, testGalaxy, testGalaxy.getBody("Sun"));

        //TEST AOP
        //createOrbit(100000, 50000, 0, Mathf.PI/4, 0, 0, 0, testGalaxy, testGalaxy.getBody("Sun"));
        //createOrbit(100000, 50000, 0, Mathf.PI /2, 0, 0, 0, testGalaxy, testGalaxy.getBody("Sun"));

        //TEST LAN
        //createOrbit(100000, 50000, Mathf.PI/4, 0, Mathf.PI / 4, 0, 0, testGalaxy, testGalaxy.getBody("Sun"));
        //createOrbit(100000, 50000, Mathf.PI/4, 0, Mathf.PI / 2, 0, 0, testGalaxy, testGalaxy.getBody("Sun"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        testGalaxy.simulateTStep();

        //Vector3 distVector = testGalaxy.getBody("Sun").getDistance(testGalaxy.getBody("Earth"));
        //Debug.Log(distVector.magnitude);
        //Debug.Log(testGalaxy.getBody("Earth").getAX() + "\t" + testGalaxy.getBody("Earth").getAY() + "\t" + testGalaxy.getBody("Earth").getAZ());
    }

    public void createOrbit(double ap, double pe, double i, double omegaAP, double omegaLAN
        , double T, double EA, Galaxy gal, Body centralBody, float mass, float radius, bool isStar, String name)
    {
        omegaLAN = degToRadians(omegaLAN);
        omegaAP = degToRadians(omegaAP);
        i = degToRadians(i);
        

        double G = gal.getGravitationalConstant();
        double M = centralBody.getMass();
        double mu = G * M;
        double a = (ap + pe) / 2;
        double e = (ap - pe) / (ap + pe);

        double n = Math.Sqrt(mu / (a * a * a));
        double nu = 2 * Math.Atan(Math.Sqrt((1 + e) / (1 - e)) * Math.Tan(EA / 2));

        double r = a * (1 - e * Math.Cos(EA));

        double h = Math.Sqrt(mu * a * (1 - e * e));

        double Om = omegaLAN;
        double w = omegaAP;

        double X = r * (Math.Cos(Om) * Math.Cos(w + nu) - Math.Sin(Om) * Math.Sin(w + nu) * Math.Cos(i));
        double Y = r * (Math.Sin(Om) * Math.Cos(w + nu) + Math.Cos(Om) * Math.Sin(w + nu) * Math.Cos(i));
        double Z = r * (Math.Sin(i) * Math.Sin(w + nu));

        double p = a * (1 - e * e);

        double VX = (X * h * e / (r * p)) * Math.Sin(nu) - (h / r) * (Math.Cos(Om) * Math.Sin(w + nu) +
    Math.Sin(Om) * Math.Cos(w + nu) * Math.Cos(i));
        double VY = (Y * h * e / (r * p)) * Math.Sin(nu) - (h / r) * (Math.Sin(Om) * Math.Sin(w + nu) -
    Math.Cos(Om) * Math.Cos(w + nu) * Math.Cos(i));
        double VZ = -1 * ((Z * h * e / (r * p)) * Math.Sin(nu) - (h / r) * (Math.Cos(w + nu) * Math.Sin(i)));

        Debug.Log("POS: " + X + "\t" + Y + "\t" + Z);
        Debug.Log("VEL: " + VX + "\t" + VY + "\t" + VZ);

        if(isStar)
        {
            gal.addStar(mass, radius, Convert.ToSingle(X) + centralBody.getPos()[0], Convert.ToSingle(Y) + centralBody.getPos()[1], Convert.ToSingle(Z) + centralBody.getPos()[2], Convert.ToSingle(VX) + centralBody.VXi, Convert.ToSingle(VY) + centralBody.VYi, Convert.ToSingle(VZ) + centralBody.VZi, 1, 10000000, name);
        }
        else
        {
            gal.addBody(mass, radius, Convert.ToSingle(X) + centralBody.getPos()[0], Convert.ToSingle(Y) + centralBody.getPos()[1], Convert.ToSingle(Z) + centralBody.getPos()[2], Convert.ToSingle(VX) + centralBody.VXi, Convert.ToSingle(VY) + centralBody.VYi, Convert.ToSingle(VZ) + centralBody.VZi, name);
        }
    }

    float degToRadians(double f)
    {
        return Convert.ToSingle((f * Math.PI) / 180);
    }

    public void setOrbitalParents()
    {
        Dropdown orbitalParentDropdown = GameObject.Find("OrbitalParentDropdown").GetComponent<Dropdown>();
        orbitalParentDropdown.ClearOptions();

        List<Body> orbitalParents = testGalaxy.getBodies();
        List<string> orbitalParentIDs = new List<string>();
        orbitalParentIDs.Add("None");
        foreach (Body b in orbitalParents)
        {
            orbitalParentIDs.Add(b.getID());
        }
        orbitalParentDropdown.AddOptions(orbitalParentIDs);
    }

    public void addBodyToGalaxy()
    {
        Dropdown defineOrbitDropdown = GameObject.Find("DefineOrbitDropdown").GetComponent<Dropdown>();
        Dropdown orbitalParentDropdown = GameObject.Find("OrbitalParentDropdown").GetComponent<Dropdown>();

        InputField mass = GameObject.Find("Mass Input").GetComponent<InputField>();
        InputField radius = GameObject.Find("Radius Input").GetComponent<InputField>();

        bool isStar = GameObject.Find("StarToggle").GetComponent<Toggle>().isOn;

        Body parentBody;
        int orbitalParent = orbitalParentDropdown.value;

        if (defineOrbitDropdown.value == 0)
        {
            InputField ap = GameObject.Find("ApInput").GetComponent<InputField>();
            InputField pe = GameObject.Find("PeInput").GetComponent<InputField>();
            InputField i = GameObject.Find("InclinationInput").GetComponent<InputField>();
            InputField aop = GameObject.Find("AoPInput").GetComponent<InputField>();
            InputField lan = GameObject.Find("LANInput").GetComponent<InputField>();

            parentBody = getOrbitalParent(orbitalParent, true);

            createOrbit(Convert.ToSingle(ap.text), Convert.ToSingle(pe.text),
                Convert.ToSingle(i.text), Convert.ToSingle(aop.text), Convert.ToSingle(lan.text),
                0, 0, testGalaxy, parentBody, Convert.ToSingle(mass.text), Convert.ToSingle(radius.text), isStar, GameObject.Find("NameInput").GetComponent<InputField>().text);
        }
        else if (defineOrbitDropdown.value == 1)
        {
            InputField rx = GameObject.Find("RXInput").GetComponent<InputField>();
            InputField ry = GameObject.Find("RYInput").GetComponent<InputField>();
            InputField rz = GameObject.Find("RZInput").GetComponent<InputField>();

            InputField vx = GameObject.Find("VXInput").GetComponent<InputField>();
            InputField vy = GameObject.Find("VYInput").GetComponent<InputField>();
            InputField vz = GameObject.Find("VZInput").GetComponent<InputField>();

            parentBody = getOrbitalParent(orbitalParent, false);

            if (parentBody.getID().Equals("DEFAULT (ORIGIN)"))
            {
                if (isStar)
                {
                    testGalaxy.addStar(Convert.ToSingle(mass.text), Convert.ToSingle(radius.text),
        Convert.ToSingle(rx.text), Convert.ToSingle(ry.text), Convert.ToSingle(rz.text),
        Convert.ToSingle(vx.text), Convert.ToSingle(vy.text), Convert.ToSingle(vz.text), 1, 10000000, GameObject.Find("NameInput").GetComponent<InputField>().text);
                }
                else
                {
                    testGalaxy.addBody(Convert.ToSingle(mass.text), Convert.ToSingle(radius.text),
                        Convert.ToSingle(rx.text) + parentBody.getPos()[0], Convert.ToSingle(ry.text) + parentBody.getPos()[1], Convert.ToSingle(rz.text) + parentBody.getPos()[2],
                        Convert.ToSingle(vx.text), Convert.ToSingle(vy.text), Convert.ToSingle(vz.text), GameObject.Find("NameInput").GetComponent<InputField>().text);
                }
            }
            else
            {
                if (isStar)
                {
                    testGalaxy.addStar(Convert.ToSingle(mass.text), Convert.ToSingle(radius.text),
        Convert.ToSingle(rx.text) + parentBody.getPos()[0], Convert.ToSingle(ry.text) + parentBody.getPos()[1], Convert.ToSingle(rz.text) + parentBody.getPos()[2],
        Convert.ToSingle(vx.text), Convert.ToSingle(vy.text), Convert.ToSingle(vz.text), 1, 10000000, GameObject.Find("NameInput").GetComponent<InputField>().text);
                }
                else
                {
                    testGalaxy.addBody(Convert.ToSingle(mass.text), Convert.ToSingle(radius.text),
                        Convert.ToSingle(rx.text) + parentBody.getPos()[0], Convert.ToSingle(ry.text) + parentBody.getPos()[1], Convert.ToSingle(rz.text) + parentBody.getPos()[2],
                        Convert.ToSingle(vx.text), Convert.ToSingle(vy.text), Convert.ToSingle(vz.text), GameObject.Find("NameInput").GetComponent<InputField>().text);
                }
            }
        }
        Canvas addBodyCanvas = GameObject.Find("addBodyCanvas").GetComponent<Canvas>();
        addBodyCanvas.enabled = false;

    }

    Body getOrbitalParent(int orbitalParent, bool defaultToBiggestMass)
    {
        if (orbitalParent > 0)
        {
            return testGalaxy.getBodies()[orbitalParent - 1];
        }
        else if (orbitalParent == 0 && testGalaxy.getBodies().Count > 0 && defaultToBiggestMass)
        {
            double maxMass = 0;
            int maxMassIndex = 0;
            for (int j = 0; j < testGalaxy.getBodies().Count; j++)
            {
                if (testGalaxy.getBodies()[j].getMass() > maxMass)
                {
                    maxMass = testGalaxy.getBodies()[j].getMass();
                    maxMassIndex = j;
                }
            }
            return testGalaxy.getBodies()[maxMassIndex];

        }
        else
        {
            return new Body(0, 0, 0, 0, 0, "DEFAULT (ORIGIN)");
        }
    }

    public void saveSystem()
    {
        InputField nameInput = GameObject.Find("SaveNameInput").GetComponent<InputField>();
        string name = nameInput.text + ".sol";
        string[] lines = new string[testGalaxy.getBodies().Count + 1];
        List<Body> bodies = testGalaxy.getBodies();
        //Line zero stores the number of objects in the scene
        lines[0] = Convert.ToString(testGalaxy.getBodies().Count);

        for (int i = 1; i < testGalaxy.getBodies().Count + 1; i++)
        {
            lines[i] = Convert.ToString(bodies[i - 1].getMass()) + " " +
                Convert.ToString(bodies[i - 1].getRadius() / 10) + " " +
                Convert.ToString(bodies[i - 1].getPos()[0]) + " " +
                Convert.ToString(bodies[i - 1].getPos()[1]) + " " +
                Convert.ToString(bodies[i - 1].getPos()[2]) + " " +
                Convert.ToString(bodies[i - 1].getVX() / testGalaxy.getSimulationSpeed()) + " " +
                Convert.ToString(bodies[i - 1].getVY() / testGalaxy.getSimulationSpeed()) + " " +
                Convert.ToString(bodies[i - 1].getVZ() / testGalaxy.getSimulationSpeed()) + " " +
                bodies[i-1].getID() + " " + Convert.ToString(bodies[i-1].isAStar());
        }

        File.WriteAllLines(name, lines);

    }

    public void loadSystemNames()
    {
        string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
        List<string> solFiles = new List<string>();
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].Substring(files[i].Length - 4).Equals(".sol")) {
                solFiles.Add(files[i].Substring(Directory.GetCurrentDirectory().Length + 1, files[i].Length - Directory.GetCurrentDirectory().Length - 5));
                Debug.Log(files[i]);
            }
        }

        Dropdown loadDropdown = GameObject.Find("LoadDropdown").GetComponent<Dropdown>();

        loadDropdown.ClearOptions();
        loadDropdown.AddOptions(solFiles);

    }

    public void loadSystem()
    {
        foreach(Body b in testGalaxy.getBodies()) {
            Destroy(b.getAppearance().gameObject);
        }
        testGalaxy.getBodies().Clear();

        Dropdown loadDropdown = GameObject.Find("LoadDropdown").GetComponent<Dropdown>();
        string fileName = Directory.GetCurrentDirectory() + "\\" + loadDropdown.options[loadDropdown.value].text + ".sol";
        string[] lines = File.ReadAllLines(fileName);

        int numObjects = Convert.ToInt32(lines[0]);

        double[] masses = new double[numObjects];
        float[] radii = new float[numObjects];
        float[] rx = new float[numObjects];
        float[] ry = new float[numObjects];
        float[] rz = new float[numObjects];
        float[] vx = new float[numObjects];
        float[] vy = new float[numObjects];
        float[] vz = new float[numObjects];
        string[] names = new string[numObjects];
        int[] isStar = new int[numObjects];

        string[] currentLine;
        for (int i = 1; i < numObjects + 1; i++)
        {
            currentLine = lines[i].Split(' ');
            masses[i - 1] = Convert.ToDouble(currentLine[0]);
            radii[i - 1] = Convert.ToSingle(currentLine[1]);
            rx[i - 1] = Convert.ToSingle(currentLine[2]);
            ry[i - 1] = Convert.ToSingle(currentLine[3]);
            rz[i - 1] = Convert.ToSingle(currentLine[4]);
            vx[i - 1] = Convert.ToSingle(currentLine[5]);
            vy[i - 1] = Convert.ToSingle(currentLine[6]);
            vz[i - 1] = Convert.ToSingle(currentLine[7]);
            names[i - 1] = currentLine[8];
            isStar[i - 1] = Convert.ToInt32(currentLine[9]);
        }

        for (int i = 0; i < numObjects; i++)
        {
            if(isStar[i] == 0)
            {
                testGalaxy.addBody(masses[i], radii[i], rx[i], ry[i], rz[i], vx[i], vy[i], vz[i], names[i]);
            } else
            {
                testGalaxy.addStar(masses[i], radii[i], rx[i], ry[i], rz[i], vx[i], vy[i], vz[i], 1, 100000000, names[i]);
            }
        }
    }

    public void setSimulationSpeed()
    {
        float newSpeed = GameObject.Find("SimulationSpeedSlider").GetComponent<Slider>().value;
        testGalaxy.setSimulationSpeed(newSpeed);
    }

    public void setSimulationPrecision()
    {
        float newPrecision = GameObject.Find("TimeDeltaSlider").GetComponent<Slider>().value;
        testGalaxy.setSimulationPrecision(newPrecision);
    }

    public void toggleOrbitalPaths()
    {
        Toggle orbitalPathToggle = GameObject.Find("OrbitalPathToggle").GetComponent<Toggle>();
        foreach (Body b in testGalaxy.getBodies())
        {
            b.showPath(orbitalPathToggle.isOn);
        }
    }
}
