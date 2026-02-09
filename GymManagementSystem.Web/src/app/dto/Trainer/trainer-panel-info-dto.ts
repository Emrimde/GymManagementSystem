import { PersonalBookingForTrainerDto } from "../PersonalBooking/personal-booking-for-trainer-dto";

export interface TrainerPanelInfoDto {
  monthlyPersonalBookingCount: string;
  trainerName: string;
  phoneNumber: string;
  email: string;
  location:string;
  personalBookings: PersonalBookingForTrainerDto[];
}
