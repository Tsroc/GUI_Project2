using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instruction : MonoBehaviour
{
    [SerializeField] private string instruction;
    [SerializeField] private Text instructionText;
    [SerializeField] private Color colour;

    public void Activate()
    {
        if(instruction != "Destroy")
        {
            DisplayInstruction();
        }
        else
        {
            DestroyInstruction();
        }
    }

    private void DisplayInstruction()
    {
        instructionText.color = colour;
        instructionText.text = instruction;
    }

    private void DestroyInstruction()
    {
        instructionText.text = "";
        Destroy(gameObject.transform.parent.gameObject);
    }
}
