using System.Collections.Generic;

public class Chat 
{
    public bool isOpen;
    public List<ChatMessage> ChatMessages = new List<ChatMessage>();
    public int maxMessages = 25;
}
