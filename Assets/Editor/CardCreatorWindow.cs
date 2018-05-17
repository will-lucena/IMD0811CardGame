using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Enums;

public class CardCreatorWindow : EditorWindow
{
    private Texture2D headerSectionTexture;
    private Texture2D infosSectionTexture;

    private Color headerSectionColor = Color.black;
    private Color infosSectionColor = Color.grey;
    private int headerHeight = 35;

    Rect header;
    Rect main;

    private static List<Sprite> spriteList;
    private int power;
    private string rootFolderName;
    public const string extension = ".asset";
    public const string pathBase = "Assets/";
    private string mainText = "Card creator";

    private Type cardType;

    [MenuItem("Window/Cards creator")]
    static void OpenWindow()
    {
        spriteList = new List<Sprite>();
        CardCreatorWindow window = GetWindow(typeof(CardCreatorWindow)) as CardCreatorWindow;
        window.minSize = new Vector2(300, 150);
        window.Show();
    }

    private void OnEnable()
    {
        initTextures();
        spriteList = new List<Sprite>();
        power = 0;
    }

    private void initTextures()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();

        infosSectionTexture = new Texture2D(1, 1);
        infosSectionTexture.SetPixel(0, 0, infosSectionColor);
        infosSectionTexture.Apply();
    }

    private void OnGUI()
    {
        drawLayouts();
        drawHeader();
        drawMain();
    }

    private void drawLayouts()
    {
        header.x = 0;
        header.y = 0;
        header.width = Screen.width;
        header.height = headerHeight;

        main.x = 0;
        main.y = headerHeight;
        main.width = Screen.width;
        main.height = Screen.height - headerHeight;

        GUI.DrawTexture(header, headerSectionTexture);
        GUI.DrawTexture(main, infosSectionTexture);
    }

    private void drawHeader()
    {
        GUILayout.BeginArea(header);

        GUILayout.Label("Card creator tool");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Root folder:");
        rootFolderName = EditorGUILayout.TextField(rootFolderName);
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    private void drawMain()
    {
        GUILayout.BeginArea(main);
        GUILayout.Label(mainText);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Card type: ");
        cardType = (Type)EditorGUILayout.EnumPopup(cardType);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Power:");
        power = EditorGUILayout.IntField(power);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Card images");
        GUILayout.BeginVertical();
        foreach (Sprite s in spriteList)
        {
            EditorGUILayout.ObjectField(s, typeof(Sprite), false);
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        switch (cardType)
        {
            case Type.HERO:
                drawHeroSettings();
                break;
            case Type.WEAPON:
                drawWeaponSettings();
                break;
            case Type.ARMOR:
                drawArmorSettings();
                break;
        }

        GUILayout.BeginHorizontal();
        Rect dropArea = GUILayoutUtility.GetRect(50f, 50f, GUILayout.ExpandHeight(true));
        GUI.Box(dropArea, "Drop images here");

        Event currentEvent = Event.current;
        switch (currentEvent.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dropArea.Contains(currentEvent.mousePosition))
                {
                    break;
                }
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (currentEvent.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    foreach (Texture2D draggedObject in DragAndDrop.objectReferences)
                    {
                        if (!draggedObject)
                        {
                            continue;
                        }
                        Sprite sprite = Resources.Load<Sprite>(draggedObject.name);
                        spriteList.Add(sprite);
                    }
                }
                Event.current.Use();
                break;
        }
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    private void drawHeroSettings()
    {
        mainText = "Hero card creator";
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create new hero"))
        {
            foreach (Sprite sprite in spriteList)
            {
                int tempPoints = power;
                int atq = Random.Range(0, tempPoints - 1);
                tempPoints -= atq;
                int def = Random.Range(0, tempPoints);
                tempPoints -= def;
                int hp = tempPoints;

                HeroData hero = CreateInstance<HeroData>();

                hero.atk = atq;
                hero.def = def;
                hero.health = hp;
                hero.image = sprite;
                string[] texts = hero.image.name.Split('_');
                hero.name = texts[1];
                hero.description = texts[0];
                hero.type = cardType;
                saveData(hero);
            }
            spriteList = new List<Sprite>();
            power = 0;
        }
        GUILayout.EndHorizontal();
    }

    private void drawWeaponSettings()
    {
        mainText = "Weapon card creator";
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create new Weapon"))
        {
            foreach (Sprite sprite in spriteList)
            {
                int tempPoints = power;
                int atq = Random.Range(0, tempPoints);
                tempPoints -= atq;
                int def = tempPoints;
                
                WeaponData weapon = CreateInstance<WeaponData>();

                weapon.atk = atq;
                weapon.def = def;
                weapon.image = sprite;
                weapon.name = weapon.image.name;
                weapon.type = cardType;
                saveData(weapon);
            }
            spriteList = new List<Sprite>();
            power = 0;
        }
        GUILayout.EndHorizontal();
    }

    private void drawArmorSettings()
    {
        mainText = "Armor card creator";
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create new Armor"))
        {
            foreach (Sprite sprite in spriteList)
            {
                int tempPoints = power;
                int atq = Random.Range(0, tempPoints);
                tempPoints -= atq;
                int def = tempPoints;

                ArmorData armor = CreateInstance<ArmorData>();

                armor.atk = atq;
                armor.def = def;
                armor.image = sprite;
                armor.name = armor.image.name;
                armor.type = cardType;
                saveData(armor);
            }
            spriteList = new List<Sprite>();
            power = 0;
        }
        GUILayout.EndHorizontal();
    }

    private void saveData(CardData data)
    {
        string path = buildPathName(rootFolderName);
        if (!AssetDatabase.IsValidFolder(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }

        string dataPath = path + data.name + extension;
        AssetDatabase.CreateAsset(data, dataPath);
    }

    private string buildPathName(string folderName)
    {
        if (folderName.Length > 0 && folderName.IndexOf("/") > 0)
        {
            folderName = folderName.Remove(folderName.IndexOf("/"));
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append(pathBase);
        sb.Append(folderName);
        sb.Append("/");

        return sb.ToString();
    }
}
