using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsReader
{
    public string IP;
    public string Username;

    public SettingsReader()
    {
        string[] lines = System.IO.File.ReadAllLines(@"C:\Bloodrun\BloodrunProperties.props");

        IP = lines[0];
        Username = lines[1];
    }
}
