using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader : Singleton<ResourceLoader>
{

    private Dictionary<string, GameObject> prefabMap = new Dictionary<string, GameObject>();
    private Dictionary<string, Sprite> spriteMap = new Dictionary<string, Sprite>();

    [HideInInspector] public GameObject defaultBlockFab;
    [HideInInspector] public Sprite slotMorale;
    [HideInInspector] public Sprite slotFood;
    [HideInInspector] public Sprite slotWood;
    [HideInInspector] public Sprite slotWeapons;
    [HideInInspector] public Sprite slotTech;
    [HideInInspector] public Sprite slotRats;
    [HideInInspector] public Sprite slotStorm;
    [HideInInspector] public Sprite slotTiger;
    [HideInInspector] public Sprite slotGorilla;
    [HideInInspector] public Sprite slotStart;
    [HideInInspector] public Sprite slotGame;
    [HideInInspector] public Sprite slotVeg;

    [HideInInspector] public float tileSize;

    private Dictionary<SlotType, Sprite> spriteDict = new Dictionary<SlotType, Sprite>();

    protected override void Awake()
    {
        base.Awake();
        LoadResourceFolders();
        LoadResources();

        spriteDict.Add(SlotType.MORALE, slotMorale);
        spriteDict.Add(SlotType.FOOD, slotFood);
        spriteDict.Add(SlotType.WOOD, slotWood);
        spriteDict.Add(SlotType.WEAPONS, slotWeapons);
        spriteDict.Add(SlotType.TECH, slotTech);
        spriteDict.Add(SlotType.RATS, slotRats);
        spriteDict.Add(SlotType.STORM, slotStorm);
        spriteDict.Add(SlotType.TIGER, slotTiger);
        spriteDict.Add(SlotType.GORILLA, slotGorilla);
        spriteDict.Add(SlotType.START, slotStart);
        spriteDict.Add(SlotType.GAME, slotGame);
        spriteDict.Add(SlotType.VEG, slotVeg);

        tileSize = slotMorale.bounds.size.x;
    }

    private void LoadResourceFolders()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs/ResourceLoader");
        Sprite[] sprites = Resources.LoadAll<Sprite>("Textures/ResourceLoader");

        foreach (GameObject prefab in prefabs)
            prefabMap.Add(prefab.name, prefab);
        foreach (Sprite sprite in sprites)
            spriteMap.Add(sprite.name, sprite);
    }

    private void LoadResources()
    {
        slotMorale = Resources.Load<Sprite>("Textures/happyface");
        slotFood = Resources.Load<Sprite>("Textures/fish");
        slotWood = Resources.Load<Sprite>("Textures/log");
        slotWeapons = Resources.Load<Sprite>("Textures/spear");
        slotTech = Resources.Load<Sprite>("Textures/bulb");
        slotRats = Resources.Load<Sprite>("Textures/rat");
        slotStorm = Resources.Load<Sprite>("Textures/storm");
        slotTiger = Resources.Load<Sprite>("Textures/tiger");
        slotGorilla = Resources.Load<Sprite>("Textures/gorilla");
        slotStart = Resources.Load<Sprite>("Textures/start");
        slotGame = Resources.Load<Sprite>("Textures/game");
        slotVeg = Resources.Load<Sprite>("Textures/vegetation");
    }

    public GameObject GetPrefab(string prefabName)
    {
        return prefabMap[prefabName];
    }

    public Sprite GetSprite(string spriteName)
    {
        return spriteMap[spriteName];
    }

    public Sprite GetSpriteForType(SlotType type)
    {
        return spriteDict[type];
    }
}
