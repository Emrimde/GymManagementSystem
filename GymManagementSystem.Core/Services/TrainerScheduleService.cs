using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class TrainerScheduleService : ITrainerScheduleService
{
    private readonly IGeneralGymRepository _gymDetailsRepo;
    private readonly IPersonalBookingRepository _personalBookingRepository;
    private readonly ITrainerTimeOffRepository _trainerTimeOffRepo;

    public TrainerScheduleService(
        IGeneralGymRepository gymDetailsRepo,
        IPersonalBookingRepository personalBookingRepository,
        ITrainerTimeOffRepository trainerTimeOffRepo)
    {
        _gymDetailsRepo = gymDetailsRepo;
        _personalBookingRepository = personalBookingRepository;
        _trainerTimeOffRepo = trainerTimeOffRepo;
    }

    public async Task<Result<TrainerScheduleResponse>> GetTrainerScheduleAsync(
     Guid trainerId,
     int days,
     CancellationToken cancellationToken)
    {
        Console.WriteLine("======= GET TRAINER SCHEDULE =======");

        DateOnly startDay = DateOnly.FromDateTime(DateTime.Now.Date);
        DateOnly endDay = startDay.AddDays(days);

        Console.WriteLine($"RANGE: {startDay} → {endDay}");

        TrainerScheduleResponse trainerResponse = new()
        {
            TrainerId = trainerId,
            Days = new List<TrainerScheduleDay>()
        };

        // 1) Pobieramy TimeOff i Booking
        var trainerTimeOffs =
            await _trainerTimeOffRepo.GetForRangeAsync(trainerId, startDay, endDay, cancellationToken);

        var personalBookings =
            await _personalBookingRepository.GetForRangeAsync(trainerId, startDay, endDay, cancellationToken);

        Console.WriteLine("TIME-OFF FROM REPO:");
        foreach (var t in trainerTimeOffs)
            Console.WriteLine($"{t.Start} - {t.End}");

        Console.WriteLine("BOOKINGS FROM REPO:");
        foreach (var b in personalBookings)
            Console.WriteLine($"{b.Start} - {b.End}");

        // 2) USUWAMY CAŁĄ KONWERSJĘ UTC → LOCAL

        // 3) Pobranie godzin otwarcia
        var details = await _gymDetailsRepo.GetGeneralGymDetailsAsync(cancellationToken);
        if (details == null)
            return Result<TrainerScheduleResponse>.Failure("Details about gym not found", StatusCodeEnum.NotFound);

        TimeSpan openTime = details.OpenTime;
        TimeSpan closeTime = details.CloseTime;

        // 4) Pętla po dniach
        for (DateOnly date = startDay; date < endDay; date = date.AddDays(1))
        {
            Console.WriteLine($"\n=== DAY {date} ===");

            var day = new TrainerScheduleDay
            {
                Date = date,
                Items = new List<TrainerScheduleItem>()
            };

            var baseBlock = new TrainerScheduleItem
            {
                Start = date.ToDateTime(TimeOnly.FromTimeSpan(openTime)),
                End = date.ToDateTime(TimeOnly.FromTimeSpan(closeTime)),
                Type = TrainerScheduleItemType.Available
            };

            Console.WriteLine($"BASE BLOCK: {baseBlock.Start} → {baseBlock.End}");

            var items = new List<TrainerScheduleItem> { baseBlock };

            // BOOKINGS
            foreach (var booking in personalBookings.Where(b => DateOnly.FromDateTime(b.Start) == date))
            {
                Console.WriteLine($"BOOKING MATCHES DAY: {booking.Start} - {booking.End}");
                items = Split(items, booking.Start, booking.End, TrainerScheduleItemType.Booked, booking.Client?.FirstName);
            }

            // TIME-OFF
            foreach (var off in trainerTimeOffs.Where(t => DateOnly.FromDateTime(t.Start) == date))
            {
                Console.WriteLine($"TIMEOFF MATCHES DAY: {off.Start} - {off.End}");
                items = Split(items, off.Start, off.End, TrainerScheduleItemType.TimeOff, off.Reason);
            }

            Console.WriteLine("AFTER SPLIT:");
            foreach (var i in items)
                Console.WriteLine($"   ITEM: {i.Type} {i.Start} → {i.End}");

            day.Items = items.OrderBy(i => i.Start).ToList();
            trainerResponse.Days.Add(day);
        }

        Console.WriteLine("======= END SCHEDULE =======");

        return Result<TrainerScheduleResponse>.Success(trainerResponse);
    }

    private static List<TrainerScheduleItem> Split(
        List<TrainerScheduleItem> items,
        DateTime cutStart,
        DateTime cutEnd,
        TrainerScheduleItemType newType,
        string? clientName = null)
    {
        var result = new List<TrainerScheduleItem>();

        foreach (TrainerScheduleItem block in items)
        {
            if (!(cutStart < block.End && cutEnd > block.Start && block.Type != newType))
            {
                result.Add(block);
                continue;
            }

            if (block.Start < cutStart)
            {
                result.Add(new TrainerScheduleItem
                {
                    Start = block.Start,
                    End = cutStart,
                    Type = block.Type,
                    ClientName = block.ClientName

                });
            }

            var middleStart = Max(block.Start, cutStart);
            var middleEnd = Min(block.End, cutEnd);

            result.Add(new TrainerScheduleItem
            {
                Start = middleStart,
                End = middleEnd,
                Type = newType,
                ClientName = clientName
            });

            if (block.End > cutEnd)
            {
                result.Add(new TrainerScheduleItem
                {
                    Start = cutEnd,
                    End = block.End,
                    Type = block.Type,
                    ClientName = block.ClientName
                });
            }
        }

        return result;
    }

    private static DateTime Max(DateTime a, DateTime b) => a > b ? a : b;
    private static DateTime Min(DateTime a, DateTime b) => a < b ? a : b;
}
