using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ScaleInfo
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

        if (CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator == ".")
        {
            scale.x = scale.x.Replace(",", ".");
            scale.y = scale.y.Replace(",", ".");
            scale.z = scale.z.Replace(",", ".");
        }

        return scale;
    }
}
