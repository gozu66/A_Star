using UnityEngine;
using System.Collections;

public class MeshGenerator : MonoBehaviour
{
    public SquareGrid squareGrid;

    public void GenereateMesh(int[,] map, float squareSize)
    {
        squareGrid = new SquareGrid(map, squareSize);
    }

    void OnDrawGizmos()
    {
        if (squareGrid != null)
        {
            for (int x = 0; x < squareGrid.squares.GetLength(0); x++)
            {
                for (int y = 0; y < squareGrid.squares.GetLength(1); y++)
                {
                    Gizmos.color = (squareGrid.squares[x, y].topLeft.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].topLeft.position, Vector3.one * 0.5f);

                    Gizmos.color = (squareGrid.squares[x, y].topRight.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].topRight.position, Vector3.one * 0.5f);

                    Gizmos.color = (squareGrid.squares[x, y].bottomLeft.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].bottomLeft.position, Vector3.one * 0.5f);

                    Gizmos.color = (squareGrid.squares[x, y].bottomRight.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].bottomRight.position, Vector3.one * 0.5f);

                    Gizmos.color = Color.gray;
                    Gizmos.DrawCube(squareGrid.squares[x, y].centerBottom.position, Vector3.one * 0.15f);
                    Gizmos.DrawCube(squareGrid.squares[x, y].centerLeft.position, Vector3.one * 0.15f);
                    Gizmos.DrawCube(squareGrid.squares[x, y].centerTop.position, Vector3.one * 0.15f);
                    Gizmos.DrawCube(squareGrid.squares[x, y].centerRight.position, Vector3.one * 0.15f);
                }
            }
        }
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class SquareGrid
    {
        public Square[,] squares;

        public SquareGrid(int[,] map, float squareSize)
        {
            int nodeCountX = map.GetLength(0);
            int nodeCountY = map.GetLength(1);

            float mapWidth = nodeCountX * squareSize;
            float mapHeight = nodeCountY * squareSize;

            ControlSQNode[,] controlNodes = new ControlSQNode[nodeCountX, nodeCountY];

            for(int x = 0; x < nodeCountX; x++)
            {
                for(int y = 0; y < nodeCountY; y++)
                {
                    Vector3 pos = new Vector3(-mapWidth / 2 + x * squareSize + squareSize / 2, 0, -mapHeight / 2 + y * squareSize + squareSize / 2);
                    controlNodes[x, y] = new ControlSQNode(pos, map[x, y] == 1, squareSize);
                }
            }

            squares = new Square[nodeCountX - 1, nodeCountY - 1];

            for (int x = 0; x < nodeCountX - 1; x++)
            {
                for (int y = 0; y < nodeCountY - 1; y++)
                {
                    squares[x, y] = new Square(controlNodes[x + 1, y + 1], controlNodes[x, y + 1], controlNodes[x + 1, y], controlNodes[x, y]);
                }
            }
        }
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Square
    {
        public ControlSQNode topRight, topLeft, bottomRight, bottomLeft;
        public SQNode centerTop, centerBottom, centerLeft, centerRight;

        public Square(ControlSQNode _topRight, ControlSQNode _topLeft, ControlSQNode _bottomRight, ControlSQNode _bottomLeft)
        {
            topRight = _topRight;
            topLeft = _topLeft;
            bottomLeft = _bottomLeft;
            bottomRight = _bottomRight;

            centerTop = topLeft.right;
            centerBottom = _bottomLeft.right;
            centerLeft = bottomLeft.above;
            centerRight = bottomRight.above;
        }
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class SQNode
    {
        public Vector3 position;
        public int vertexIndex = -1;

        public SQNode(Vector3 _pos)
        {
            position = _pos;
        }
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public class ControlSQNode : SQNode
    {
        public bool active;
        public SQNode above, right;

        public ControlSQNode(Vector3 _pos, bool _active, float squaresize) : base(_pos)
        {
            active = _active;
            above = new SQNode(position + Vector3.forward * squaresize / 2f);
            right = new SQNode(position + Vector3.right * squaresize / 2f);
        }
    }
}