using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;  // for stringbuilder
using UnityEngine;
using UnityEngine.Windows.Speech;   // grammar recogniser

public class GameGrammarController : MonoBehaviour
{
    private GrammarRecognizer gr;
    private GameObject player;

    private void Start()
    {
        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath,
                                                "GameGrammar.xml"),
                                    ConfidenceLevel.Low);
        Debug.Log("Grammar loaded!");
        gr.OnPhraseRecognized += GR_OnPhraseRecognized;
        gr.Start();
        if (gr.IsRunning) Debug.Log("Recogniser running");

        player = GameObject.FindGameObjectWithTag("Player"); 

    }

    private void GR_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder message = new StringBuilder();
        SemanticMeaning[] meanings = args.semanticMeanings;
        string keyString;
        string valueString = "";

        foreach (SemanticMeaning meaning in meanings)
        {
            keyString = meaning.key.Trim();
            valueString = meaning.values[0].Trim();
            message.Append("Key: " + keyString + ", Value: " + valueString + " ");
        }
        Debug.Log(message);
        DecideAction(valueString);
    }

    private void DecideAction(string arg)
    {
        // Moveemnt
        if (arg.ToLower().Equals("left"))
        {
            player.GetComponent<Player>().Movement("left");
        }
        else if (arg.ToLower().Equals("right"))
        {
            player.GetComponent<Player>().Movement("right");
        }
        // Step
        else if (arg.ToLower().Equals("step left"))
        {
            player.GetComponent<Player>().MovementStep("left");
        }
        else if (arg.ToLower().Equals("step right"))
        {
            player.GetComponent<Player>().MovementStep("right");
        }
        // Jump
        else if (arg.ToLower().Equals("jump left"))
        {
            player.GetComponent<Player>().Jump("left");
        }
        else if (arg.ToLower().Equals("jump right"))
        {
            player.GetComponent<Player>().Jump("right");
        }
        else if (arg.ToLower().Equals("jump up"))
        {
            player.GetComponent<Player>().Jump("up");
        }
        // Stop
        else if (arg.ToLower().Equals("stop"))
        {
            player.GetComponent<Player>().ClearDestination();
        }
        // Portal
        else if (arg.ToLower().Equals("portal"))
        {
            player.GetComponent<Player>().InteractWithPortal();
        }
        // Teleportal
        else if (arg.ToLower().Equals("teleport"))
        {
            player.GetComponent<Player>().InteractWithTeleport();
        }



    }

}
