using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Body
{
    private Light light;

    public Star(double m, float r, float x, float y, float z, float lightVal, float lightRange, string id) : base(m, r, x, y, z, id, Color.yellow)
    {
        Light light = appearance.AddComponent<Light>();
        light.intensity = lightVal;
        light.range = lightRange;

    }

    public Star(double m, float r, float x, float y, float z, float vX, float vY, float vZ, float lightVal, float lightRange, string id) : base(m, r, x, y, z, vX, vY, vZ, id, Color.yellow)
    {
        Light light = appearance.AddComponent<Light>();
        light.intensity = lightVal;
        light.range = lightRange;
    }

}
