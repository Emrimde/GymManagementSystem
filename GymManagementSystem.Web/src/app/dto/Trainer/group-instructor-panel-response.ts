import { ScheduledClassDto } from "../ScheduledClass/scheduled-class-dto";

export interface GroupInstructorPanelResponse {
    trainerName: string;
    phoneNumber: string;
    email: string;
    location: string;
    scheduledClasses: ScheduledClassDto[];
}
