using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class Chat 
{
    public bool isOpen;
    public List<Message> ChatMessages = new List<Message>();
    public int maxMessages = 25;
}
