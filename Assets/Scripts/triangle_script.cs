using System;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
public class triangle_script : MonoBehaviour
{
    private int up, down, left, right;
    private bool toggleChangeColor;
    //Using Events.
    //This allows a clearer one-line approach
    //of updating various attributes in respnose
    //to player control input which saves effort.
    public event Action changePosition;
    public event Action changeColor;
    private Vector2 ds;
    private Transform ts;
    private SpriteRenderer sr;
    private float lastChangeTime = 0.0f;
    private int currentColor = 0;
    private Color[] colors = {new Color(1.0f, 1.0f, 1.0f), new Color(1.0f, 0.0f, 0.0f), new Color(0.0f, 1.0f, 0.0f), new Color(0.0f, 0.0f, 1.0f)};
    //Using Attributes.
    //This allows quicker testing by enabling changing
    //various attributes of this object in the editor
    //instead of manually changing the entire code.
    [Header("Custom Fields")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float colorCoolDown;

    public void GetKey() 
    {
        up = Convert.ToInt32(Input.GetKey(KeyCode.UpArrow));
        down = Convert.ToInt32(Input.GetKey(KeyCode.DownArrow));
        left = Convert.ToInt32(Input.GetKey(KeyCode.LeftArrow));
        right = Convert.ToInt32(Input.GetKey(KeyCode.RightArrow));
        toggleChangeColor = Input.GetKey(KeyCode.E);
    }
    public Vector2 GetDs(float speed)
    {
        Vector2 ds = new Vector2((float)(right - left), (float)(up - down)) * Time.deltaTime * speed;
        return ds;
    }
    void UpdateColor()
    {
        if(Time.time - lastChangeTime > colorCoolDown && toggleChangeColor)
        {
            lastChangeTime = Time.time;
            currentColor += 1;
            if(currentColor > 3) currentColor = 0;
            sr.color = colors[currentColor];
        }
    }
    void DebugLogColor()
    {
        Console.WriteLine("ColorRGB: ({0}, {1}, {2})", sr.color.r, sr.color.g, sr.color.b);
    }
    void UpdatePosition()
    {
        Vector3 new_pos = ts.position + new Vector3(ds.x, ds.y, 0.0f);
        float right = (float)Screen.width / 2.0f;
        float left = -right;
        float up = (float) Screen.height / 2.0f;
        float down = -up;
        if (new_pos.x > right) new_pos.x = right;
        if (new_pos.x < left) new_pos.x = left;
        if (new_pos.y > up) new_pos.y = up;
        if (new_pos.y < down) new_pos.y = down;
        ts.position = new_pos;
    }
    void SetDs()
    {
        ds = GetDs(speed);
    }
    void DebugLogPos()
    {
        Console.WriteLine("Position: ({0}, {1}, {2})", ts.position.x, ts.position.y, ts.position.z);
    }

    void Start()
    {
        //Using generics of Unity API.
        //This gets the current object of "transformation"
        //and "sprite renderer" which will need to be updated.
        ts = GetComponent<Transform> ();
        ts.position = new Vector3(0.0f, 0.0f, 0.0f);

        sr = GetComponent<SpriteRenderer> ();
        sr.color = new Color(1.0f, 1.0f, 1.0f);

        changePosition += GetKey;
        changePosition += SetDs;
        changePosition += UpdatePosition;
        changePosition += DebugLogPos;

        //Get Key already done in the first event
        changeColor += UpdateColor;
        changeColor += DebugLogColor;
    }
    // Update is called once per frame
    void Update()
    {
        changePosition?.Invoke();
        changeColor?.Invoke();
    }
}
