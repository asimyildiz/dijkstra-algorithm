using System;
using System.Linq;

namespace dijkstra
{
    /** 
     * <summary>
     * Dijkstra's Algorihtm
     * </summary>
     * <example>
     * About Dijkstra's algorithm
     * 
     * When Dijkstra's algorithm first starts, it assigns the value infinite (INF) to all elements of the distance array.
     * Dijkstra algorithm starts working by assigning the distance of the selected node as the starting node to itself as 0 in this distance array.
     * Visits the neighboring nodes in order (starting with the smallest distance) that can be reached at once from the starting node,
     *  for each node it visits, writes the shortest distance of each node from the parent node to the one-dimensional distance array it keeps,
     *  keeps that information in another array when there is no other node to visit from a node, and
     *  then moves to the smallest distance node in the distance array.
     *
     * The same operations above are applied this time for this node (the distances of the neighboring nodes to this node are calculated from the main node),
     * if there is a shorter path to a node in the array through that node, then the previously found value for that node in the distance array is replaced with this smaller value.
     *
     * These operations continue until there is no other node to visit from all nodes.
     *
     * When the algorithm is finished, the distances of the shortest path from the main node to that node are found in the distance array.
     * </example>
     */
    public class Dijkstra
    {        
        /// <summary>infinity value</summary>
        public static int INF = 9999;

        private readonly int[] _distance;
        private readonly bool[] _allNeighboursVisited;
        private readonly int _verticesCount;

        /**
         * <summary>
         * Greedy Dijkstra's algorithm implementation to find the shortest distance from a starting point on a graph represented by a 2d matrix         
         * </summary>
         * <param name="verticesCount">number of vertices</param>
         */
        public Dijkstra(int verticesCount)
        {
            _verticesCount = verticesCount;

            _distance = Enumerable.Repeat(INF, verticesCount).ToArray();
            _allNeighboursVisited = new bool[verticesCount];
        }

