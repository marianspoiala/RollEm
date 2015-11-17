using System;
using System.Collections.Generic;

public class Trie
{
    public TrieNode Root { get; private set; }

    public Trie()
    {
        Root = new TrieNode(TrieNode.Root);
    }

    public void AddWord(string word)
    {
        word = word.ToLower() + TrieNode.Eow;
        TrieNode current_node = Root;
        foreach (var letter in word)
        {
            current_node = current_node.AddChild(letter);
        }
    }

    public bool LookUp(string word)
    {
        word = word.ToLower();
        TrieNode current_node = Root;

        foreach (var letter in word)
        {
            if (!current_node.ContainsKey(letter))
                return false;
            else
                current_node = current_node.Children[letter];
        }
        return current_node.IsLeaf;
    }
}
