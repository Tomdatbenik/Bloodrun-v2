
using Newtonsoft.Json.Linq;
using UnityEngine;

public class FinishInfo
{
    public TransformInfo transform = new TransformInfo();
    public ScaleInfo scale;

    public static FinishInfo FromJson(JToken token)
    {
        FinishInfo finish = new FinishInfo();
        finish.transform = TransformInfo.FromJson(token.SelectToken("transform"));
        finish.scale = ScaleInfo.FromJson(token.SelectToken("scale"));

        return finish;
    }
}
