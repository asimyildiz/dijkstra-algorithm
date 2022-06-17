using dijkstra;
using NUnit.Framework;

namespace dijkstra_tests
{
    public class Tests
    {
        int[,] graph;
        Dijkstra dijkstra;

        [SetUp]
        public void Setup()
        {
            graph = new int[8, 8]
                {
                { 0, 3, 7, 8, 0, 0, 0, 0 },
                { 3, 0, 2, 0, 5, 4, 0, 0 },
                { 0, 2, 0, 0, 0, 0, 1, 0 },
                { 8, 0, 0, 0, 0, 0, 2, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 6 },
                { 0, 0, 0, 0, 0, 0, 0, 3 },
                { 0, 0, 1, 0, 0, 0, 0, 4 },
                { 0, 0, 0, 0, 6, 3, 4, 0 }
            };

            dijkstra = new Dijkstra(8);
        }

        [Test]
        public void TestShortestPathFromPoint0()
        {
            int[] correctResultForCase = new int[] { 0, 3, 5, 8, 8, 7, 6, 10 };
            int[] shortestPath = dijkstra.CalculateDistance(graph, 0);
            Assert.AreEqual(shortestPath, correctResultForCase);
        }

        [Test]
        public void TestShortestPathFromPoint1()
        {
            int[] correctResultForCase = new int[] { 3, 0, 2, 11, 5, 4, 3, 7 };
            int[] shortestPath = dijkstra.CalculateDistance(graph, 1);
            Assert.AreEqual(shortestPath, correctResultForCase);
        }
    }
}
