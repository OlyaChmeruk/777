using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach
{
    /**
    Single cell class
        Fields: value, fieldState
    */
    public class Cell
    {
        int n;
    
        /**
        Constructor, set cell empty on create.
        */
        public Cell()
        {
            this.n = 0;
        }
        /////////////////////////////////////////////////////////
        /**
       Check whether fieldstate is empty
       */
        public bool isZeroValue()
        {
            return (n == 0);
              
        }

        /**
        Set zero value to cell (reset cell).
        */
        public void setZeroValue()
        {
            n = 0;
        }

        /**
         Set beginning value
        */
        public void setFirstValue()
        {
            n= 2;
        }

        /**
        Set givenValue to current cell.
        */
        public void setValue(int givenValue)
        {
            n = givenValue;
        }

        /**
        Get Value
        */
        public int getValue()
        {
            return n;
        }

        /**
        Double value
        */
        public void doubleValue()
        {
            n *= 2;
        }
    }
}
