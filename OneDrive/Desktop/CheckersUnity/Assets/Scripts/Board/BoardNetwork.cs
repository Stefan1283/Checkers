using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardNetwork : Board
{
    readonly SyncList<int[]> boardList = new SyncList<int[]>();
    public override IList<int[]> BoardList
    {
        get { return boardList; }
    }
    public override event Action<Vector3> OnPieceCaptured;

    public override void OnStartServer()
    {
        FillBoardList(boardList);
        Board.Instance.OnPieceCaptured += ServerHandlePieceCaptured;
    }

    public override void OnStopServer()
    {
        Board.Instance.OnPieceCaptured -= ServerHandlePieceCaptured;
    }

    [Server]
    public override void MoveOnBoard(Vector2Int oldPosition, Vector2Int newPosition, bool nextTurn)
    {
        base.MoveOnBoard(boardList, oldPosition, newPosition);

    }
    [ClientRpc]
    void RPCMoveOnBoard(Vector2Int oldPosition, Vector2Int newPosition, bool nextTurn)
    {
        if (NetworkServer.active)
        {
            return;
        }
        MoveOnBoard(boardList, oldPosition, newPosition);
        if (nextTurn)
        {
            NetworkClient.connection.identity.GetComponent<PlayerNetwork>().CMDNextTurn();
        }
    }
    [Server]
    public override void CaptureOnBoard(Vector2Int piecePosition)
    {
        Capture(boardList, piecePosition);
        RPCCaptureOnBoard(piecePosition);
        OnPieceCaptured?.Invoke(new Vector3(piecePosition.x, 0, piecePosition.y));
    }

    [ClientRpc]
    void RPCCaptureOnBoard(Vector2Int piecePosition)
    {
        Capture(boardList, piecePosition);
    }

    [Server]
    void ServerHandlePieceCaptured(Vector3 capturedPiecePosition)
    {
        if (capturedPiecePosition != transform.position) return;
        NetworkServer.Destroy(gameObject);
    }

    [Server]
    bool TryPromotePieceOnBoard(PiecePromotionHandler promotedPiece, int x, int z)
    {
        return true;
    }
}
