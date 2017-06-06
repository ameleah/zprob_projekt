using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bookreview.Models.BaseModels
{
    public abstract class Rateable
    {
        [Key]
        public int Id { get; private set; }
        public List<Rate> RateList { get; protected set; }
        
        public int GetNumberOfRates()
        {
            return RateList.Count;
        }

        public float GetAverageOfRates()
        {
            if (RateList.Count == 0) { return 0; }
            float sum = 0;
            foreach (Rate r in RateList)
            {
                sum += r.Value;
            }
            return sum / RateList.Count;
        }
    }
}