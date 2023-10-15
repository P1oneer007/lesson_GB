using System;
using System.Collections.Generic;

namespace Program
{
    using System;
    using System.Collections;
    using System.Xml.Linq;
    using static Program.GeekBrainsTests.DoublyLinkedList;

    namespace GeekBrainsTests
    {
        public class Node
            {
                public int Value { get; set; }
                public Node NextNode { get; set; }
                public Node PrevNode { get; set; }
            }
        public interface ILinkedList
            {
                int GetCount(); // возвращает количество элементов в списке
                void AddNode(int value); // добавляет новый элемент списка
                void AddNodeAfter(Node node, int value); // добавляет новый элемент
                //списка после определённого элемента
            void RemoveNode(int index); // удаляет элемент по порядковому номеру
                void RemoveNode(Node node); // удаляет указанный элемент
                Node FindNode(int searchValue); // ищет элемент по его значению
            }
        
        public class DoublyLinkedList: ILinkedList
        {
            
            //Начальную и конечную ноду нужно хранить в самой реализации интерфейса
            
            private Node head;
            private Node tail;
            private int count;

            public int GetCount()
            {
                return count;
            }

            public void AddNode(int value)
            {
                Node newNode = new Node { Value = value };

                if (head == null)
                {
                    head = newNode;
                    tail = newNode;
                }
                else
                {
                    tail.NextNode = newNode;
                    newNode.PrevNode = tail;
                    tail = newNode;
                }
                count++;
            }

            public void AddNodeAfter(Node node, int value)
            {
                if (node == null)
                {
                    throw new ArgumentNullException(nameof(node));
                }
                Node newNode = new Node { Value = value };

                newNode.NextNode = node.NextNode;
                newNode.PrevNode = node;
                node.NextNode = newNode;

                if (newNode.NextNode != null)
                {
                    newNode.NextNode.PrevNode = newNode;
                }
                else
                {
                    tail = newNode;
                }

                count++;
            }

            public void RemoveNode(int index)
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException();
                }

                if (index == 0)
                {
                    head = head.NextNode;
                    if (head != null)
                    {
                        head.PrevNode = null;
                    }
                }
                else if (index == count - 1)
                {
                    tail = tail.PrevNode;
                    tail.NextNode = null;
                }
                else
                {
                    Node currentNode = head;
                    for (int i = 0; i < index; i++)
                    {
                        currentNode = currentNode.NextNode;
                    }

                    currentNode.PrevNode.NextNode = currentNode.NextNode;
                    currentNode.NextNode.PrevNode = currentNode.PrevNode;
                }
                count--;
            }

            public void RemoveNode(Node node)
            {
                if (node == null)
                {
                    throw new ArgumentNullException(nameof(node));
                }

                if (node == head)
                {
                    head = head.NextNode;
                    if (head != null)
                    {
                        head.PrevNode = null;
                    }
                }
                else if (node == tail)
                {
                    tail = tail.PrevNode;
                    tail.NextNode = null;
                }
                else
                {
                    node.PrevNode.NextNode = node.NextNode;
                    node.NextNode.PrevNode = node.PrevNode;
                }

                count--;
            }

            public Node FindNode(int searchValue)
            {
                Node currentNode = head;
                while (currentNode != null)
                {
                    if (currentNode.Value == searchValue)
                    {
                        return currentNode;
                    }
                    currentNode = currentNode.NextNode;
                }

                return null;
            }
        }
        class Program
        {
            private static ILinkedList lList;
        static void Main(string[] args)
        {
            lList = new DoublyLinkedList();
            lList.AddNode(3);
            lList.AddNode(55);
            lList.AddNode(33);
            lList.AddNodeAfter(new Node(55), 22);
            Console.WriteLine("Первоначальный список");
            PrintLinkedList();

            lList.FindNode(33);
            lList.RemoveNode(3);
            Console.WriteLine("Удалили элемент с индексом 3");
            PrintLinkedList();

            lList.AddNode(11);
            lList.AddNodeAfter(new Node(3), 5);
            Console.WriteLine("Добавили элемент в хвост и новый после '3'");
            PrintLinkedList();
        }
        static void PrintLinkedList()
        {
            for (int i = 0; i < lList.GetCount(); i++)
            {
                    Console.WriteLine("Элемент связанного списка # {0} - {1}", i);
            }
        }
    }
}
}
