using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTVaniKhanna
{
    public class Car:Vehicle
    {
        public Car(int id, string name, double rentalPrice, string category, string type, bool isReserved)
         : base(id, name, rentalPrice, category, type, isReserved)
        {

        }
      /*  public override string ToString()
        {
            return base.ToString();
        }*/
    }
}
