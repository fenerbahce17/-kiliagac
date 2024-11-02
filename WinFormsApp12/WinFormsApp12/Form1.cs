using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp12
{
    public partial class Form1 : Form
    {
        private BinaryTree binaryTree = new BinaryTree();
        private int nodeCount = 0;

        public Form1()
        {
            InitializeComponent();
            button1.Click += btnEkle_Click;
            button2.Click += btnArama_Click;
            button3.Click += btnMinBul_Click;
            button4.Click += btnMaxBul_Click;
            button5.Click += btnPreOrder_Click;
            button6.Click += btnInOrder_Click;
            button7.Click += btnPostOrder_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                int value = int.Parse(textBox2.Text);
                binaryTree.Add(value);
                AddNodeToForm(value);
            }
            textBox2.Clear();
        }

        private void btnArama_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox3.Text))
            {
                int value;
                if (int.TryParse(textBox3.Text, out value))
                {
                    bool found = binaryTree.Search(value);
                    MessageBox.Show(found ? "Bulundu" : "Bulunamad�");
                }
                else
                {
                    MessageBox.Show("Ge�ersiz say� giri�i!");
                }
            }
            else
            {
                MessageBox.Show("L�tfen bir say� girin.");
            }
            textBox3.Clear();
        }

        private void btnMinBul_Click(object sender, EventArgs e)
        {
            int minValue = binaryTree.FindMin();
            MessageBox.Show("Min De�er: " + minValue);
        }

        private void btnMaxBul_Click(object sender, EventArgs e)
        {
            int maxValue = binaryTree.FindMax();
            MessageBox.Show("Max De�er: " + maxValue);
        }

        private void btnPreOrder_Click(object sender, EventArgs e)
        {
            List<int> preOrderList = binaryTree.PreOrder();
            textBox1.Text = "PreOrder: " + string.Join(", ", preOrderList);
        }

        private void btnInOrder_Click(object sender, EventArgs e)
        {
            List<int> inOrderList = binaryTree.InOrder();
            textBox1.Text = "InOrder: " + string.Join(", ", inOrderList);
        }

        private void btnPostOrder_Click(object sender, EventArgs e)
        {
            List<int> postOrderList = binaryTree.PostOrder();
            textBox1.Text = "PostOrder: " + string.Join(", ", postOrderList);
        }

        private void AddNodeToForm(int value)
        {
            Label label = new Label();
            label.Text = value.ToString();
            label.AutoSize = true;

            // D���m�n derinli�ini ve x koordinat�n� hesaplayal�m
            int depth = GetDepth(binaryTree.Root, value);
            int x = CalculateXPosition(binaryTree.Root, value, 100); // 100 piksellik yatay aral�k
            int y = depth * 50; // Her seviye 50 piksel dikey aral�k

            label.Location = new Point(x, y);
            this.Controls.Add(label); // Label'i form'a ekleyin

            nodeCount++;
        }


        // D���m�n x koordinat�n� hesaplamak i�in
        private int CalculateXPosition(Node node, int value, int xOffset)
        {
            if (node == null)
                return xOffset;

            if (value < node.Value)
                return CalculateXPosition(node.Left, value, xOffset - 50); // Sol alt d���m
            else if (value > node.Value)
                return CalculateXPosition(node.Right, value, xOffset + 50); // Sa� alt d���m
            else
                return xOffset + 720; // D���m�n kendisi
        }


        // D���m�n derinli�ini hesaplamak i�in
        private int GetDepth(Node node, int value)
        {
            if (node == null)
                return 0;

            if (value < node.Value)
                return 1 + GetDepth(node.Left, value); // Sol alt d���m
            else if (value > node.Value)
                return 1 + GetDepth(node.Right, value); // Sa� alt d���m
            else
                return 1; // D���m�n kendisi
        }




        public class Node
        {
            public int Value;
            public Node Left;
            public Node Right;

            public Node(int value)
            {
                Value = value;
                Left = null;
                Right = null;
            }
        }

        public class BinaryTree
        {
            public Node Root;

            public BinaryTree()
            {
                Root = null;
            }

            public void Add(int value)
            {
                Root = AddRecursive(Root, value);
            }

            private Node AddRecursive(Node node, int value)
            {
                if (node == null)
                {
                    return new Node(value);
                }

                if (value < node.Value)
                {
                    node.Left = AddRecursive(node.Left, value);
                }
                else if (value > node.Value)
                {
                    node.Right = AddRecursive(node.Right, value);
                }

                return node;
            }


            public bool Search(int value)
            {
                return SearchRecursive(Root, value);
            }

            private bool SearchRecursive(Node node, int value)
            {
                if (node == null) return false;
                if (value == node.Value) return true;

                return value < node.Value
                    ? SearchRecursive(node.Left, value)
                    : SearchRecursive(node.Right, value);
            }

            public int FindMin()
            {
                return FindMinRecursive(Root).Value;
            }

            private Node FindMinRecursive(Node node)
            {
                return node.Left == null ? node : FindMinRecursive(node.Left);
            }

            public int FindMax()
            {
                return FindMaxRecursive(Root).Value;
            }

            private Node FindMaxRecursive(Node node)
            {
                return node.Right == null ? node : FindMaxRecursive(node.Right);
            }

            public List<int> PreOrder()
            {
                List<int> result = new List<int>();
                PreOrderRecursive(Root, result);
                return result;
            }

            private void PreOrderRecursive(Node node, List<int> result)
            {
                if (node != null)
                {
                    result.Add(node.Value);
                    PreOrderRecursive(node.Left, result);
                    PreOrderRecursive(node.Right, result);
                }
            }

            public List<int> InOrder()
            {
                List<int> result = new List<int>();
                InOrderRecursive(Root, result);
                return result;
            }

            private void InOrderRecursive(Node node, List<int> result)
            {
                if (node != null)
                {
                    InOrderRecursive(node.Left, result);
                    result.Add(node.Value);
                    InOrderRecursive(node.Right, result);
                }
            }

            public List<int> PostOrder()
            {
                List<int> result = new List<int>();
                PostOrderRecursive(Root, result);
                return result;
            }

            private void PostOrderRecursive(Node node, List<int> result)
            {
                if (node != null)
                {
                    PostOrderRecursive(node.Left, result);
                    PostOrderRecursive(node.Right, result);
                    result.Add(node.Value);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
