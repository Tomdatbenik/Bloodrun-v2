using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class Chat 
{
    public bool isOpen;
    public List<ChatMessage> ChatMessages = new List<ChatMessage>();
    public int maxMessages = 25;
}
