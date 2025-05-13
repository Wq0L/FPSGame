using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
   public static Action shootInput;


    private void Update()
    {
        if(InputManager.Instance.IsFiring())
        {
            shootInput?.Invoke();
        }
    }

}
