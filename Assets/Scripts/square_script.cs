using System;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class square_script : MonoBehaviour
{
    public float move_speed, colorCoolDown;
    private Transform ts;
    private SpriteRenderer sr;
    public UnityEvent onCollideYellow;
    public UnityEvent onCollideRed;
    private int up, down, left, right;
    bool toggleChangeColor;
    private float lastChangeTime = 0.0f;
    private int currentColor = 0;
    private Color[] colors = {new Color(1.0f, 1.0f, 1.0f), new Color(1.0f, 0.0f, 0.0f), new Color(0.0f, 1.0f, 0.0f), new Color(0.0f, 0.0f, 1.0f)};
    private void GetKey()
    {
        up = Convert.ToInt32(Input.GetKey(KeyCode.UpArrow));
        down = Convert.ToInt32(Input.GetKey(KeyCode.DownArrow));
        left = Convert.ToInt32(Input.GetKey(KeyCode.LeftArrow));
        right = Convert.ToInt32(Input.GetKey(KeyCode.RightArrow));
        toggleChangeColor = Input.GetKey(KeyCode.E);
        
    }
    private void UpdateColor()
    {
        if(Time.time - lastChangeTime > colorCoolDown && toggleChangeColor)
        {
            lastChangeTime = Time.time;
            currentColor += 1;
            if(currentColor > 3) currentColor = 0;
            sr.color = colors[currentColor];
        }
    }
    private void UpdatePosition()
    {
        Vector3 ds = new Vector3((float)(right - left), (float)(up - down), 0.0f) * Time.deltaTime * move_speed;
        Vector3 new_pos = ts.position + ds;
        if (new_pos.x > 1200) new_pos.x = 1200;
        if (new_pos.x < -1200) new_pos.x = -1200;
        if (new_pos.y > 750) new_pos.y = 750;
        if (new_pos.y < -750) new_pos.y = -750;
        ts.position = new_pos;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ts = GetComponent<Transform> ();
        sr = GetComponent<SpriteRenderer> ();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Yellow Circle(Clone)")
        {
            //Collided with yellow circle, lose
            move_speed = 0.0f;
            onCollideYellow?.Invoke();
        }
        else if (collision.name == "Circle")
        {
            //Collided with red circle, win
            move_speed = 0.0f;
            onCollideRed?.Invoke();
        }
    }
    // Update is called once per frame
    void Update()
    {
        GetKey();
        UpdatePosition();
        UpdateColor();
    }
}
