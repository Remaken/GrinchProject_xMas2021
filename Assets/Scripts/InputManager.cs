using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Audio;

public class InputManager : MonoBehaviour
{
   /*public delegate void Movement(float inputValue);
   public static event Movement MovementEvent;*/
   public delegate void MovementInputs();

   public static event MovementInputs MoveRight;

   public static event MovementInputs MoveLeft;

   public static event MovementInputs DropItem;



   private void Update()
   {
    MovementInput();
    DropInput();
   }

   private void MovementInput()
   {
      /*if (Input.GetAxis("Horizontal") != 0f)
      {
         MovementEvent?.Invoke(Input.GetAxis("Horizontal"));
      }*/

      if (Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow))
      {
         MoveRight?.Invoke();
      }
      if (Input.GetKeyDown(KeyCode.Q)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow))
      {
         MoveLeft?.Invoke();
      }
   }

   private void DropInput()
   {
      if (Input.GetKeyDown(KeyCode.G))
      {
         DropItem?.Invoke();
      }
   }
}
