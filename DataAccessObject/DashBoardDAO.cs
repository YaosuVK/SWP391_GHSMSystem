using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class DashBoardDAO : BaseDAO<Appointment>
    {
        private readonly GHSMContext _context;
        public DashBoardDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<(object span, int totalAppointments, double totalAppointmentsAmount)>> GetTotalAppointmentsTotalAppointmentsAmountAsync(
        DateTime startDate, DateTime endDate, string? timeSpanType)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException($"startDate <= endDate");
            }
            List<(object span, int totalAppointments, double totalAppointmentsAmount)> result = new List<(object, int, double)>();

            switch (timeSpanType?.ToLower())
            {
                case "day":
                    for (DateTime date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
                    {
                        DateTime currentDayStart = date.Date;
                        DateTime currentDayEnd = date.Date.AddDays(1).AddTicks(-1);
                        double totalAppointmentsAmount = 0;
                        int totalAppointments = 0;

                            var transactionsDayCompleted = await _context.Transactions
                            .Where(t => t.StatusTransaction == StatusOfTransaction.Completed)
                            .Where(t => t.FinishDate >= currentDayStart && t.FinishDate <= currentDayEnd)
                            .ToListAsync();

                            var transactionsDayRefunded = await _context.Transactions
                                .Where(t => t.StatusTransaction == StatusOfTransaction.Refunded)
                                .Where(t => t.FinishDate >= currentDayStart && t.FinishDate <= currentDayEnd)
                                .ToListAsync();

                            var transactionsDayCancelled = await _context.Transactions
                                .Where(t => t.StatusTransaction == StatusOfTransaction.Cancelled)
                                .Where(t => t.FinishDate >= currentDayStart && t.FinishDate <= currentDayEnd)
                                .ToListAsync();

                        // totalAppointments là tổng count của Completed + Refunded
                        totalAppointments += transactionsDayCompleted.Count;

                        // totalAppointmentsAmount là tổng OwnerAmount của Completed + Cancelled + Refunded
                        totalAppointmentsAmount += transactionsDayCompleted.Sum(t => t.Amount)
                               + transactionsDayCancelled.Sum(t => t.Amount)
                               + transactionsDayRefunded.Sum(t => t.Amount);

                        result.Add((date.Date, totalAppointments, totalAppointmentsAmount));
                    }
                    break;

                case "week":
                    DateTime currentWeekStart = startDate.Date.AddDays(-(int)startDate.DayOfWeek + (int)DayOfWeek.Monday);
                    if (currentWeekStart > startDate.Date)
                    {
                        currentWeekStart = startDate.Date.AddDays(-(int)startDate.DayOfWeek - 6);
                    }
                    while (currentWeekStart <= endDate.Date)
                    {
                        DateTime currentWeekEnd = currentWeekStart.AddDays(6);
                        if (currentWeekEnd > endDate.Date)
                        {
                            currentWeekEnd = endDate.Date.AddTicks(-(int)endDate.DayOfWeek + 7);
                        }

                        double totalAppointmentsAmount = 0;
                        int totalAppointments = 0;

                            var transactionsWeekCompleted = await _context.Transactions
                             .Where(t => t.StatusTransaction == StatusOfTransaction.Completed)
                             .Where(t => t.FinishDate >= currentWeekStart && t.FinishDate <= currentWeekEnd)
                             .ToListAsync();

                            var transactionsWeekRefunded = await _context.Transactions
                                .Where(t => t.StatusTransaction == StatusOfTransaction.Refunded)
                                .Where(t => t.FinishDate >= currentWeekStart && t.FinishDate <= currentWeekEnd)
                                .ToListAsync();

                            var transactionsWeekCancelled = await _context.Transactions
                                .Where(t => t.StatusTransaction == StatusOfTransaction.Cancelled)
                                .Where(t => t.FinishDate >= currentWeekStart && t.FinishDate <= currentWeekEnd)
                                .ToListAsync();

                        // totalAppointments là tổng count của Completed + Refunded
                        totalAppointments += transactionsWeekCompleted.Count;

                        // totalAppointmentsAmount là tổng OwnerAmount của Completed + Cancelled + Refunded
                        totalAppointmentsAmount += transactionsWeekCompleted.Sum(t => t.Amount)
                               + transactionsWeekCancelled.Sum(t => t.Amount)
                               + transactionsWeekRefunded.Sum(t => t.Amount);
                        
                        string weekRange = $"{currentWeekStart:MM/dd/yyyy} - {currentWeekEnd:MM/dd/yyyy}";
                        result.Add((weekRange, totalAppointments, totalAppointmentsAmount));

                        currentWeekStart = currentWeekEnd.AddDays(1);
                    }
                    break;

                case "month":
                    DateTime currentMonthStart = new DateTime(startDate.Year, startDate.Month, 1);

                    while (currentMonthStart <= endDate.Date)
                    {
                        DateTime currentMonthEnd = currentMonthStart.AddMonths(1).AddTicks(-1);

                        double totalAppointmentsAmount = 0;
                        int totalAppointments = 0;

                            
                            var transactionsMonthCompleted = await _context.Transactions
                             .Where(t => t.StatusTransaction == StatusOfTransaction.Completed)
                             .Where(t => t.FinishDate >= currentMonthStart && t.FinishDate <= currentMonthEnd)
                             .ToListAsync();

                            var transactionsMonthRefunded = await _context.Transactions
                                .Where(t => t.StatusTransaction == StatusOfTransaction.Refunded)
                                .Where(t => t.FinishDate >= currentMonthStart && t.FinishDate <= currentMonthEnd)
                                .ToListAsync();

                            var transactionsMonthCancelled = await _context.Transactions
                                .Where(t => t.StatusTransaction == StatusOfTransaction.Cancelled)
                                .Where(t => t.FinishDate >= currentMonthStart && t.FinishDate <= currentMonthEnd)
                                .ToListAsync();

                        // totalAppointments là tổng count của Completed + Refunded
                        totalAppointments += transactionsMonthCompleted.Count;

                        // totalAppointmentsAmount là tổng OwnerAmount của Completed + Cancelled + Refunded
                        totalAppointmentsAmount += transactionsMonthCompleted.Sum(t => t.Amount)
                               + transactionsMonthCancelled.Sum(t => t.Amount)
                               + transactionsMonthRefunded.Sum(t => t.Amount);
                        

                        string monthName = currentMonthStart.ToString("MM/yyyy");
                        result.Add((monthName, totalAppointments, totalAppointmentsAmount));

                        currentMonthStart = currentMonthStart.AddMonths(1);
                    }
                    break;

                default:
                    for (DateTime date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
                    {
                        DateTime currentDayStart = date.Date;
                        DateTime currentDayEnd = date.Date.AddDays(1).AddTicks(-1);
                        double totalAppointmentsAmount = 0;
                        int totalAppointments = 0;

                        var transactionsDayCompleted = await _context.Transactions
                        .Where(t => t.StatusTransaction == StatusOfTransaction.Completed)
                        .Where(t => t.FinishDate >= currentDayStart && t.FinishDate <= currentDayEnd)
                        .ToListAsync();

                        var transactionsDayRefunded = await _context.Transactions
                            .Where(t => t.StatusTransaction == StatusOfTransaction.Refunded)
                            .Where(t => t.FinishDate >= currentDayStart && t.FinishDate <= currentDayEnd)
                            .ToListAsync();

                        var transactionsDayCancelled = await _context.Transactions
                            .Where(t => t.StatusTransaction == StatusOfTransaction.Cancelled)
                            .Where(t => t.FinishDate >= currentDayStart && t.FinishDate <= currentDayEnd)
                            .ToListAsync();

                        // totalAppointments là tổng count của Completed + Refunded
                        totalAppointments += transactionsDayCompleted.Count;

                        // totalAppointmentsAmount là tổng OwnerAmount của Completed + Cancelled + Refunded
                        totalAppointmentsAmount += transactionsDayCompleted.Sum(t => t.Amount)
                               + transactionsDayCancelled.Sum(t => t.Amount)
                               + transactionsDayRefunded.Sum(t => t.Amount);

                        result.Add((date.Date, totalAppointments, totalAppointmentsAmount));
                    }
                    break;
            }
            return result;
        }

        public async Task<(int totalBookings, double totalBookingsAmount)> GetTotalAppointmentsAndAmount()
        {
            double totalAppointmentsAmount = 0;
            int totalAppointments = 0;

                var transactionsDayCompleted = await _context.Transactions
                            .Where(t => t.StatusTransaction == StatusOfTransaction.Completed)
                            .ToListAsync();

                var transactionsDayRefunded = await _context.Transactions
                    .Where(t => t.StatusTransaction == StatusOfTransaction.Refunded)
                    .ToListAsync();

                var transactionsDayCancelled = await _context.Transactions
                    .Where(t => t.StatusTransaction == StatusOfTransaction.Cancelled)
                    .ToListAsync();

            // totalBookings là tổng count của Completed + Refunded
            totalAppointments += transactionsDayCompleted.Count + transactionsDayRefunded.Count;

            // totalBookingsAmount là tổng OwnerAmount của Completed + Cancelled + Refunded
            totalAppointmentsAmount += transactionsDayCompleted.Sum(t => t.Amount)
                   + transactionsDayCancelled.Sum(t => t.Amount)
                   + transactionsDayRefunded.Sum(t => t.Amount);
            
            return (totalAppointments, totalAppointmentsAmount);
        }
    }
}