        /**
         * <summary>
         * Find the minimum index of the closest node to the main node
         * </summary>
         * <returns>min index of the closest node to the main node</returns>
         */
        private int MinimumDistance()
        {
            int min = INF;
            int minIndex = 0;

            for (int i = 0; i < _verticesCount; ++i)
            {
                if (_allNeighboursVisited[i] == false && _distance[i] <= min)
                {
                    min = _distance[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }

        /**
         * <summary>
         * Calculates the minimun distances from a graph represented by a 2d matrix using the source parameter as the starting node
         * </summary>
         * <example>         
         * The shortest distance of all nodes to the main node, starting from the desired node, setting the distance to itself 0 in the distance array for this selected node,
         *  works for all nodes by calculating the shortest distance of all nodes from the parent node until there are no unvisited neighbor nodes of a node.
         * 
         * When moving from one node to another node, always the node with the shortest distance from the parent node in the distance sequence, which has not visited all its neighbors, is selected.
         * These operations continue until there are no unvisited nodes.
         *
         * For example, let's look at the proximity matrix below
         *    D1  D2  D3  D4  D5  D6  D7  D8
         * D1  0   3   7   8   0   0   0   0
         * D2  3   0   2   0   5   4   0   0
         * D3  0   2   0   0   0   0   1   0
         * D4  8   0   0   0   0   0   2   0
         * D5  0   0   0   0   0   0   0   6
         * D6  0   0   0   0   0   0   0   3
         * D7  0   0   1   0   0   0   0   4
         * D8  0   0   0   0   6   3   4   0
         * 
         * First we create a distance array for the solution. [INF, INF, INF, INF, INF, INF, INF, INF]
         * We also keep another boolean array [false, false, false, false, false, false, false, false] to keep track of whether we have visited all nodes adjacent to a node.
         *
         * Let's take D1 as the starting point. We set the distance for this point in the distance array as 0 (distance to itself 0) [0, INF, INF, INF, INF, INF, INF, INF]
         * Then we start the process by selecting the node that has the shortest distance to the main node we have chosen and has not visited all its neighbor nodes yet (it will be itself for the first time!!)
         * Since we will visit all nodes of this node first in the sequence, we mark it in the truth array (to avoid falling into an infinite loop) [true, false, false, false, false, false, false, false]
         * For all the nodes that were not visited afterwards (on the graph), if they have a distance to the node we selected (that is, if it is adjacent to this node), D2, D3, and D4 for D1,
         *  in the distance array we calculate and write the distances from the parent node.
         *
         * For example, since D1 is 3km away from D2 and D1 is the main node and its distance from the main node (to itself) is 0,
         * 0 + 3 &lt; INF and 3 will be written in the corresponding place in the distance array [0, 3, INF, INF, INF, INF, INF, INF]
         * Since then D1 is 7km away from D3 and D1 is the main node and its distance from the main node (to itself) is 0,
         * 0 + 7 will be &lt; INF and 7 will be written in the corresponding place in the distance array [0, 3, 7, INF, INF, INF, INF, INF]
         * Since then D1 is 8km away from D4 and D1 is the main node and its distance from the main node (to itself) is 0,
         * 0 + 8 &lt; INF and 8 will be written in the corresponding place in the distance array [0, 3, 7, 8, INF, INF, INF, INF]
         * 
         * Then we start the process by selecting the node that has the shortest distance to the main node and has not visited all its neighboring nodes yet.
         * [0, 3, 7, 8, INF, INF, INF, INF], [true, false, false, false, false, false, false, false] ie "3" ie D2
         *
         * Before starting the operations, we also mark in the truth array that we have visited all neighboring nodes for this node [true, true, false, false, false, false, false, false]
         * For all unvisited nodes (on the graph) if they have a distance to this node (D2) (ie neighboring to this node) D3, D5 and D6 for D2
         * This time we start doing the same calculations for these nodes.
         * 
         * For example, since D2 is 2km from D3, and D2 is 3km from the home node as the shortest distance
         * 3 + 2 &lt; 7 (distance of D3 from the main node, we found in the previous step) will be true and 5 will be written to the corresponding place from the distance index [0, 3, 5, 8, INF, INF, INF, INF]
         * Since then D2 is 5km from D5 and the shortest distance of D2 from the main node is 3km
         * 3 + 5 will be &lt; INF and 8 will be written in the corresponding place in the distance array [0, 3, 5, 8, 8, INF, INF, INF]
         * Since then D2 is 4km from D6 and the shortest distance of D2 from the main node is 3km
         * 3 + 4 will be &lt; INF and 7 will be written in the corresponding place in the distance array [0, 3, 5, 8, 8, 7, INF, INF]
         * 
         * In this way, the algorithm continues to work in the same way until the entire boolean visited array [true, true, true, true, true, true, true, true] returns true.
         * After the algorithm is completed, all nodes in the distance array have the shortest distance values from the main node. [0, 3, 5, 8, 8, 7, 6, 10]
         * 
         * If a parent node information is kept for the shortest distance of all nodes from the main node in a sequence, [-, D1, D2, D1, D2, D2, D3, D7]
         * The route to be followed for the shortest route from the end point to the main point can also be given as a result. D8->D7->D3->D2->D1
         * 
         * This algorithm includes a greedy solution.
         * It takes O(n) time to find the smallest unvisited node.
         * It takes O(n) time for the selected node to visit all neighboring nodes.
         * O(n * (O(n)+O(n)) = O(2n^2) => O(n^2) as this algorithm will run until all nodes are visited
         * The runtime of this greedy algorithm is O(n^2).
         * 
         * The runtime of the solution using (Binary Heap) excluding Greedy solution is O(Elogn) (E corresponds to the number of connections between nodes)
         * 
         * The memory cost of the solution offered by this class (as two arrays will use O(n) space) is O(2n) => O(n)
         *
         * </example>
         * <param name="graph">representation of the graph as a 2d multi-dimensional array</param>
         * <param name="source">starting node to start calculating distances from</param>
         * <returns>returns calculated minimum distances from starting node to the end node</returns>
         */
        public int[] CalculateDistance(int[,] graph, int source)
        {
            _distance[source] = 0;

            for (int i = 0; i < _verticesCount - 1; ++i)
            {
                int minIndex = MinimumDistance();
                _allNeighboursVisited[minIndex] = true;

                for (int j = 0; j < _verticesCount; ++j)
                {
                    if (!_allNeighboursVisited[j] &&
                        Convert.ToBoolean(graph[minIndex, j]) &&
                        _distance[minIndex] != INF &&
                        _distance[minIndex] + graph[minIndex, j] < _distance[j])
                    {
                        _distance[j] = _distance[minIndex] + graph[minIndex, j];
                    }
                }

            }

            return _distance;
        }

        /**
         * <summary>
         * Prints out the current calculated distance matrix
         * </summary>         
         */
        public void Print()
        {
            for (int i = 0; i < _verticesCount; ++i)
            {
                Console.WriteLine("{0}\t  {1}", i, _distance[i]);
            }
            Console.WriteLine();
        }
    }
}