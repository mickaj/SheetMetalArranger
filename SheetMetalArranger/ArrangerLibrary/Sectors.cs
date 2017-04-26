﻿using System.Collections.Generic;
using ArrangerLibrary.Abstractions;


namespace ArrangerLibrary
{
    public sealed class HSector : ISector
    {
        private static readonly HSector instance = new HSector();

        public static HSector Instance
        {
            get { return instance; }
        }

        public List<IBox> DoSection(IBox _container, IItem _item)
        {
            int newX;
            int newY;
            int newWidth;
            int newHeight;
            List<IBox> result = new List<IBox>();
            //horizontal section
            //first newly created container
            newX = _container.PosX + _item.Width;
            newY = _container.PosY;
            newWidth = _container.Width - _item.Width;
            newHeight = _item.Height;
            IBox h1 = new Box(newX, newY, newHeight, newWidth);
            if (h1.Area > 0) { result.Add(h1); }
            //second newly created container
            newX = _container.PosX;
            newY = _container.PosY + _item.Height;
            newWidth = _container.Width;
            newHeight = _container.Height - _item.Height;
            IBox h2 = new Box(newX, newY, newHeight, newWidth);
            if (h2.Area > 0) { result.Add(h2); }
            return result;
        }
    }

    public sealed class VSector : ISector
    {
        private static readonly VSector instance = new VSector();

        public static VSector Instance
        {
            get { return instance; }
        }

        public List<IBox> DoSection(IBox _container, IItem _item)
        {
            int newX;
            int newY;
            int newWidth;
            int newHeight;
            List<IBox> result = new List<IBox>();
            //vertical section
            //first newly created container
            newX = _container.PosX;
            newY = _container.PosY + _item.Height;
            newWidth = _item.Width;
            newHeight = _container.Height - _item.Height;
            IBox v1 = new Box(newX, newY, newHeight, newWidth);
            if (v1.Area > 0) { result.Add(v1); }
            //second newly created container
            newX = _container.PosX + _item.Width;
            newY = _container.PosY;
            newWidth = _container.Width - _item.Width;
            newHeight = _container.Height;
            IBox v2 = new Box(newX, newY, newHeight, newWidth);
            if (v2.Area > 0) { result.Add(v2); }
            return result;
        }
    }
}