using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aim : MonoBehaviour
{
    Camera maincam;
    PlayerControl pc;
    [HideInInspector] public Vector2 mousePos;
    public float rotz;
    void Start()
    {
        maincam = Camera.main;
        pc = Player.pc;
    }
    void Update()
    {
        mouseMove();
    }
    private void mouseMove()
    {
        mousePos = pc.Player.MousePos.ReadValue<Vector2>(); //posisi mouse
        mousePos = maincam.ScreenToWorldPoint(mousePos);
        var x = mousePos.x - transform.position.x;
        var y = mousePos.y - transform.position.y;
        var z = transform.position.z;
        Vector3 rotation = new Vector3(x, y, z);
        rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz);
    }
}
