using UnityEngine;
using System.Collections;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;

using System;

public class mqttTest : MonoBehaviour 
{
	private MqttClient client;

    public string IpToParse = "10.0.123.12";

    // Use this for initialization
    public void Action () 
    {
		// create client instance 
		client = new MqttClient(IPAddress.Parse(IpToParse),8080 , false , null ); 
		
		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		
		string clientId = Guid.NewGuid().ToString();
      
        Debug.Log(clientId);

        client.Connect(clientId); 
		
		// subscribe to the topic "/home/temperature" with QoS 2 
		client.Subscribe(new string[] { "hello/world" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 

	}

    private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 

		Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
	} 

	private void OnGUI()
    {
		if ( GUI.Button (new Rect (20,40,80,20), "Level 1")) 
        {
			Debug.Log("sending...");
			client.Publish("hello/world", System.Text.Encoding.UTF8.GetBytes("Sending from Unity3D!!!"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
			Debug.Log("sent");
		}
	}
}
