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
    
    // Update is called once per frame
    void Update()
    {
        ShowChatByKey();
        hideChatByKey();
        addmessagedebugger();
        sendMessage();
    }

    private void addmessagedebugger()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChatMessage mes = new ChatMessage("hoi", "mario", MessageType.CHAT);
            AddMessageToChat(mes);
        }
    }

    public void sendMessage()
    {
        if (chatField.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return) && chat.isOpen)
            {
                ConnectionManager ConnectionManager = gameObject.GetComponent(typeof(ConnectionManager)) as ConnectionManager;

                ChatMessage message = new ChatMessage(chatField.text, ConnectionManager.Username, MessageType.CHAT);
                AddMessageToChat(message);
                chatField.text = "";
                chatBox.SetActive(false);
                chat.isOpen = false;
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
