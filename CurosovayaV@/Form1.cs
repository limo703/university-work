using System;
using System.Text;
using System.Windows.Forms;

namespace CurosovayaV_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        PerfectTree tree = new PerfectTree();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                tree.Add(int.Parse(textBox1.Text));
                textBox1.Text = null;
                tree.sw.Close();
                File.WriteAllText("D:\\tree.txt", "");
                tree.sw = new StreamWriter("D:\\tree.txt", false);
                if (tree.root != null)
                {
                    tree.print(tree.root);
                    for (int i = 1; i <= tree.ForPrint1.Max(); i++)
                    {
                        for (int k = 0; k < tree.ForPrint.Count; k++)
                        {
                            if (tree.ForPrint1[k] == i) tree.sw.Write(tree.ForPrint[k] + " ");
                        }
                        tree.sw.WriteLine();
                    }
                    tree.ForPrint1.Clear();
                    tree.ForPrint.Clear();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный формат", "Ошибка");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                textBox2.Text = tree.Find(int.Parse(textBox2.Text));
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный формат", "Ошибка");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                tree.Delete(int.Parse(textBox3.Text));
                tree.sw.Close();
                File.WriteAllText("D:\\tree.txt", "");
                tree.sw = new StreamWriter("D:\\tree.txt", false);
                textBox3.Text = null;
                if (tree.root != null)
                {
                    tree.print(tree.root);
                    for (int i = 1; i <= tree.ForPrint1.Max(); i++)
                    {
                        for (int k = 0; k < tree.ForPrint.Count; k++)
                        {
                            if (tree.ForPrint1[k] == i) tree.sw.Write(tree.ForPrint[k] + " ");
                        }
                        tree.sw.WriteLine();
                    }
                    tree.ForPrint1.Clear();
                    tree.ForPrint.Clear();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный формат", "Ошибка");
            }
        }

        private void printTree(Node node )
        {
            
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (tree.root != null)
            {
                richTextBox1.Text = null;
                tree.sw.Close();
                StreamReader sr = new StreamReader("D:\\tree.txt");
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    richTextBox1.AppendText(line+"\n");
                }
                sr.Close();
                tree.sw = new StreamWriter("D:\\tree.txt");
            }
            else richTextBox1.Text = "дерево пусто";
            
        }
    }
    public class Node
    {
        public int data;
        public Node left;//правый потомок
        public Node right;//левый потомок
        public Node(int data)//классический конструктор
        {
            this.data = data;
        }
    }
    class PerfectTree
    {
        
        public StreamWriter sw = new StreamWriter("D:\\tree.txt");
        public List<string> ForPrint = new List<string>();//
        public List<int> ForPrint1 = new List<int>();
        public void print(Node node,int i = 1)
        {
            if(node!= null)
            {
                ForPrint.Add(node.data.ToString());
                ForPrint1.Add(i);
                if (node.left != null) print(node.left, i+1);
                else { ForPrint.Add("null"); ForPrint1.Add(i+1); }
                if (node.right != null) print(node.right, i+1);
                else {ForPrint.Add("null"); ForPrint1.Add(i+1); }
        }
        }
        public Node root;//основа дерева
        public void Add(int data)
        {
            Node newItem = new Node(data);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
        }
        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (n.data < current.data)
            {
                current.left = RecursiveInsert(current.left, n);
                current = balance_tree(current);
            }
            else if (n.data > current.data)
            {
                current.right = RecursiveInsert(current.right, n);
                current = balance_tree(current);
            }
            return current;
        }
        public string Find(int a)
        {
            if (FindClose(a) != null) return FindClose(a).data.ToString() + " найдено";
            else return "Не найдено";
        }
        private Node FindClose(int value)
        {
            if (root != null)
            {
                Node currentNode = root;
                while (currentNode.data != value)
                {
                    if (value < currentNode.data)
                    {
                        currentNode = currentNode.left;
                    }
                    else
                    {
                        currentNode = currentNode.right;
                    }
                    if (currentNode == null)
                    {
                        return null;
                    }
                }
                return currentNode;
            }
            return null;
        }
        private Node balance_tree(Node current)
        {
            int b_factor = balance_factor(current);
            if (b_factor > 1)
            {
                if (balance_factor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (balance_factor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }
        public void Delete(int target)
        {
            root = Delete(root, target);
        }
        private Node Delete(Node current, int target)
        {
            Node parent;
            if (current == null)
            { return null; }
            else
            {
                if (target < current.data)
                {
                    current.left = Delete(current.left, target);
                    if (balance_factor(current) == -2)
                    {
                        if (balance_factor(current.right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                else if (target > current.data)
                {
                    current.right = Delete(current.right, target);
                    if (balance_factor(current) == 2)
                    {
                        if (balance_factor(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                else
                {
                    if (current.right != null)
                    {
                        parent = current.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        current.data = parent.data;
                        current.right = Delete(current.right, parent.data);
                        if (balance_factor(current) == 2)
                        {
                            if (balance_factor(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {
                        return current.left;
                    }
                }
            }
            return current;
        }
        

        private int max(int l, int r)
        {
            return l > r ? l : r;
        }
        private int getHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = getHeight(current.left);
                int r = getHeight(current.right);
                int m = max(l, r);
                height = m + 1;
            }
            return height;
        }
        private int balance_factor(Node current)
        {
            int l = getHeight(current.left);
            int r = getHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }
        private Node RotateRR(Node parent)
        {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }
        private Node RotateLL(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }
        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }
}