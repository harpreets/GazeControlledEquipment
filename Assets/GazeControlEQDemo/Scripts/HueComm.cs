using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net;

public class HueComm : MonoBehaviour {

    public int testLight = 2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Control"))
        {
            //string json = "{\"on\":false}";
            //Debug.Log(json.ToString());
            SendOffMessageToLight(testLight);
            Debug.Log("off pressed");
        }

        if (Input.GetButton("Control2"))
        {
            SendOnMessageToLight(testLight);
            Debug.Log("on pressed");
        }
    }

    public void SendOnMessageToLight(int lightID)
    {
        if(lightID < 1 && lightID > 3) { return;  }

        Uri uri = new Uri("http://192.168.2.3/api/130701522fff357723b2c16e194fefa3/lights/" + lightID.ToString() + "/state");
        var httpWebRequest = WebRequest.Create(uri);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "PUT";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{\"on\":true}";

            streamWriter.Write(json);
        }

        using (httpWebRequest.GetResponse()) { }

        //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //{
        //    var responseText = streamReader.ReadToEnd();
        //    return true;
        //}
    }

    public void SendOffMessageToLight(int lightID)
    {
        Uri uri = new Uri("http://192.168.2.3/api/130701522fff357723b2c16e194fefa3/lights/" + lightID.ToString() + "/state");
        var httpWebRequest = WebRequest.Create(uri);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "PUT";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{\"on\":false}";

            streamWriter.Write(json);
            streamWriter.Close();
        }


        using (httpWebRequest.GetResponse()) { }

        //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //{
        //	var responseText = streamReader.ReadToEnd();
        //	return true;
        //}
    }
}
