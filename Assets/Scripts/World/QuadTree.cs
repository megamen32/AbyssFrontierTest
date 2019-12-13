using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuadTreeIndex {
    LeftTop = 0, //00,
    RightTop = 1, //01,
    RightBottom = 2, //10,
    LeftBottom = 3, //11,
}

public class QuadTree
{
    public QuadTreeNode RootNode { get; private set; }
    public int Depth { get; private set; }

    public QuadTree(Vector3 position, Vector3 size, int depth) {
        RootNode = new QuadTreeNode(position, size);
        RootNode.Subdivide(depth);
    }

    public class QuadTreeNode {
        Vector3 position;
        Vector3 size;
        QuadTreeNode[] subNodes;
        public bool IsFree { get; set; }

        public QuadTreeNode(Vector3 pos, Vector3 _size) {
            position = pos;
            size = _size;
            IsFree = true;
        }

        public QuadTreeNode[] Nodes {
            get { return subNodes; }
        }

        public Vector3 Position {
            get { return position; }
        }

        public Vector3 Size {
            get { return size; }
        }

        public void Subdivide(int depth = 0) {
            subNodes = new QuadTreeNode[4];
            for (int i = 0; i < subNodes.Length; ++i) {
                Vector3 newPos = position;
                if ((i & 2) == 2) {
                    newPos.y += size.y * 0.25f;
                } else {
                    newPos.y -= size.y * 0.25f;
                }

                if ((i & 1) == 1) {
                    newPos.x += size.x * 0.25f;
                } else {
                    newPos.x -= size.x * 0.25f;
                }

                subNodes[i] = new QuadTreeNode(newPos, new Vector3(size.x * .5f, size.y * .5f, size.z));
                if (depth > 0) {
                    subNodes[i].Subdivide(depth - 1);
                }
            }
        }

        public bool IsLeaf() {
            return subNodes == null;
        }

        public List<QuadTreeNode> GetAllFreeNodes() {
            List<QuadTreeNode> result = new List<QuadTreeNode>();
            if (IsLeaf())
                return new List<QuadTreeNode>() { this };
            else {
                foreach (QuadTreeNode subNode in subNodes) {
                    List<QuadTreeNode> freeLeafsInSubNode = subNode.GetAllFreeNodes();
                    foreach (QuadTreeNode freeLeaf in freeLeafsInSubNode) {
                        result.Add(freeLeaf);
                    }
                }
                return result;
            }
        }
    }
}
