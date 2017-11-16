
using System;
using System.Collections.Generic;
using System.Linq;
using CoTech.Bi.Entity;
using CoTech.Bi.Loader;
using CoTech.Bi.Modules.Wer.Models.Entities;

namespace CoTech.Bi.Modules.Wer.Seeds

{
    public class WerSeed1Weeks : ISeed
    {
        public int Version => 1;

        public List<WeekEntity> Weeks { get; set; }
        public WerSeed1Weeks(){
            this.Weeks = new List<WeekEntity>();
            var firstDay = new DateTime(2017, 7, 17);
            var endDay = DateTime.Today;
            var auxDate = firstDay;
            int idCounter = 1;
            while(auxDate.Ticks < endDay.Ticks){
                this.Weeks.Add(new WeekEntity(){
                    Id = idCounter,
                    StartTime = auxDate,
                    EndTime = auxDate.AddDays(4)
                });
                auxDate = auxDate.AddDays(7);
                idCounter += 1;
            }
        }
        public void Down(BiContext context)
        {
            var weeks = context.Set<WeekEntity>().ToList();
            context.Set<WeekEntity>().RemoveRange(weeks);
            context.SaveChanges();
        }

        public void Up(BiContext context)
        {
            context.Set<WeekEntity>().AddRange(this.Weeks);
            context.SaveChanges();
        }
    }
}