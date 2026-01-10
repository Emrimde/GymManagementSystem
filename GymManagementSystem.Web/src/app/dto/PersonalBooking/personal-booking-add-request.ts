export interface PersonalBookingAddRequest {
    trainerId: string;
    trainerRateId: string;
    startDay:string;
    startHour:string;
    isClientReservation: boolean;
}
