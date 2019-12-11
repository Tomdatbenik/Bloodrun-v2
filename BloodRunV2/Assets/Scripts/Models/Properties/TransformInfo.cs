using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformInfo
{
    public LocationInfo location;
    public RotationInfo rotation;

    public TransformInfo()
    {
        location = new LocationInfo();
        rotation = new RotationInfo();
    }

    public static TransformInfo FromJson(JToken token)
    {
        TransformInfo transform = new TransformInfo();

        transform.location = LocationInfo.FromJson(token.SelectToken("location"));
        transform.rotation = RotationInfo.FromJson(token.SelectToken("rotation"));

        return transform;
    }
}
