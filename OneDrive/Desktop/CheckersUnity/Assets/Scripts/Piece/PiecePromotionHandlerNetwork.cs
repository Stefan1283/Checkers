using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePromotionHandlerNetwork : PiecePromotionHandler
{
    protected override bool TryPromotePiece(PiecePromotionHandler promotedPiece, int x, int z)
    {
        if (!base.TryPromotePiece(promotedPiece, x, z)) return false;
        RPCPromotePiece();
        return true;
    }
    public override void OnStartServer()
    {
        PieceMovementHandlerNetwork.ServerOnPieceReachedBackLine += TryPromotePiece;
    }

    public override void OnStopServer()
    {
        PieceMovementHandlerNetwork.ServerOnPieceReachedBackLine -= TryPromotePiece;
    }

    [ClientRpc]
    void RPCPromotePiece()
    {
        if (NetworkServer.active) return;

        PromotePiece();
    }
}
