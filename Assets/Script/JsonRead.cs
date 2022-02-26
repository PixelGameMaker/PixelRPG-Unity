using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class JsonRead : MonoBehaviour
{
    private void Start()
    {
        // Read JSON file to string
        var json = File.ReadAllText(@"Assets\Json\character_data\enemy.json");
        // Prettify the JSON string
        var prettyJson = JToken.Parse(json).ToString();
        // Print the prettified JSON string
        Debug.Log("String JSON");
        Debug.Log(prettyJson);
        // Parse the JSON string to a JSON object
        var jsonObject = JObject.Parse(json);
        // Print the JSON object
        Debug.Log("Object JSON");
        Debug.Log(jsonObject);
    }
}