using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySlot : MonoBehaviour
{
   public Sprite availableSprite;
   public Sprite unavailableSprite;
   
   public bool isAvailable;

   public BuySystem buySystem;

   public int databaseItemID;

   private void Start()
   {
      HandleResourceChanged();
   }

   public void ClickedOnSlot()
   {
      if (isAvailable)
      {
         buySystem.placementSystem.StartPlacement(databaseItemID);
      }
   }
   

   private void UpdateAvailabilityUI()
   {
      if (isAvailable)
      {
         GetComponent<Image>().sprite = availableSprite;
         GetComponent<Button>().interactable = true;
      }
      else
      {
         GetComponent<Image>().sprite = unavailableSprite;
         GetComponent<Button>().interactable = false;
      }
   }
   
   private void OnEnable()
   {
      ResourceManager.Instance.OnResourceChanged += HandleResourceChanged;
        
   }
    
   private void OnDisable()
   {
      ResourceManager.Instance.OnResourceChanged -= HandleResourceChanged;
        
   }


  

   private void HandleResourceChanged()
   {
      ObjectData objectData = DatabaseManager.Instance.databaseSO.objectsData[databaseItemID];

      bool requirementMet = true;

      foreach (BuildRequirement req in objectData.requirements)
      {
         if (ResourceManager.Instance.GetResourceAmount(req.resource) < req.amount)
         {
            requirementMet = false;
            break;
         }
            
      }
        
      isAvailable = requirementMet;

      UpdateAvailabilityUI();



   }

}
