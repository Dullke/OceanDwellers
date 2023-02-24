using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System.Linq;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] [BoxGroup("Required Data")] private Tilemap ObstaclesMap;
    [SerializeField] int x, y;

    public Queue<Vector2> FindPath(Vector2 start, Vector2 destination)
    {
        Queue<Vector2> path = new Queue<Vector2>();

        Vector2Int roundStart = Utilities.Misc.RoundUpVector2(start);
        Vector2Int roundDestination = Utilities.Misc.RoundUpVector2(destination);

        List<Node> frontier = new List<Node>();
        Dictionary<Vector2Int, int> costSoFar = new Dictionary<Vector2Int, int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        costSoFar.Add(roundStart, 0);
        cameFrom[roundStart] = roundStart;
        frontier.Add(new Node(roundStart, 0));


        while (frontier.Count > 0)
        {
            Node current = GetSmallestHeuristic(frontier);
            frontier.Remove(current);

            if (current.position == roundDestination) break;

            foreach (Vector2Int neighbour in Neighbours(current.position))
            {
                int newCost = costSoFar[current.position] + Cost(current.position, neighbour);
                if (costSoFar.ContainsKey(neighbour) == false || newCost < costSoFar[neighbour])
                {
                    costSoFar[neighbour] = newCost;
                    cameFrom[neighbour] = current.position;
                    frontier.Add(new Node(neighbour, newCost + Heuristic(neighbour, roundDestination)));
                }
            }
        }

        Vector2Int pathTrack = roundDestination;
        while (pathTrack != roundStart)
        {
            path.Enqueue(pathTrack);
            pathTrack = cameFrom[pathTrack];
        }

        return new Queue<Vector2>(path.Reverse());

    }

    private Node GetSmallestHeuristic(List<Node> frontier)
    {
        Node result = frontier[0];
        int smallestH = result.heuristc;
        foreach (Node node in frontier)
        {
            if (node.heuristc < smallestH)
            {
                result = node;
                smallestH = node.heuristc;
            }
        }

        return result;
    }

    public int Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    public int Cost(Vector2Int a, Vector2Int b)
    {
        int distance = (int)(Vector2.Distance(a, b) * 10);

        if (ObstaclesMap.GetTile(new Vector3Int(b.x, b.y)) != null)
            return distance * 10;

        return distance;
    }

    private List<Vector2Int> Neighbours(Vector2Int current)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>(8);
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                Vector2Int neighbourPosition = new Vector2Int(current.x - x, current.y - y);
                if (current == neighbourPosition) continue;

                neighbours.Add(neighbourPosition);
            }
        }

        return neighbours;
    }


    internal struct Node
    {
        public Node(Vector2Int newPosition, int newHeuristc)
        {
            position = newPosition;
            heuristc = newHeuristc;
        }

        public Vector2Int position;
        public int heuristc;
    }

}
