using System;
using System.CodeDom.Compiler;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CursovayaNicolaev
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
        BinaryTree tree = new BinaryTree();
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                tree.insertNewBook(int.Parse(textBox1.Text), textBox3.Text, textBox2.Text, int.Parse(textBox4.Text));
                textBox1.Text = null;
                textBox2.Text = null;
                textBox3.Text = null;
                textBox4.Text = null;
            }
            catch (Exception)
            {
                MessageBox.Show("Error","Message");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                tree.delBook(int.Parse(textBox5.Text));
                textBox5.Text = null;
            }
            catch (Exception)
            {
                MessageBox.Show("Error", "Message");
            }

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (tree.forCheckBookBtn3(tree.rootBook, textBox6.Text))
                    richTextBox1.Text = "Книга в наличии.";
                else richTextBox1.Text = "Книги в наличии нет.";   
            }
            catch (Exception)
            {
                MessageBox.Show("error");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tree.sw.Close();
            StreamReader sr = new StreamReader("D:\\derevo.txt");
            string line;
            richTextBox1.Text = null;
            while((line = sr.ReadLine())!= null)
            {
                richTextBox1.AppendText(line+ "\n");
            }
            sr.Close();
            tree.sw = new StreamWriter("D:\\derevo.txt", true);
        }
    }
    class Book
    {
        
        private int udk;//удк 
        public string nameOfCreater;//автор
        public string nameOfBook;//название
        public int yearOfBook;//год
        public int number;//колличество экземпляров

        public Book(int udk, string nameOfCreater, string nameOfBook, int yearOfBook)
        {
            this.udk = udk;
            this.nameOfCreater = nameOfCreater;
            this.nameOfBook = nameOfBook;
            this.yearOfBook = yearOfBook;
            number = 1;
        }
        public int GetUdk() { return udk; }//для получения Удк
        public Book rightBook;//правый потомок
        public Book leftBook;//левый потомок
    }

    class BinaryTree
    {
        public Book rootBook;

        public StreamWriter sw = new StreamWriter("D:\\derevo.txt", false);
        private void writeInFile(Book book,string road)
        {

            if (book != null)
            {
                sw.WriteLine($"УДК: {book.GetUdk().ToString()}, Название: {book.nameOfBook}, Автор: {book.nameOfCreater}, Год: {book.yearOfBook.ToString()}, Количество: {book.number.ToString()};");
                if (book.rightBook != null) writeInFile(book.rightBook, road);
                if (book.leftBook != null) writeInFile(book.leftBook, road);
            }
        }
        public void insertNewBook(int udk, string nameOfCreater, string nameOfBook, int yearOfBook)
        {

            Book newBook = new Book(udk, nameOfCreater, nameOfBook, yearOfBook);
            if (rootBook == null) { rootBook = newBook; rootBook.number = 1; }
            else
            {
                Book tempBook = rootBook;
                Book parentBook;
                while (true)
                {
                    parentBook = tempBook;
                    if (newBook.GetUdk() == tempBook.GetUdk()) break;
                    else if (newBook.GetUdk() < tempBook.GetUdk())
                    {
                        tempBook = tempBook.leftBook;
                        if (tempBook == null) {  parentBook.leftBook = newBook; break; }
                    }
                    else
                    {
                        tempBook = tempBook.rightBook;
                        if (tempBook == null) {parentBook.rightBook = newBook; break; }
                    }
                }
                newBook.number = 0;
                checkBookforNumber(newBook, rootBook);
            }
            sw.Close();
            File.WriteAllText("D:\\derevo.txt", "");
            sw = new StreamWriter("D:\\derevo.txt", false);
            writeInFile(rootBook, "D:\\derevo.txt");


        }
        

        public bool forCheckBookBtn3(Book book,string name)
        {
            if (book.nameOfBook == name) return true;
            if (book.leftBook != null) return forCheckBookBtn3(book.leftBook, name);
            if (book.rightBook != null) return forCheckBookBtn3(book.rightBook, name);
            return false;
        }

        public bool delBook(int value)
        {
            Book currentBook = rootBook;
            Book parentBook = rootBook;
            bool isLeftChild = true;
            while (currentBook.GetUdk() != value)
            { 
                parentBook = currentBook;
                if (value < currentBook.GetUdk())
                { 
                    isLeftChild = true;
                    currentBook = currentBook.leftBook;
                }
                else
                { 
                    isLeftChild = false;
                    currentBook = currentBook.rightBook;
                }
                if (currentBook == null)
                    return false;
            }

            if (currentBook.leftBook == null && currentBook.rightBook == null)
            { 
                if (currentBook == rootBook) 
                    rootBook = null;
                else if (isLeftChild)
                {
                    parentBook.leftBook = null; 
                }
                else
                {
                    parentBook.rightBook = null;
                }
            }
            else if (currentBook.rightBook == null)
            {
                if (currentBook == rootBook)
                    rootBook = currentBook.leftBook;
                else if (isLeftChild)
                    parentBook.leftBook = currentBook.leftBook;
                else
                    parentBook.rightBook = currentBook.leftBook;
            }
            else if (currentBook.leftBook == null)
            { 

                if (currentBook == rootBook)
                    rootBook = currentBook.rightBook;
                else if (isLeftChild)
                    parentBook.leftBook = currentBook.rightBook;
                else
                    parentBook.rightBook = currentBook.rightBook;
            }
            else
            { 
                Book heir = receiveNext(currentBook);
                if (currentBook == rootBook)
                    rootBook = heir;
                else if (isLeftChild)
                    parentBook.leftBook = heir;
                else
                    parentBook.rightBook = heir;
            }
            minusBookforNumber(rootBook, rootBook);
            forDelite();
            return true;

        }
        private void minusBookforNumber(Book book,Book rootBook)
        {
            book.number = 0;
            checkBookforNumber(book, rootBook);
            if (book.leftBook != null) minusBookforNumber( book.leftBook,rootBook);
            if (book.rightBook != null) minusBookforNumber(book.rightBook,rootBook);
        }
        private Book receiveNext(Book Book)
        {
            Book parentBook = Book;
            Book heirBook = Book;
            Book tempBook = Book.rightBook;
            while (tempBook != null)
            {
                parentBook = heirBook;
                heirBook = tempBook;
                tempBook = tempBook.leftBook;
            }

            if (heirBook != Book.rightBook)
            {
                parentBook.leftBook = heirBook.rightBook;
                heirBook.rightBook = Book.rightBook;
            }
            return heirBook;
        }
        private void forDelite()
        {
            sw.Close();
            File.WriteAllText("D:\\derevo.txt", "");
            sw = new StreamWriter("D:\\derevo.txt", false);
            writeInFile(rootBook, "D:\\derevo.txt");
        }
        private void checkBookforNumber(Book name,Book book)
        {
            if (name.nameOfBook == book.nameOfBook && name.yearOfBook == book.yearOfBook && name.nameOfCreater == book.nameOfCreater) 
                if (book == name) book.number++; 
                else 
                { 
                    book.number++;
                    name.number++;
                }
            if (book.leftBook != null) checkBookforNumber(name , book.leftBook);
            if (book.rightBook != null) checkBookforNumber(name , book.rightBook);
        }
    }
}