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
        if (!TokenGridManager.animating)
            PlayerInput();
    }

    void PlayerInput()
    {
        Vector3 MouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //offset by half token as tokens grid is on token center
        int mX = (int)(MouseInWorld.x + 0.5f);
        int mY = (int)(MouseInWorld.y + 0.5f);

        //Check that the click is on the grid
        if (mouseIsInTokenGrid(mX, mY))
        {
            Highlight.GetComponent<SpriteRenderer>().enabled = true;
            Highlight.transform.position = new Vector3(mX, mY, Highlight.transform.position.z);

            if (Input.GetButtonUp("Fire1"))
            {
                //if we do not have a token selected, selected the one under the mouse position
                if (selectedToken == null && TokenGridData.IsValidToken(new GridPos(mX, mY)))
                {
                    SetSelectedToken(new GridPos(mX, mY));
                }
                //if we do already have a selected token, check if we are clicking an adjecent one
                else if (TokenGridData.TokensAreValidForSwap(selectedToken, new GridPos(mX, mY)))
                {
                    TokenGridData.SwapToken(new GridPos(selectedToken.x, selectedToken.y), new GridPos(mX, mY));

                    //GridRenderer.GetComponent<TokenGridRenderer>().RenderGrid();
                    SetSelectedToken(null);
                }
            }
        }
        else
        {
            Highlight.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            SetSelectedToken(null);
        }
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

   

    bool mouseIsInTokenGrid(int mouseX, int mouseY)
    {
        return (mouseX >= 0 && mouseY >= 0 && mouseX < TokenGridData.GRID_SIZE && mouseY < TokenGridData.GRID_SIZE);
    }
}
