using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastIntoScene : MonoBehaviour
{
   [SerializeField] private Camera _camera;
   [SerializeField] private MovingHammer movingHammer;
   [SerializeField] private MovingDrill _movingDrill;
   private Fence _currentFence;
   private Screw _screw;
   [SerializeField] private Transform hammerPosition;
   [SerializeField] private Transform drillPosition;
   private bool isMoved = false;
   private void OnValidate()
   {
      if(_camera==null)
         _camera=Camera.main;
   }

   private void Update()
   {
      Vector2 mouseScreenPosition = Input.mousePosition;
      Ray ray = _camera.ScreenPointToRay(mouseScreenPosition);
      RaycastHit raycastHit;
      
      if (Input.GetMouseButton(0)&&!isMoved)
      {
         Debug.DrawRay(ray.origin, ray.direction * 20f, Color.green);
         if (Physics.Raycast(ray, out raycastHit,20f))
         {
            _currentFence = raycastHit.collider.gameObject.GetComponent<Fence>();
            _screw = raycastHit.collider.gameObject.GetComponent<Screw>();

            if (_currentFence)
            {
               _movingDrill.transform.SetParent(null);
               _movingDrill.transform.position =_camera.transform.position+ new Vector3(2, -2f, 6);
               _movingDrill.transform.rotation = _camera.transform.rotation;
               _currentFence.fenceModelRoot.localPosition += new Vector3(0, -0.1f, 0);
               movingHammer.transform.SetParent(_currentFence.hammerPosition);
               StartCoroutine(HammerMoved());
               StartCoroutine(HammerBack());
            }

            if (_screw)
            {
               movingHammer.transform.SetParent(null);
               movingHammer.transform.position = _camera.transform.position+ new Vector3(3, -2f, 6);
               movingHammer.transform.rotation = _camera.transform.rotation;
               _movingDrill.transform.SetParent(_screw.drillPosition);
               StartCoroutine(DrillMoved());
            }

            
         }
      }
   }

   private void FixedUpdate()
   {
      if (Input.GetMouseButton(0))
      {
         if (_screw)
         {
            
            _screw.screwModelRoot.localPosition += new Vector3(0, -0.1f*Time.deltaTime, 0);
            
         }
        
            
      }
     
   }

   IEnumerator HammerMoved()
   {
      float timer = 0f;
      isMoved = true;
      while (true)
      {
         timer += Time.deltaTime;
         movingHammer.transform.localPosition = Vector3.Lerp(movingHammer.transform.localPosition,hammerPosition.localPosition , timer);
         movingHammer.transform.localRotation =
            Quaternion.Lerp(movingHammer.transform.localRotation, Quaternion.Euler(90,0,0), timer);

         if (timer >= 1)
         {
            isMoved = false;
            break;
         }
         yield return new WaitForEndOfFrame();
      }
   }

   IEnumerator HammerBack()
   {
      yield return new WaitForSeconds(1f);
      movingHammer.transform.localRotation = Quaternion.Lerp(movingHammer.transform.localRotation, Quaternion.Euler(0,0,0), 0.6f);
   }
   IEnumerator DrillMoved()
   {
      float timer = 0f;
      isMoved = true;
      while (true)
      {
         timer += Time.deltaTime;
         _movingDrill.transform.localPosition = Vector3.Lerp(_movingDrill.transform.localPosition,drillPosition.localPosition , timer);
         _movingDrill.transform.localRotation =
            Quaternion.Lerp(_movingDrill.transform.localRotation, Quaternion.Euler(90,0,0), timer);

         if (timer >= 1)
         {
            isMoved = false;
            break;
         }
         yield return new WaitForEndOfFrame();
      }
   }
}
