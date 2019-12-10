using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ScaleInfo : ScriptableObject
{
    public string x;
    public string y;
    public string z;

    public ScaleInfo()
    {
        x = "0";
        y = "0";
        z = "0";
    }

    public ScaleInfo(string x, string y, string z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static ScaleInfo FromJson(JToken token)
    {
        ScaleInfo scale = new ScaleInfo();

        scale.x = (string)token.SelectToken("x");
        scale.y = (string)token.SelectToken("y");
        scale.z = (string)token.SelectToken("z");

        return scale;
    }
}
