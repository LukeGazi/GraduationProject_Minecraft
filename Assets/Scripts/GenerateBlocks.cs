using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBlocks : MonoBehaviour {

    public GameObject Block; //the block will be generated
    private int m_generateWidth = 100; //the width of terrian made of blocks

    // Use this for initialization
    void Start() {
        //generate 100 * 100 blocks to test
        GameObject blockSets = new GameObject("BlockSets");
        for (int row = 0; row < m_generateWidth; row++) {
            for (int cloum = 0; cloum < m_generateWidth; cloum++) {
                GameObject block = GameObject.Instantiate(Block, blockSets.transform);
                block.transform.position = new Vector3(-m_generateWidth + row, 0, -m_generateWidth + cloum);
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
