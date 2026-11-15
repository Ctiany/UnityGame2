using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpdateText : MonoBehaviour
{
    private TextMeshPro tm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tm = GetComponent<TextMeshPro>();
    }
    public void Update_Text(System.String text)
    {
        tm.SetText(text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
