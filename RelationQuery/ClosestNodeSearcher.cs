using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationQuery
{
    internal class ClosestNodeSearcher
    {
        public ClosestNodeSearcher(Person[] pPeople)
        {
            m_People = pPeople;
        }

        public int FindMinRelationLevel(int personAidx, int personBidx)
        {
            // We may look at the problem as a quest for finding the minimum distance between two nodes
            // in a graph. Whereas a Person represents a node, and a 1-level relation represents an edge.
            // We are going to use the most common algorithm for solving the problem which is BFS (Breadth
            // First Search)
            // The complexity of the algorithm is O(V+E) in it's worst case, whereas V is the number of nodes 
            // (length of Person array), and E is the number of edges.
            // For the creation of the graph we have to options
            // 1. Create it in advance - which means to find all 1-level relations that exist. O(N^2) complexity
            // 2. Create it on the go, and at each iteration, search for the 1-level relations for a node. Every relation
            //    found will be held in a HashSet (the search if a 1-level already found, is O(1)), 
            //    so that it doesn't have to be searched twice. The worst case complexity
            //    is the same as before, but we profit, that by the time we find the closest distance, we end the loop, and 
            //    we don't have to keep creating the graph
            //
            // We are going to use the second option

            Queue<PersonDistance> queue = new Queue<PersonDistance>();
            HashSet<Person> discovered = new HashSet<Person>();

            queue.Enqueue(new PersonDistance() { PersonIdx = personAidx, Distance = 0});
            discovered.Add(m_People[personAidx]);

            while (queue.Count > 0)
            {
                PersonDistance current = queue.Dequeue();
                if (current.PersonIdx == personBidx)
                {
                    return current.Distance;
                }

                List<int> adjacentEdges = GetAdjacentEdges(current.PersonIdx);
                foreach (int idx in adjacentEdges)
                {
                    if (!discovered.Contains(m_People[idx]))
                    {
                        discovered.Add(m_People[idx]);
                        queue.Enqueue(new PersonDistance() { PersonIdx = idx, Distance = current.Distance + 1});
                    }
                }
            }

            return -1;
        }

        private class PersonDistance
        {
            public int PersonIdx { get; set; }
            public int Distance { get; set; }
        }

        private List<int> GetAdjacentEdges(int personIdx)
        {
            List<int> res = new List<int>();

            for (int i=0; i<m_People.Length; i++)
            {
                if (i == personIdx)
                    continue;

                bool found = m_EdgesFound.Contains(new Edge() { PersonAidx = personIdx, PersonBidx = i }) ||
                             m_EdgesFound.Contains(new Edge() { PersonAidx = i, PersonBidx = personIdx });

                if (found)
                {
                    res.Add(i);
                    continue;
                }

                if (m_People[personIdx].In1LevelRelationWith(m_People[i]))
                    res.Add(i);
            }

            return res;
        }

        private class Edge
        {
            public int PersonAidx { get; set; }
            public int PersonBidx { get; set; }
        }

        private Person[] m_People;
        private HashSet<Edge> m_EdgesFound = new HashSet<Edge>();
    }
}
