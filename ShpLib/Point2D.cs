﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShpLib
{
    public class Point2D
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}