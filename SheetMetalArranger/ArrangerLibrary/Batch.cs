using ArrangerLibrary.Abstractions;
using System.Collections.Generic;

namespace ArrangerLibrary
{
    public class Batch : IBatch
    {
        private List<IItem> content;

        public List<IItem> Content()
        {
            return new List<IItem>(content);
        }

        public int Remaining { get { return content.Count; } }

        public Batch()
        {
            content = new List<IItem>();
        }

        public Batch(List<IItem> _items)
        {
            content = new List<IItem>();
            content.AddRange(_items);
        }

        public void AddItem(IItem _item)
        {
            content.Add(_item);
        }

        public bool RemoveItem(IItem _item)
        {
            if(content.Contains(_item))
            {
                content.Remove(_item);
                return true;
            }
            return false;
        }

        public void AddItems(List<IItem> _items)
        {
            content.AddRange(_items);
        }

        public void Clear()
        {
            content.Clear();
        }

        public IItem GetFirst(IComparer<IItem> _comparer1, IComparer<IItem> _comparer2, IComparer<IItem> _comparer3)
        {
            bool finished;
            int runs = 0;
            do
            {
                finished = true;
                for (int i = 0; i < content.Count - 1 - runs; i++)
                {
                    if (_comparer1.Compare(content[i], content[i + 1]) != 1)
                    {
                        if (_comparer1.Compare(content[i], content[i + 1]) == 0)
                        {
                            if (_comparer2.Compare(content[i], content[i + 1]) != 1)
                            {
                                if (_comparer2.Compare(content[i], content[i + 1]) == 0)
                                {
                                    if (_comparer3.Compare(content[i], content[i + 1]) == -1)
                                    {
                                        //switch position
                                        IItem bufferI0 = content[i].CreateCopy();
                                        IItem bufferI1 = content[i + 1].CreateCopy();
                                        content.RemoveAt(i);
                                        content.Insert(i, bufferI1);
                                        content.RemoveAt(i + 1);
                                        content.Insert(i + 1,bufferI0);
                                        finished = false;
                                    }
                                }
                                else
                                {
                                    //switch position
                                    IItem bufferI0 = content[i].CreateCopy();
                                    IItem bufferI1 = content[i + 1].CreateCopy();
                                    content.RemoveAt(i);
                                    content.Insert(i, bufferI1);
                                    content.RemoveAt(i + 1);
                                    content.Insert(i + 1, bufferI0);
                                    finished = false;
                                }
                            }
                        }
                        else
                        {
                            //switch position
                            IItem bufferI0 = content[i].CreateCopy();
                            IItem bufferI1 = content[i + 1].CreateCopy();
                            content.RemoveAt(i);
                            content.Insert(i, bufferI1);
                            content.RemoveAt(i + 1);
                            content.Insert(i + 1, bufferI0);
                            finished = false;
                        }
                    }
                }
                runs++;
            } while (!finished);
            //content.Reverse();
            return content[0];
        }
    }
}
