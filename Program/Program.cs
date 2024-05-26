using System;
using System.Collections.Generic;

namespace LABA6AOIOS_
{
    public class Node
    {
        public string ID { get; set; }
        public bool CollisionFlag { get; set; }
        public bool OccupiedFlag { get; set; }
        public bool TerminalFlag { get; set; }
        public bool LinkFlag { get; set; }
        public bool DeletedFlag { get; set; }
        public int OverflowPointer { get; set; }
        public string Data { get; set; }

        public Node(string id, string data)
        {
            ID = id;
            Data = data;
            CollisionFlag = false;
            OccupiedFlag = true;
            TerminalFlag = false;
            LinkFlag = false;
            DeletedFlag = false;
            OverflowPointer = -1;
        }
    }

    public class LiteratureDictionary
    {
        private int capacity;
        private Node[] table;

        public LiteratureDictionary(int capacity = 20)
        {
            this.capacity = capacity;
            table = new Node[capacity];
        }

        private int HashFunction(string key)
        {
            if (key.Length < 2) throw new ArgumentException("Key must have at least two characters.");
            int first = char.ToUpper(key[0]) - 'A';
            int second = char.ToUpper(key[1]) - 'A';
            return (first * 26 + second) % capacity;
        }

        public void AddTerm(string term, string definition)
        {
            int index = HashFunction(term);

            for (int i = 0; i < capacity; i++)
            {
                int probeIndex = (index + i) % capacity;

                if (table[probeIndex] == null || table[probeIndex].DeletedFlag)
                {
                    table[probeIndex] = new Node(term, definition);
                    if (i > 0)
                    {
                        table[probeIndex].CollisionFlag = true;
                    }
                    return;
                }
            }

            throw new Exception("HashTable is full");
        }

        public string SearchTerm(string term)
        {
            int index = HashFunction(term);

            for (int i = 0; i < capacity; i++)
            {
                int probeIndex = (index + i) % capacity;

                if (table[probeIndex] == null)
                {
                    return "The term is not found in the dictionary.";
                }

                if (table[probeIndex].ID == term && !table[probeIndex].DeletedFlag)
                {
                    return table[probeIndex].Data;
                }
            }

            return "The term is not found in the dictionary.";
        }

        public string DeleteTerm(string term)
        {
            int index = HashFunction(term);

            for (int i = 0; i < capacity; i++)
            {
                int probeIndex = (index + i) % capacity;

                if (table[probeIndex] == null)
                {
                    return "The term is not found in the dictionary.";
                }

                if (table[probeIndex].ID == term && !table[probeIndex].DeletedFlag)
                {
                    table[probeIndex].DeletedFlag = true;
                    return "The term has been successfully deleted.";
                }
            }

            return "The term is not found in the dictionary.";
        }

        public void DisplayAllTerms()
        {
            Console.WriteLine("A list of all terms and their definitions:");
            for (int i = 0; i < capacity; i++)
            {
                if (table[i] != null && !table[i].DeletedFlag)
                {
                    Console.WriteLine($"Cell {i}: {table[i].ID}: {table[i].Data}, CollisionFlag: {table[i].CollisionFlag}");
                }
                else
                {
                    Console.WriteLine($"Cell {i}: Empty");
                }
            }
        }


    }
}
