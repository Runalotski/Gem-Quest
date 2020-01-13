using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithTokens : MonoBehaviour
{

    GridPos selectedToken;

    public Transform Highlight;
    public Transform SelectedIndicator;

    public Transform GridRenderer;

    private void Start()
    {
        SelectedIndicator.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 MouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        int mX = (int)(MouseInWorld.x + 0.5f);
        int mY = (int)(MouseInWorld.y + 0.5f);

        //Check that the click is on the grid
        if(mouseIsInTokenGrid(mX, mY))
        {
            Highlight.GetComponent<SpriteRenderer>().enabled = true;
            Highlight.transform.position = new Vector3(mX, mY, Highlight.transform.position.z);

            if (Input.GetButtonUp("Fire1"))
            {
                //if we do not have a token selected, selected the one under the mouse position
                if (selectedToken == null)
                {
                    SetSelectedToken(new GridPos(mX, mY));
                    Debug.Log("You have selected a new Token");
                }
                //if we do already have a selected token, check if we are clicking an adjecent one
                else if (isAdjacentToSelected(mX,mY))
                {
                    Debug.Log("Swapping Selected token ");
                    SwapToken(selectedToken.x, selectedToken.y, mX, mY);



                    GridRenderer.GetComponent<TokenGridRenderer>().RenderGrid();
                    SetSelectedToken(null);
                }
            }
        }
        else
        {
            Highlight.GetComponent<SpriteRenderer>().enabled = false;
        }


        if(Input.GetButtonDown("Fire2"))
        {
            SetSelectedToken(null);
        }
    }

    void CheckForMatch()
    {

    }

    void SetSelectedToken(GridPos pos)
    {
        if(pos == null)
        {
            selectedToken = null;
            SelectedIndicator.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            selectedToken = pos;
            SelectedIndicator.transform.position = new Vector3(selectedToken.x, selectedToken.y, SelectedIndicator.position.z);
            SelectedIndicator.GetComponent<SpriteRenderer>().enabled = true;
            
        }
    }

    void SwapToken(int tk1x, int tk1y, int tk2x, int tk2y)
    {
        TokenClass tk1v = TokenGridData.Grid[tk1x, tk1y];
        TokenClass tk2v = TokenGridData.Grid[tk2x, tk2y];

        TokenGridData.Grid[tk1x, tk1y] = tk2v;
        TokenGridData.Grid[tk2x, tk2y] = tk1v;

        TokenGridData.Grid[tk1x, tk1y].updated = true;
        TokenGridData.Grid[tk2x, tk2y].updated = true;

    }

    bool isAdjacentToSelected(int mouseX, int mouseY)
    {
        if (selectedToken == null)
        {
            Debug.LogError("Tried to swap a token without having a selected token");
            return false;
        }

        //Is mouse position adajcent to the selected token to swap?
        if ((mouseX == selectedToken.x + 1 || mouseX == selectedToken.x - 1) && mouseY == selectedToken.y)
            return true;

        if ((mouseY == selectedToken.y + 1 || mouseY == selectedToken.y - 1) && mouseX == selectedToken.x)
            return true;

        //No Match found return false
        return false;

    }

    bool mouseIsInTokenGrid(int mouseX, int mouseY)
    {
        return (mouseX >= 0 && mouseY >= 0 && mouseX < TokenGridData.GRID_SIZE && mouseY < TokenGridData.GRID_SIZE);
    }
}
