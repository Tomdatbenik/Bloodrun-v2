using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatMessage
{
   public string content;
   public string sender;
   public MessageType messageType;
   public TextMeshProUGUI textObject;

   public ChatMessage(string Content, string Sender)
   {
      this.content = Content;
      this.sender = Sender;
   }
}
