﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coordinates
{
    class CoordinateData
    {
        List<double> xCoord = new List<double>();
        List<double> yCoord = new List<double>();
        List<double> zCoord = new List<double>();

        public List<double> XCoord { get => xCoord; set => xCoord = value; }
        public List<double> YCoord { get => yCoord; set => yCoord = value; }
        public List<double> ZCoord { get => zCoord; set => zCoord = value; }
    }
}
