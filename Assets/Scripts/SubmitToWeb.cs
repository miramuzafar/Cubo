using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.Linq;

public class SubmitToWeb : MonoBehaviour {
    public Text test;
    //Spawn.dinosaurs = new List<string>();

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
	}

    public void SendToWeb()
    {
        GameObject thePlayer = GameObject.Find("SpawnManager");
        Spawn playerScript = thePlayer.GetComponent<Spawn>();
        Dictionary<string, int> yohoo = playerScript.ItemArray;


        foreach (var item in yohoo)
        {
            //Debug.Log(item);
            Application.ExternalCall("SayToWeb", item.Key, item.Value, item);
        }


     
        //use to pass item and price to web when checkout

            //Application.ExternalCall("SayToWeb", "The game says hello!", "2", yohoo);

        
     //   foreach (string dinosaur in dinosaurs)
      //  {
      //      Debug.Log(dinosaur);
      //  }
        
    }

    public void ReceivedFromWeb(string s)
    {
        //Text myString = GetComponentInChildren<Text>();
        test.text = s;
        //Debug.Log(myString);
    }
    
}


/*
 * 
 * web javascript
 * need to call function SaySomethingToUnity to send params

 * 
 * 
 * <!DOCTYPE html>
<html lang="en-us">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <title>Unity WebGL Player | cuboproject</title>
    <link rel="shortcut icon" href="TemplateData/favicon.ico">
    <link rel="stylesheet" href="TemplateData/style.css">
    <script src="TemplateData/UnityProgress.js"></script>  
    <script src="Build/UnityLoader.js"></script>
    <script>
      var gameInstance = UnityLoader.instantiate("gameContainer", "Build/unity_cubo.json", {onProgress: UnityProgress});
    </script>
  </head>
<body>
    <div class="webgl-content">
        <div id="gameContainer" style="width: 960px; height: 560px"></div>
        <div class="footer">
            <div class="webgl-logo"></div>
            <div class="fullscreen" onclick="gameInstance.SetFullscreen(1)"></div>
            <div class="title">cuboproject</div>

            <button onclick="SaySomethingToUnity()" id="autoclick" >Web to Unity</button>

        </div>
    </div>
</body>

    
          Params 1:<input type="text" id="p1" value=""><br>
          Params 2:<input type="text" id="p2" value=""><br>
          Params 3:<input type="text" id="p3" value=""><br>
</html>


<script type="text/javascript" language="javascript">



    function SaySomethingToUnity() {
        gameInstance.SendMessage("GameObject", "ReceivedFromWeb", "Hello from a web page!");
    }

    //
    // function SubmitToWeb(param : String)
    // {
    //     Debug.Log(param);
    // }


    function SayToWeb(arg,bb,cc) {
        // show the message
        alert(arg + bb + cc);
        document.getElementById("p1").value = arg;
        document.getElementById("p2").value = bb;
        document.getElementById("p3").value = cc;

    }


</script>
 * 
 * 
 * 
 * 
*/