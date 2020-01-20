using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointInfo
{
    public TransformInfo transform = new TransformInfo();
    public ScaleInfo scale;

    public static CheckpointInfo FromJson(JToken token)
    {
        CheckpointInfo checkpoint = new CheckpointInfo();
        checkpoint.transform = TransformInfo.FromJson(token.SelectToken("transform"));
        checkpoint.scale = ScaleInfo.FromJson(token.SelectToken("scale"));

        return checkpoint;
    }
}
