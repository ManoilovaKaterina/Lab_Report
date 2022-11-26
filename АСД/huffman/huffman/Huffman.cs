using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace huffman
{
    // вузол для побудови дерева кодування
    public class Node
    {
        public char Symb { get; set; }
        public int Freq { get; set; }
        public Node Right { get; set; }
        public Node Left { get; set; }

        public List<bool> Trav(char symb, List<bool> list)
        {
            if (Right == null && Left == null)
            {
                if (symb == Symb)
                {
                    return list;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                List<bool> left = null;
                List<bool> right = null;

                if (Left != null)
                {
                    List<bool> leftP = new List<bool>();
                    leftP.AddRange(list);
                    leftP.Add(false);

                    left = Left.Trav(symb, leftP);
                }

                if (Right != null)
                {
                    List<bool> rightP = new List<bool>();
                    rightP.AddRange(list);
                    rightP.Add(true);
                    right = Right.Trav(symb, rightP);
                }

                if (left == null)
                {
                    return right;
                }
                else
                {
                    return left;
                }
            }
        }
    }

    // дерево
    public class HuffmanTree
    {
        private List<Node> nodes = new List<Node>();
        public Node Root { get; set; }
        public Dictionary<char, int> Freqs = new Dictionary<char, int>();

        public void Build(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!Freqs.ContainsKey(str[i]))
                {
                    Freqs.Add(str[i], 0);
                }

                Freqs[str[i]]++;
            }

            foreach (KeyValuePair<char, int> symb in Freqs)
            {
                nodes.Add(new Node() { Symb = symb.Key, Freq = symb.Value });
            }

            while (nodes.Count > 1)
            {
                List<Node> sortNodes = nodes.OrderBy(node => node.Freq).ToList<Node>();

                if (sortNodes.Count >= 2)
                {
                    List<Node> taken = sortNodes.Take(2).ToList<Node>();

                    Node parent = new Node()
                    {
                        Symb = '*',
                        Freq = taken[0].Freq + taken[1].Freq,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                this.Root = nodes.FirstOrDefault();

            }

        }

        public BitArray Code(string source)
        {
            List<bool> encodedSource = new List<bool>();

            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = this.Root.Trav(source[i], new List<bool>());
                encodedSource.AddRange(encodedSymbol);
            }

            BitArray bits = new BitArray(encodedSource.ToArray());

            return bits;
        }

        public string Decode(BitArray bits)
        {
            Node current = this.Root;
            string decoded = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                if (IsLeaf(current))
                {
                    decoded += current.Symb;
                    current = this.Root;
                }
            }

            return decoded;
        }

        public bool IsLeaf(Node node)
        {
            return (node.Left == null && node.Right == null);
        }

    }
}
