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

    public override void OnStartServer()
    {
        FillBoardList(boardList);
    }
}
