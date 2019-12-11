using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Message
{
   public string content;
   public string sender;
   public MessageEnum messageType;
   public TextMeshProUGUI textObject;

   public Message(string Content, string Sender, MessageEnum MessageType)
   {
      this.content = Content;
      this.sender = Sender;
      this.messageType = MessageType;
   }
}
