using System;
using System.Collections;
using System.Collections.Generic;

public class TrieNode
{
    public const char Eow = '$';
    public const char Root = ' ';

    public char Letter { get; set; }
    public Dictionary<char, TrieNode> Children { get; private set; }
    public bool IsLeaf { get; set; }

    public TrieNode()
    {
    }

    public TrieNode(char letter)
    {
        this.Letter = letter;
    }

    public TrieNode(char letter, bool isLeaf)
    {
        this.Letter = letter;
        this.IsLeaf = isLeaf;
    }

    public ICollection Keys
    {
        get { return Children.Keys; }
    }

    public bool ContainsKey(char key)
    {
        return Children.ContainsKey(key);
    }

    public TrieNode AddChild(char letter)
    {
        if (Children == null)
        {
            Children = new Dictionary<char, TrieNode>();
        }
        if (!Children.ContainsKey(letter))
        {
            if (letter == Eow)
            {
                this.IsLeaf = true;
            }
            TrieNode child = letter != Eow ? new TrieNode(letter) : null;
            Children.Add(letter, child);
            return child;
        }
        else
        {
            return Children[letter];
        }
    }
}
