using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstrasAlgorithm
{
    /*
     * Shortest Path Problem using Dijkstra Algorithm
     *  Problem Url : http://www.spoj.com/problems/SHPATH/
     */
    class Program
    {
        //holds the resulting shortest path tree
        static List<List<int>> shortestPathTree = new List<List<int>>();

        //holds the edges of initial tree
        static List<List<int>> edges = new List<List<int>>();

        static void Main(string[] args)
        {
            //Reads input from file passed via command line

            using (StreamReader sr = new StreamReader(args[0]))
            {

                //Number of scenarios -test cases 
                int scenariosCount = Convert.ToInt32(sr.ReadLine());

                //node index--first city has an index  of 1,second city has 2 and so on
                int nodeNumber = 1;

                int citiesCount = 0;

                //run djiskras code for each test case
                while (scenariosCount > 0)
                {
                    ArrayList shortestPathsOutput = new ArrayList();

                    //get the number of cities/nodes
                    citiesCount = Convert.ToInt32(sr.ReadLine());

                    string[] cities = new string[citiesCount];

                    //for each city, get its neighboring nodes/cities
                    int i = 0;
                    while (i >= 0 && i < citiesCount)
                    {
                        string cityName = sr.ReadLine();
                        int neighborsCount = Convert.ToInt32(sr.ReadLine());

                        cities[i] = cityName;



                        while (neighborsCount > 0)
                        {
                            //neighboring node and tranportation cost....seperated by white space
                            string line = sr.ReadLine();
                            string[] values = line.Split(' ');

                            int neighbor = Convert.ToInt32(values[0]);
                            int cost = Convert.ToInt32(values[1]);

                            //add to edges list
                            edges.Add(new List<int> { nodeNumber, neighbor, cost });


                            neighborsCount--;
                        }

                        nodeNumber++;

                        i++;

                    }

                    //number of paths to find
                    int pathCount = Convert.ToInt32(sr.ReadLine());

                    getShortestPathTree(citiesCount);

                    //foraech path to be calculated
                    while (pathCount > 0)
                    {
                        //get the start and end nodes of the path,
                        // seperated by space
                         
                        string line = sr.ReadLine();
                        string[] values = line.Split(' ');

                        string startNode = values[0];
                        int startNodeIndex = Array.IndexOf(cities, startNode);

                        string endNode = values[1];
                        int endNodeIndex = Array.IndexOf(cities, endNode);

                        ///substract the cost of the start and end nodes..to get the shortest path cost
                        int startCost = shortestPathTree.Find(j => j[1] == startNodeIndex + 1)[2];
                        int endCost = shortestPathTree.Find(j => j[1] == endNodeIndex + 1)[2];

                        shortestPathsOutput.Add(endCost - startCost);
                        pathCount--;
                    }

                    Console.WriteLine("\nOUTPUT:");

                    if (shortestPathsOutput.Count > 0)
                    {
                        foreach (var output in shortestPathsOutput)
                        {
                            Console.WriteLine(output);
                        }

                        //clear mst for next testcase
                        shortestPathTree.Clear();
                    }

                    scenariosCount--;
                }

                

            }

            Console.ReadLine();
        }

        public static void getShortestPathTree(int endNode)
        {
            int node = 1;//starting node
            int previousWeight = 0;
            int minimum = 1; //starting node has the minimum transportation cost

            //Add the origin node to the shortest Path tree
            shortestPathTree.Add(new List<int>() { minimum, minimum, 0 });
            previousWeight = 0;
            node = minimum;

            //while endnode is not yet reached, continue to buid shortest path
            while (shortestPathTree.Any(j => j[1] == endNode) == false)
            {

                //get neighbors that are not yet included in shortest path tree
                List<List<int>> neighbours = edges.Where(x => x[0] == node && shortestPathTree.Any(j => j[1] == x[1]) == false).ToList();

                if (neighbours.Count > 0)
                {
                    //Update the wights of neighbors
                    foreach (var neighbor in neighbours)
                    {
                        neighbor[2] = neighbor[2] + previousWeight;
                    }

                    //Get the neighbor with the mimimum weight/ transportation cost
                    List<int> minimumnode = neighbours.OrderBy(x => x[2]).First();

                    //set minimum node as the next destination
                    node = minimumnode[1];

                    //add minimum node to the shortest path tree
                    shortestPathTree.Add(minimumnode);

                    previousWeight = minimumnode[2];
                }
            }

        }
    }
}
