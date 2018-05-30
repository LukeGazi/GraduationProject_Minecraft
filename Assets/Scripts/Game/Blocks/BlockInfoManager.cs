using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfoManager : Singletons<BlockInfoManager> {

    public Block GrassBlock = new Block( 0, "Grass");
    public Block DirtBlock = new Block( 1, "Dirt");
    public Block StoneBlock = new Block( 2, "Stone");



}
