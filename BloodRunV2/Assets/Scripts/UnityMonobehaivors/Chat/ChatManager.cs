using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public GameObject chatBox;
    public GameObject chatPanel;
    public GameObject textObject;
    public TMP_InputField chatField;

    public readonly Chat chat = new Chat();

    public static ChatMessage ChatMessage;
    
    // Update is called once per frame
    void Update()
    {
        ShowChatByKey();
        hideChatByKey();
        sendMessage();

        if(ChatMessage != null)
        {
            AddMessageToChat(ChatMessage);
            ChatMessage = null;
        }
    }

    public void sendMessage()
    {
        if (chatField.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return) && chat.isOpen)
            {
                GameObject bloodrun = GameObject.Find("Bloodrun");
                ConnectionManager ConnectionManager = bloodrun.GetComponent(typeof(ConnectionManager)) as ConnectionManager;

                ChatMessage message = new ChatMessage(chatField.text, ConnectionManager.Username);
                AddMessageToChat(message);
                chatField.text = "";
                chatBox.SetActive(false);
                chat.isOpen = false;

                ConnectionManager.connection.TcpClient.Sender.TrySend(new Message(ConnectionManager.Username, message.content, MessageType.CHAT));
            }
        }
    }
    
    public void ShowChatByKey()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !chat.isOpen)
        {
            Debug.Log("open");
            chatBox.SetActive(true);
            chatField.Select();
            chat.isOpen = true;
        }
    }
    
    public void ShowChat()
    {
        chatBox.SetActive(true);
    }
    
    public void hideChatByKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && chat.isOpen)
        {
                 Debug.Log("close");
                chatBox.SetActive(false);
                chat.isOpen = false;
        }
    }

    public void AddMessageToChat(ChatMessage message)
    {
        if (chat.ChatMessages.Count >= chat.maxMessages)
        {
            Destroy(chat.ChatMessages[0].textObject.gameObject);
            chat.ChatMessages.Remove(chat.ChatMessages[0]);
        }
       
        GameObject chatMessage = Instantiate(textObject, chatPanel.transform);
        message.textObject = chatMessage.GetComponent<TextMeshProUGUI>();
        message.textObject.text = $"{message.sender} || {message.content}";
        chat.ChatMessages.Add(message);
    }
}
