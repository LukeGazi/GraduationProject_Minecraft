using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockTexturesManager : MonoBehaviour {

    public static BlockTexturesManager Instance;

    private void Awake() {
        Instance = this;
    }

    public Sprite[] BagBlcokImageSprites;
    public GameObject ItemImagePrefab;

    public GameObject[] BlokPrefabs;


}
