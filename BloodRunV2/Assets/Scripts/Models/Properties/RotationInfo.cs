using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationInfo : ScriptableObject
{
    public string x;
    public string y;
    public string z;
    public string w;

    public RotationInfo()
    {
        x = "0";
        y = "0";
        z = "0";
        w = "0";
    }

    public RotationInfo(string x, string y, string z, string w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public static RotationInfo FromJson(JToken token)
    {
        RotationInfo rotation = new RotationInfo();

        rotation.x = (string)token.SelectToken("x");
        rotation.y = (string)token.SelectToken("y");
        rotation.z = (string)token.SelectToken("z");
        rotation.w = (string)token.SelectToken("w");

        return rotation;
    }
}
