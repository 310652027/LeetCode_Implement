using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QRCodeReader.Program;

namespace HTR
{
    class _987_Vertica_OrderTraversalofaBinaryTree
    {
        public IList<IList<int>> VerticalTraversal(TreeNode root)
        {
            //Main Idea: The graphic of question presented the position of node to (r,c);
            //Remember array of each row need to order by the position , from left to right;
            //Moreover, for the nodes in same place, we need to order by their value;
            //Thus, we use a dictionary to keep the position and value of a node;
            //Next, for nodes which are in same row, collecting them to a array, if there are two nodes or more in same place, sort them;


            // To represent the row and col to single integer, we make value = row * 10000 + 1000 + col. (since 0 <= row <= 1000 & -1000 <= col <=1000)
            int encode(int row, int col) => row * 10000 + 1000 + col;
            (int row, int col) decode(int value) => (value / 10000, value % 10000 - 1000);

            //Using queue to build BFS.
            Dictionary<int, List<int>> nodeIncolumn = new Dictionary<int, List<int>>();
            int minCol = 0, maxCol = 0;
            Queue<(int, TreeNode)> q = new Queue<(int, TreeNode)>();
            q.Enqueue((encode(0, 0), root));
            while (q.Count > 0)
            {
                (int Value, TreeNode node) = q.Dequeue();
                (int rowIndex, int colIndex) = decode(Value);
                minCol = int.Min(minCol, colIndex);
                maxCol = int.Max(maxCol, colIndex);
                if (!nodeIncolumn.ContainsKey(Value)) nodeIncolumn[Value] = new List<int>();
                nodeIncolumn[Value].Add(node.val);
                if (node.left != null) q.Enqueue((encode(rowIndex + 1, colIndex - 1), node.left));
                if (node.right != null) q.Enqueue((encode(rowIndex + 1, colIndex + 1), node.right));
            }


            List<int>[] Return = new List<int>[maxCol - minCol + 1];
            int[] KeyOrder = nodeIncolumn.Keys.ToArray();
            // Sort can help us to collect the value for left to right;
            Array.Sort(KeyOrder);

            foreach (var key in KeyOrder)
            {
                (int rowIndex, int colIndex) = decode(key);
                int[] Values = nodeIncolumn[key].ToArray();

                if (Values.Length > 1) Array.Sort(Values);
                if (Return[colIndex - minCol] == null) Return[colIndex - minCol] = new List<int>();
                foreach (int value in Values) Return[colIndex - minCol].Add(value);
            }
            return Return;
        }
    }
}
