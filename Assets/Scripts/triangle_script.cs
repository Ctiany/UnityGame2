using System;
using UnityEngine;
public class mysterious_obj: MonoBehaviour
{
    private float speed;
    private int up, down, left, right;
    private bool changecolor;
    private void Awake() //Using Awake() function
    {
        speed = 5f; //setting variable
    }
    public void changeSpeed(float new_speed)
    {
        speed = new_speed; //Custom method with parameter
    }
    public void GetKey() 
    {
        up = Convert.ToInt32(Input.GetKey(KeyCode.UpArrow));
        down = Convert.ToInt32(Input.GetKey(KeyCode.DownArrow));
        left = Convert.ToInt32(Input.GetKey(KeyCode.LeftArrow));
        right = Convert.ToInt32(Input.GetKey(KeyCode.RightArrow));
        changecolor = Input.GetKey(KeyCode.E);
        //Modifying fields & properties
    }
    public Vector2 GetDs()
    {
        Vector2 ds = new Vector2(0.0f, 0.0f);
        ds.x = (float)(right - left) * Time.deltaTime * speed;
        ds.y = (float)(up - down) * Time.deltaTime * speed;
        //Accessing fields & properties (in this case accessing speed)
        
        return ds;
    }
    public bool shouldChangeColor()
    {
        return changecolor;
    }

}
public class triangle_script : mysterious_obj //using Polymorphism
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector2 ds;
    void Start()
    {
        //Everything right now has been done in Awake(); nothing to do
        changeSpeed(10.0f); //calling custom method
    }
    void ChangeColor()
    {
        //To be done later
    }
    // Update is called once per frame
    void Update()
    {
        GetKey();
        ds = GetDs();
        Debug.Log("ds:" + Convert.ToString(ds.x) + "; " + Convert.ToString(ds.y));
        Debug.Log("ChangeColor: " + Convert.ToString(shouldChangeColor()));
        //Print to debug console
    }
}
