using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
  
    public static ResourceManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        resources.Add(ResourcesType.Credits, 300);
        resources.Add(ResourcesType.Wood, 150);
        resources.Add(ResourcesType.Stone, 100);
        resources.Add(ResourcesType.Gold, 50);
        resources.Add(ResourcesType.Food, 200);
        
    }

    //public int credits = 300;
    public Dictionary<ResourcesType, int> resources = new Dictionary<ResourcesType, int>();
    
    public event Action OnResourceChanged;
    public event Action OnBuildingsChanged;

    public TextMeshProUGUI creditsUI;
    public TextMeshProUGUI woodUI;
    public TextMeshProUGUI stoneUI;
    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI foodUI;


    public List<BuildingType> allExistingBuildings;
    
    
    public enum ResourcesType
    {
        Credits,
        Wood,
        Stone,
        Food,
        Gold
    }

    private void Start()
    {
        
        UpdateUI();
    }

    public void UpdateBuildingChanged(BuildingType buildingType, bool isNew)
    {
        if (isNew)
        {
            allExistingBuildings.Add(buildingType);
        }
        else
        {
            allExistingBuildings.Remove(buildingType);
        }
        OnBuildingsChanged?.Invoke();
    }
    
    
    
    
    
    public void IncreaseResource(ResourcesType resource, int amountToIncrease)
    {
        if (resources.ContainsKey(resource))
        {
            resources[resource] += amountToIncrease;
            OnResourceChanged?.Invoke(); // UI更新のイベントを発行
        }
        
    }
    
    
    public void DecreaseResource(ResourcesType resource, int amountToDecrease)
    {
        if (resources.ContainsKey(resource) && resources[resource] >= amountToDecrease)
        {
            resources[resource] -= amountToDecrease;
            OnResourceChanged?.Invoke(); // UI更新のイベントを発行
        }
    }


    

    private void UpdateUI()
    {
        if(creditsUI != null) creditsUI.text = $"Credits: {resources[ResourcesType.Credits]}";
        if(woodUI != null) woodUI.text = $"Woods: {resources[ResourcesType.Wood]}";
        if(stoneUI != null) stoneUI.text = $"Stone: {resources[ResourcesType.Stone]}";
        if(goldUI != null) goldUI.text = $"Gold: {resources[ResourcesType.Gold]}";
        if(foodUI != null) foodUI.text = $"Food: {resources[ResourcesType.Food]}";
       
    }
    
    
    public int GetCredits()
    {
        return resources[ResourcesType.Credits];
    }

    
    
    
    internal int GetResourceAmount(ResourcesType resource)
    {
        if (resources.ContainsKey(resource))
        {
            return resources[resource];
        }
        
        // 存在しない場合は0を返す
        return 0;
    }

    internal void DecreaseRemoveResourcesBasedOnRequirement(ObjectData objectData)
    {
        foreach (BuildRequirement req in objectData.resourceRequirements)
        {
            DecreaseResource(req.resource, req.amount);
        }
    }

    private void OnEnable()
    {
        OnResourceChanged += UpdateUI;
    }
    private void OnDisable()
    {
        OnResourceChanged -= UpdateUI;
    }
    
    
    
   // public void AddCredits(int amount)
   //{
   //   credits += amount;
   //  // 将来的に、クレジットが増えた時のUIアニメーションや効果音などをここに追加できる
   // Debug.Log($"{amount} クレジット獲得！ 現在のクレジット: {credits}");
   // }
    
}
