using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapInfo
{
    public int id;
    public TransformInfo transform;
    public bool activated;
    public TrapType type;
    public ScaleInfo scale;

    public GameObject trap;

    public TrapInfo()
    {
        activated = false;
        id = 0;
    }

    public TrapInfo(int id, TransformInfo transform, ScaleInfo scale, TrapType type)
    {
        this.id = id;
        this.transform = transform;
        this.type = type;
        this.scale = scale;
        activated = false;
    }

    public static TrapInfo FromJson(JToken token)
    {
        TrapInfo trap = new TrapInfo();

        trap.id = int.Parse(token.SelectToken("id").ToString());
        trap.transform = TransformInfo.FromJson(token.SelectToken("transform"));
        trap.scale = ScaleInfo.FromJson(token.SelectToken("scale"));
        trap.activated = (bool)token.SelectToken("activated");
        trap.type = (TrapType)int.Parse(token.SelectToken("type").ToString());

        return trap;
    }
}
