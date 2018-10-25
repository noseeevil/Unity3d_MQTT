using UnityEngine;
using System.Collections;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt.Utility;
using uPLibrary.Networking.M2Mqtt.Exceptions;

using System;
using UnityEngine.UI;

public class mqttTest : MonoBehaviour 
{
	private MqttClient client;

    public string BrokerIp= "192.168.42.1";
    public int BrokerPort = 1883;
    public string TopicText = "";
    public string MessageText = "";

    public Text ConnectStatusText;

    private void Start()
    {
        //Debug.Log("Screen - " + Screen.width.ToString() + "  " + Screen.height.ToString());
    }

    // Use this for initialization
    public void Connect() 
    {
        // create client instance 
        //Debug.Log(BrokerIp);
		client = new MqttClient(IPAddress.Parse(BrokerIp), BrokerPort, false , null ); 
		
		// register to message received 
		client.MqttMsgPublishReceived += client_MqttMsgPublishReceived; 
		
		string clientId = Guid.NewGuid().ToString();
      
        //Debug.Log(clientId);

        client.Connect(clientId);

        //Debug.Log("Connected - " + BrokerIp);
        ConnectStatusText.text = "Connected - " + BrokerIp;
        // subscribe to the topic "/home/temperature" with QoS 2 
        //client.Subscribe(new string[] { TopicText }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }); 

    }

    private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) 
	{ 

		Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message)  );
	} 

    public void Send(string topic, string msg)
    {
        client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(msg), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        ConnectStatusText.text = "Sent - " + topic + "/" + msg;
    }

    public void Send()
    {
		Debug.Log("[Sending...]");
        //client.Publish("hello/world", System.Text.Encoding.UTF8.GetBytes("Sending from Unity3D!!!"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        client.Publish(TopicText, System.Text.Encoding.UTF8.GetBytes(MessageText), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        Debug.Log("[Sent]");
	}

    public void RemoteControl(string msg)
    {
        Send("/devices/shedule0/controls/virtual/on", msg);
    }

    public void VirtualNight(string msg)
    {
        Send("/devices/shedule7/controls/virtual_night/on", msg);
    }

    public void VirtualLeak(string msg)
    {
        Send("/devices/shedule1/controls/virtual_leak/on", msg);
    }

    public void VirtualRadiation(string msg)
    {
        Send("/devices/shedule5/controls/virtual_radiation/on", msg);
    }

    public void VirtualVoltage(string msg)
    {
        Send("/devices/shedule3/controls/virtual_voltage/on", msg);
    }

    public void VirtualNose1(string msg)
    {
        Send("/devices/shedule4/controls/virtual_nose1/on", msg);
    }

    public void VirtualNose2(string msg)
    {
        Send("/devices/shedule4/controls/virtual_nose2/on", msg);
    }

    public void VirtualIntruder(string msg)
    {
        Send("/devices/shedule2/controls/virtual_intruder/on", msg);
    }

}
