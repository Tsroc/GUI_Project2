using System.IO;
using System.Text;  // for stringbuilder
using UnityEngine;
using UnityEngine.Windows.Speech;   // grammar recogniser

public class GameGrammarController : MonoBehaviour
{
    private GrammarRecognizer gr;
    private GrammarAdaptor grammarAdaptor;

    private void Start()
    {
        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath,
                                                "GameGrammar.xml"),
                                    ConfidenceLevel.Low);
        Debug.Log("Grammar loaded!");
        gr.OnPhraseRecognized += GR_OnPhraseRecognized;
        gr.Start();
        if (gr.IsRunning) Debug.Log("Recogniser running");

        grammarAdaptor = GetComponent<GrammarAdaptor>();

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
        // Move
        if (arg.ToLower().Equals("left"))
        {
            grammarAdaptor.PlayerAction("move", "left");
        }
        else if (arg.ToLower().Equals("right"))
        {
            grammarAdaptor.PlayerAction("move", "right");
        }
        // Step
        else if (arg.ToLower().Equals("step left"))
        {
            grammarAdaptor.PlayerAction("step", "left");
        }
        else if (arg.ToLower().Equals("step right"))
        {
            grammarAdaptor.PlayerAction("step", "right");
        }
        // Jump
        else if (arg.ToLower().Equals("jump left"))
        {
            grammarAdaptor.PlayerAction("jump", "left");
        }
        else if (arg.ToLower().Equals("jump right"))
        {
            grammarAdaptor.PlayerAction("jump", "right");
        }
        else if (arg.ToLower().Equals("jump up"))
        {
            grammarAdaptor.PlayerAction("jump", "up");
        }
        // Dash 
        else if (arg.ToLower().Equals("dash left"))
        {
            grammarAdaptor.PlayerAction("dash", "left");
        }
        else if (arg.ToLower().Equals("dash right"))
        {
            grammarAdaptor.PlayerAction("dash", "right");
        }
        // Stop
        else if (arg.ToLower().Equals("stop"))
        {
            grammarAdaptor.StopMovement();
        }
        // Portal
        else if (arg.ToLower().Equals("portal"))
        {
            grammarAdaptor.PlayerInteractions("portal");
        }



    }

}
