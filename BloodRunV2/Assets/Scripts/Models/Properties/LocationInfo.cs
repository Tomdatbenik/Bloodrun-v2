using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class LocationInfo : ScriptableObject
{
    public string x;
    public string y;
    public string z;

    public LocationInfo()
    {
        x = "0";
        y = "0";
        z = "0";
    }

    public LocationInfo(string x, string y, string z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static LocationInfo FromJson(JToken token)
    {
        LocationInfo location = new LocationInfo();

        location.x = (string)token.SelectToken("x");    
        location.y = (string)token.SelectToken("y");
        location.z = (string)token.SelectToken("z");

        return location;
    }
}