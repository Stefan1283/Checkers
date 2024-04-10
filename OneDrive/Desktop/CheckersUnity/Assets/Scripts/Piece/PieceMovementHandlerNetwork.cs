using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovementHandlerNetwork : PieceMovementHandler
{
    public override void OnStartAuthority()
    {
        TilesSelectionHandler.OnTileSelected += HandleTileSelected;
    }

    public override void OnStopClient()
    {
        TilesSelectionHandler.OnTileSelected -= HandleTileSelected;
    }

    protected override void Move(Vector3 position, bool nextTurn)
    {
        CMDMove(position, nextTurn);
    }

    [Command]
    void CMDMove(Vector3 position, bool nextTurn)
    {
        base.Move(position, nextTurn);
    }

    protected override void Capture(Vector2Int piecePosition)
    {
        CMDCapture(piecePosition);
    }

    [Command]
    void CMDCapture(Vector2Int piecePosition)
    {
        base.Capture(piecePosition);
    }
}
