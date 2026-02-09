import { PersonalBookingForTrainerDto } from "../PersonalBooking/personal-booking-for-trainer-dto";
import { TrainerTimeOffWebResponse } from "../TrainerTImeOff/trainer-time-off-web-response";

export interface TrainerPanelInfoDto {
  monthlyPersonalBookingCount: string;
  trainerName: string;
  phoneNumber: string;
  email: string;
  location:string;
  personalBookings: PersonalBookingForTrainerDto[];
  trainerTimeOffs: TrainerTimeOffWebResponse[];
}
