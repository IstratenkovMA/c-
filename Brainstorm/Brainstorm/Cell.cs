using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstorm
{
    class Cell
    {
        string type = "empty";

        public string getType()
        {
            return type;
        }

        public void setType(string value)
        {
            type = value;
        }

        public bool isBlocker()
        {
            if (type.Equals("block") || type.Equals("painted"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void paintCell()
        {
            type = "painted";
        }

        public void blockCell()
        {
            type = "block";
        }

        public void emptyCell()
        {
            type = "empty";
        }

        public void arrowCell(String direction)
        {
            type = direction + "Arrow";
        }

        public bool isArrow()
        {
            return type.Contains("Arrow");
        }
    }
}
