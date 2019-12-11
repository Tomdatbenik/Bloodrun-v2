using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic
{
    public void HandleGameMessage(Message message)
    {
        GameManager.game = Game.Fromjson(message.content);
    }

}
