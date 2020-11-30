using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Test.Sotkom.Data;

namespace Test.Sotkom.ViewModels
{
    public class IndexViewModel : INotifyPropertyChanged
    {
        public int TimeOfFuture { get; set; }
        public List<Bun> Buns { get; set; }

        private readonly BunDbContext _dbContext;

        public IndexViewModel(BunDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            Buns = _dbContext.Buns.ToList();
        }

        public void ReloadTable(ChangeEventArgs args)
        {
            TimeOfFuture = Convert.ToInt32(args.Value);

            OnPropertyChanged(nameof(Buns));
        } 

        public double PriceForTime(Bun bun, int? time = null)
        {
            time ??= TimeOfFuture;

            if (time > bun.CountBadHours) return 0;
            if (time < bun.CountFreshHours) return bun.Price;

            double newPrice = bun.BunType switch
            {
                TypeOfBun.Pretzel => PretzelPrice(bun),
                TypeOfBun.SourCream => SourCreamPrice(time.Value, bun),
                _ => BunPrice(time.Value, bun),
            };

            return newPrice > 0 ? newPrice : 0;
        }

        private double SourCreamPrice(int time, Bun bun)
        {
            return bun.Price - DefaultLowPrice(time, bun) * 2;
        }
        private double BunPrice(int time, Bun bun)
        {
            return bun.Price - DefaultLowPrice(time, bun);
        }
        private double PretzelPrice(Bun bun)
        {
            return bun.Price / 2;
        }

        private static double DefaultLowPrice(int time, Bun bun)
        {
            return (time * bun.Price * 0.02);
        }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify on property changed
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
