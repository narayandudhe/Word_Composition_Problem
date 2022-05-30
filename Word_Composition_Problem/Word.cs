
using System.Collections.Generic;
using System.Linq;

namespace Word_Composition_Problem
{
    public class Word
    {
        private string _name;
        private List<string> _splitWords;
        private List<string> _subWords;
        private List<string> _buffer;
        private int _numberOfSubWords;
        public Word(string name)
        {
            //parameterised constructer
            _name = name;
            _subWords = new List<string>();
            _buffer = new List<string>();
            splitWords(_name);
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        //search word in tree
        public void Search(Trie tree)
        {
            foreach (string wor in _splitWords)
            {
                if (tree.Contains(wor))
                {
                    _buffer.Add(wor);
                }
            }
            Stack<string> sub = new Stack<string>();
            findSubWords(Name, sub);
            _subWords = sub.Distinct().ToList();
            _numberOfSubWords = _subWords.Count;
        }

        public bool IsComplete()
        {
            return _subWords.Count != 0;
        }

        

        private void splitWords(string name)
        {
            HashSet<string> s = new HashSet<string>();
            int index = 0;
            while (index <= (name.Length - 2))
            {
                int length = 2;
                while ((index + length) <= name.Length)
                {
                    string substring = name.Substring(index, length);
                    if (substring.Length == name.Length) break;
                    s.Add(substring);
                    length += 1;
                }
                index += 1;
            }
            _splitWords = s.OrderByDescending(x => x.Length).ThenByDescending(x => x).ToList();
        }

        //find longest words subparts
        private bool findSubWords(string name, Stack<string> subwords)
        {
            foreach (var s in _buffer.Where(x => x[0] == name[0]).OrderByDescending(x => x.Length))
            {
                if (name.IndexOf(s) == 0)
                {
                    string newStr = name.Substring(s.Length, name.Length - s.Length);
                    if (string.IsNullOrEmpty(newStr) || findSubWords(newStr, subwords))
                    {
                        subwords.Push(s);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
