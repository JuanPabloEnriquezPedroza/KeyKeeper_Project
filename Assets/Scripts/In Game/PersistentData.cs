using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static int initialHealthPoints = 3;
    public static int healthPoints = 3;
    public static int totalKeys = 5;
    public static int obtainedKeys = 0;
    public static Vector3 intialPositionGraveyard;
    public static Vector3 intialPositionChurch;
    public static Vector3 intialPositionTower;
    public static Vector3 intialPositionCatacombs;
    public static Vector3 intialPositionDungeons;
    public static Vector3 intialViewGraveyard;
    public static Vector3 intialViewChurch;
    public static Vector3 intialViewTower;
    public static Vector3 intialViewCatacombs;
    public static Vector3 intialViewDungeons;
    public static bool isGraveyard;
    public static bool isChurch;
    public static bool isCatacombs;
    public static bool isTower;
    public static bool isDungeons;
    public static int[] keysID = new int[] { 0, 0, 0, 0, 0 };
}
