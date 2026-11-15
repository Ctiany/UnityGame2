using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;

public class rotating_circles : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float layer_separation_pt;
    public int number_of_layers;
    public int number_per_layer;
    public float speed; //linear or angular, depending on the user setting below
    public int void_layers; //the first [void_layers] layers won't be instantiated.
    public enum Linear_Angular {
        LINEAR,
        ANGULAR,
    };
    public Linear_Angular linear_angular;
    public GameObject center_obj;
    public GameObject yellow_circle_obj;
    private Vector3 center_coordinates; //we dont edit z component
    private bool should_freeze;
    private float offset_angle;
    private float current_time; //keep the same time over a loop to prevent mild increases interfering with angular displacement
    private List<(GameObject, int, int)> yellow_circles; //prefab objects of yellow circles
    //<Game Object, Layer Number, In Layer Number>
    public void freeze() //called by unity event
    {
        should_freeze = true;
    }
    private Vector3 get_single_position(int layer_number, int acw_number_offset)
    {
        //layer_number starts at 1, acw_number_offset starts at 0
        float radius = (float) layer_number * layer_separation_pt;
        float angular_speed = linear_angular == Linear_Angular.ANGULAR ? speed : (speed / radius);
        //angular displacement = angular speed * time
        float angular_disp = current_time * angular_speed;
        //calculate offset
        angular_disp += offset_angle * acw_number_offset;
        float x = radius * math.cos(angular_disp), y = radius * math.sin(angular_disp);
        Vector3 pos = new Vector3(x, y, 0);
        pos += center_coordinates;
        return pos;
    }
    void update_position()
    {
        foreach ((GameObject current_circle, int i, int j) in yellow_circles)
        {
            current_circle.transform.position = get_single_position(i, j);
        }
    }
    void Start()
    {
        center_coordinates = center_obj.transform.position;
        should_freeze = false;
        offset_angle = 6.283185307f / (float)number_per_layer;
        //fill yellow circles
        yellow_circles = new List<(GameObject, int, int)>();
        for(int i=void_layers + 1; i<=number_of_layers; i++) //layer starts at 1
        {
            for(int j=0; j<number_per_layer; j++) //offset starts at 0
            {
                GameObject current_obj = Instantiate(yellow_circle_obj);
                current_obj.transform.position = get_single_position(i, j);
                yellow_circles.Add((current_obj, i, j));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        current_time = Time.time;
        if (!should_freeze)
        {
            update_position();
        }
    }
}
