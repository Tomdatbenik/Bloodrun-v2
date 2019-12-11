using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    public string sender;
    public string content;
    public MessageType type;

    #region Getters and Setters
    public string getContent()
    {
        return content;
    }

    public void setContent(string content)
    {
        this.content = content;
    }

    public MessageType getType()
    {
        return type;
    }

    public void setType(MessageType type)
    {
        this.type = type;
    }
    #endregion

    public Message()
    {
        content = "";
        type = MessageType.CONNECT;
    }

    public Message(string sender,string content, MessageType type)
    {
        this.sender = sender;
        this.content = content;
        this.type = type;
    }

   /// <summary>
   /// For Debug purposes
   /// </summary>
   /// <returns></returns>
    public string toString()
    {
        return "Content: " + this.content + ", Type: " + type.ToString() + ".";
    }

    /// <summary>
    /// Generate JSON from this message
    /// </summary>
    /// <returns></returns>
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    /// <summary>
    /// Get Message from Json
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static Message FromJson(string json)
    {
        try
        {
            Message message = JsonConvert.DeserializeObject<Message>(json);
            if (message.sender != null || message.sender != "")
            {
                return message;
            }
            else
            {
                return null;
            }
            
        }
        catch
        {
            return null;
        }
      
    }
}
