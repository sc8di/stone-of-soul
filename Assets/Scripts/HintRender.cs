using System.Collections.Generic;
using UnityEngine;

public class HintRender : MonoBehaviour
{
    public MazeSpawner MazeSpawner;
    private LineRenderer componentLineRender;

    private void Start()
    {
        componentLineRender = GetComponent<LineRenderer>();
    }

    public void DrawPath()
    {
        Maze maze = MazeSpawner.maze;
        Vector2Int currentPosition = maze.finishPosition;
        List<Vector3> positions = new List<Vector3>();
        int ccc = 0;
        while (currentPosition != Vector2Int.zero && positions.Count< 10000)
        {
            Debug.Log(ccc++);

            int x = currentPosition.x;
            int y = currentPosition.y;

            positions.Add(new Vector3(x *  MazeSpawner.CellSize.x, y * MazeSpawner.CellSize.y, y * MazeSpawner.CellSize.z));

            MazeGeneratorCell currentCell = maze.cells[currentPosition.x, currentPosition.y];

            if (currentPosition.x > 0 && !currentCell.WallLeft &&
                maze.cells[currentPosition.x - 1, currentPosition.y].DistanceFromStart == currentCell.DistanceFromStart - 1)
            {
                currentPosition.x--;
            }
            else if (currentPosition.y > 0 && !currentCell.WallBottom &&
                maze.cells[currentPosition.x, currentPosition.y - 1].DistanceFromStart == currentCell.DistanceFromStart - 1)
            {
                currentPosition.y--;
            }
            else if (currentPosition.x < maze.cells.GetLength(0) - 1 && !maze.cells[currentPosition.x + 1, currentPosition.y].WallLeft && 
                maze.cells[currentPosition.x + 1, currentPosition.y].DistanceFromStart == currentCell.DistanceFromStart - 1)
            {
                currentPosition.x++;
            }
            else if (currentPosition.y < maze.cells.GetLength(1) - 1 && !maze.cells[currentPosition.x, currentPosition.y + 1].WallBottom &&
                maze.cells[currentPosition.x, currentPosition.y + 1].DistanceFromStart == currentCell.DistanceFromStart - 1)
            {
                currentPosition.y++;
            }
        }
        componentLineRender = GetComponent<LineRenderer>();
        Debug.Log(positions.Count);
        Debug.Log(componentLineRender.positionCount);


        positions.Add(Vector3.zero);
        componentLineRender.positionCount = positions.Count;
        componentLineRender.SetPositions(positions.ToArray());
    }
}
