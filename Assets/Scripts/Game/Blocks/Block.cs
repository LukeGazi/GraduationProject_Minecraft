using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block {

    public int ID;
    public string Name;
    private Sprite BlockSprite;
    public Sprite GetSprite {
        get {
            return BlockTexturesManager.Instance.BagBlcokImageSprites[ID];
        }
    }
    private GameObject BlockPrefab;
    public GameObject GetBlockPrefab {
        get {
            return BlockTexturesManager.Instance.BlokPrefabs[ID];
        }
    }

    public Block() { }

    public Block(int _id, string _name) {
        ID = _id;
        Name = _name;
    }

}
