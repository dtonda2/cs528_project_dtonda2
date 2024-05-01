using UnityEngine;
using System.Collections.Generic;
public class StarVisiualizer : MonoBehaviour
{
    public GameObject exo_State;
    public float x_exp = 40f;
    public float y_exp = 40f;
    public float z_exp = 40f;
    private Vector3 cubeSize;
    // Adjust the radius as needed
    public GameObject cubeCenter;
    private Collider[] visible_Stars;
    public GameObject csv_Loader;
    private Vector3Int currentOctreeblockPosition;
    CSV_Loader csvLoader;
    void Start()
    {
        csvLoader  = csv_Loader.GetComponent<CSV_Loader>();
        currentOctreeblockPosition = new Vector3Int(-1, -1, -1);
        cubeSize = new Vector3(x_exp, y_exp, z_exp);
        InvokeRepeating("CheckOctree", 1f, 1f);
        visible_Stars = new Collider[0];
    }
    void CheckOctree()
    {
        Vector3 headPos = cubeCenter.transform.position; //get pos of the head
        
        // Calculate the block pos containing ship
        Vector3Int head_blockPosition = new Vector3Int(
            Mathf.FloorToInt(headPos.x / 20) * 20,
            Mathf.FloorToInt(headPos.y / 20) * 20,
            Mathf.FloorToInt(headPos.z / 20) * 20
        );
         
        if (csvLoader != null)
        {
            if (head_blockPosition != currentOctreeblockPosition)
            {
                DeactivateStarsInBlock(currentOctreeblockPosition, csvLoader);
                ActivateStarsInBlock(head_blockPosition, csvLoader);
                currentOctreeblockPosition = head_blockPosition;
            }
        }
    }

    void DeactivateStarsInBlock(Vector3Int blockPosition, CSV_Loader csvLoader)
    {
        
        if (csvLoader.octree_Blocks.ContainsKey(blockPosition))
        {
            OctreeBlock block = csvLoader.octree_Blocks[blockPosition];
            foreach (GameObject star in block.starsBlocks)
            {
                star.SetActive(false);
            }

            // Deactivate stars in adjacent blocks
            foreach (Vector3Int adjacentOffset in Getadjacent_BlockPositions(blockPosition))
            {
                Vector3Int adjacentPosition = adjacentOffset;
                
                if (csvLoader.octree_Blocks.ContainsKey(adjacentPosition))
                {
                    OctreeBlock adjacent_Block = csvLoader.octree_Blocks[adjacentPosition];
                    foreach (GameObject star in adjacent_Block.starsBlocks)
                    {
                        star.SetActive(false);
                    }
                }
            }
        }
    }

void ActivateStarsInBlock(Vector3Int blockPosition, CSV_Loader csvLoader)
    {
        if (csvLoader.octree_Blocks.ContainsKey(blockPosition))
        {
            OctreeBlock block = csvLoader.octree_Blocks[blockPosition];
            foreach (GameObject star in block.starsBlocks)
            {
                star.SetActive(true);
            }

            // Activate stars in adjacent blocks
            foreach (Vector3Int adjacentOffset in Getadjacent_BlockPositions(blockPosition))
            {
                Vector3Int adjacentPosition =  adjacentOffset;
                if (csvLoader.octree_Blocks.ContainsKey(adjacentPosition))
                {
                    OctreeBlock adjacent_Block = csvLoader.octree_Blocks[adjacentPosition];
                    foreach (GameObject star in adjacent_Block.starsBlocks)
                    {
                        star.SetActive(true);
                    }
                }
            }

            ColorManager ColorManagerScript = exo_State.GetComponent<ColorManager>();
            ColorManagerScript.update_exo();
        }
    }


    void CheckStarVisibility()
    {

        // Get all colliders within the detection sphere
        Collider[] starsInBox= Physics.OverlapBox(cubeCenter.transform.position, cubeSize, cubeCenter.transform.rotation);
    
        // Make previously visible stars invisible
        foreach (Collider starCollider in visible_Stars)
        {
            starCollider.gameObject.SetActive(false);
        }

        // Update the list of currently visible stars
        visible_Stars = starsInBox;
        // Iterate through the colliders and set their renderers to visible
        foreach (Collider starCollider in starsInBox)
        {
            starCollider.gameObject.SetActive(true);
        }
    }


    private List<Vector3Int> Getadjacent_BlockPositions(Vector3Int blockPosition)
    {
        List<Vector3Int> adjacentPositions = new List<Vector3Int>();

        // Define the offsets for adjacent blocks in each dimension (x, y, z)
        int offset = 20;
        
        Vector3Int[] offsets = new Vector3Int[]
        {
            new Vector3Int(-offset, 0, 0),             // Left adjacent cube
            new Vector3Int(offset, 0, 0),              // Right adjacent cube
            new Vector3Int(0, -offset, 0),             // Bottom adjacent cube
            new Vector3Int(0, offset, 0),              // Top adjacent cube
            new Vector3Int(0, 0, -offset),             // Back adjacent cube
            new Vector3Int(0, 0, offset),              // Front adjacent cube
            new Vector3Int(-offset, 0, -offset),       // Front-left adjacent cube
            new Vector3Int(offset, 0, -offset),        // Front-right adjacent cube
            new Vector3Int(-offset, 0, offset),        // Back-left adjacent cube
            new Vector3Int(offset, 0, offset),         // Back-right adjacent cube
            new Vector3Int(-offset, offset, -offset),  // Upper-left-front adjacent cube
            new Vector3Int(offset, offset, -offset),   // Upper-right-front adjacent cube
            new Vector3Int(-offset, offset, offset),   // Upper-left-back adjacent cube
            new Vector3Int(offset, offset, offset),    // Upper-right-back adjacent cube
            new Vector3Int(-offset, -offset, -offset), // Lower-left-front adjacent cube
            new Vector3Int(offset, -offset, -offset),  // Lower-right-front adjacent cube
            new Vector3Int(-offset, -offset, offset),  // Lower-left-back adjacent cube
            new Vector3Int(offset, -offset, offset),   // Lower-right-back adjacent cube
            new Vector3Int(0, offset, -offset),        // Top-left-back adjacent cube
            new Vector3Int(0, offset, offset),         // Top-right-back adjacent cube
            new Vector3Int(0, -offset, -offset),       // Bottom-left-back adjacent cube
            new Vector3Int(0, -offset, offset),        // Bottom-right-back adjacent cube
            new Vector3Int(-offset, offset, 0),        // Top-front-left adjacent cube
            new Vector3Int(offset, offset, 0),         // Top-front-right adjacent cube
            new Vector3Int(-offset, -offset, 0),       // Bottom-front-left adjacent cube
            new Vector3Int(offset, -offset, 0)         // Bottom-front-right adjacent cube
        };
        // Calculate the positions of adjacent blocks relative to the current block position
        foreach (Vector3Int adjacentOffset in offsets)
        {
            Vector3Int adjacentPosition = blockPosition + adjacentOffset;
            adjacentPositions.Add(adjacentPosition);
        }

        return adjacentPositions;
    }
}


